#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FunctionTree : ScriptableObject
{
	public enum Function
	{
		Null,
		Constant,
		Trig,
		Noise,
		Simplex,
		Voronoi1,
		Voronoi2,
		Pass,
		Negate,
		Add,
		Subtract,
		Multipy,
		Divide,
		Max,
		Min,
		Translate,
		Rotate,
		Scale
	}

	public class FunctionData
	{
		public Function m_Function;

		public string m_Name;

		public string m_Category;

		public int m_NumInputs;

		public string[] m_ParamNames;

		public float[] m_DefaultParamValues;

		public bool m_BindParamValues;
	}

	public struct Xform
	{
		public float a;

		public float b;

		public float c;

		public float d;

		public float x;

		public float y;

		public static Xform identity;

		public Xform(float a, float b, float c, float d, float x, float y)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
			this.x = x;
			this.y = y;
		}

		public static Xform operator *(Xform p, Xform q)
		{
			return new Xform(p.a * q.a + p.b * q.c, p.a * q.b + p.b * q.d, p.c * q.a + p.d * q.c, p.c * q.b + p.d * q.d, p.x * q.a + p.y * q.c + q.x, p.x * q.b + p.y * q.d + q.y);
		}

		public static Vector2 operator *(Xform t, Vector2 v)
		{
			return new Vector2(t.a * v.x + t.b * v.y + t.x, t.c * v.x + t.d * v.y + t.y);
		}

		public Vector2 Multiply(Vector2 v)
		{
			return new Vector2(a * v.x + b * v.y + x, c * v.x + d * v.y + y);
		}

		static Xform()
		{
			identity = new Xform(1f, 0f, 0f, 1f, 0f, 0f);
		}

		public Xform Translate(float p, float q)
		{
			return new Xform(a, b, c, d, x + p, y + q);
		}

		public Xform Rotate(float p)
		{
			float f = p * ((float)Math.PI / 180f);
			return this * new Xform(Mathf.Cos(f), Mathf.Sin(f), 0f - Mathf.Sin(f), Mathf.Cos(f), 0f, 0f);
		}

		public Xform Scale(float p, float q)
		{
			return new Xform(a * p, b * q, c * p, d * q, x * p, y * q);
		}

		public override string ToString()
		{
			return $"{a} {b}, {c} {d}, {x} {y}";
		}
	}

	public class ExpressionParameterSwapper : ExpressionVisitor
	{
		private ParameterExpression paramToReplace;

		private Expression replaceWith;

		public Expression ReplaceParam(Expression expression, ParameterExpression param, Expression replace)
		{
			if (param.Type != replace.Type)
			{
				d.LogError(string.Concat("incompatible expression types: ", param.Type, " vs ", replace.Type));
				return expression;
			}
			paramToReplace = param;
			replaceWith = replace;
			return Visit(expression);
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			if (p == paramToReplace)
			{
				return replaceWith;
			}
			return base.VisitParameter(p);
		}
	}

	[Serializable]
	public class Node
	{
		public string id;

		public Function function;

		public float value0;

		public float value1;

		[NonSerialized]
		public Node input0;

		[NonSerialized]
		public Node input1;

		[NonSerialized]
		public Node output;

		public string input0Guid;

		public string input1Guid;

		public Vector2 m_EditorNodePosition;

		public Node(Function function, float value0 = 0f, float value1 = 0f)
		{
			this.function = function;
			this.value0 = value0;
			this.value1 = value1;
			id = Guid.NewGuid().ToString();
		}

		public void SetInput(Node node, int index)
		{
			d.Assert(index == 0 || index == 1);
			string text = ((node != null) ? node.id : string.Empty);
			switch (index)
			{
			case 0:
				input0 = node;
				input0Guid = text;
				break;
			case 1:
				input1 = node;
				input1Guid = text;
				break;
			}
			if (node != null)
			{
				node.output = this;
			}
		}

		public Xform GetTransform()
		{
			Xform xform = ((output == null) ? Xform.identity : output.GetTransform());
			return function switch
			{
				Function.Translate => xform.Translate(0f - value0, 0f - value1), 
				Function.Rotate => xform.Rotate(0f - value0), 
				Function.Scale => xform.Scale(1f / value0, 1f / value1), 
				_ => xform, 
			};
		}

		private Expression ReplaceCoordParam(Expression<Func<Vector2, float>> generatorExpression, ParameterExpression inputCoord)
		{
			MethodCallExpression replace = Expression.Call(Expression.Constant(GetTransform(), typeof(Xform)), "Multiply", null, inputCoord);
			return new ExpressionParameterSwapper().ReplaceParam(generatorExpression.Body, generatorExpression.Parameters[0], replace);
		}

		public Expression GetExpression(ParameterExpression inputCoord)
		{
			switch (function)
			{
			case Function.Constant:
				return Expression.Constant(value0, typeof(float));
			case Function.Trig:
				return ReplaceCoordParam((Vector2 v) => Mathf.Sin(v.x) + Mathf.Cos(v.y), inputCoord);
			case Function.Noise:
				return ReplaceCoordParam((Vector2 v) => MapFuncFastNoise.value(v.x, v.y), inputCoord);
			case Function.Simplex:
				return ReplaceCoordParam((Vector2 v) => MapFuncSimplexNoise.value(v.x, v.y), inputCoord);
			case Function.Voronoi1:
				return ReplaceCoordParam((Vector2 v) => MapFuncVoronoi.valuePreset1(v.x, v.y), inputCoord);
			case Function.Voronoi2:
				return ReplaceCoordParam((Vector2 v) => MapFuncVoronoi.valuePreset2(v.x, v.y), inputCoord);
			case Function.Pass:
				return input0.GetExpression(inputCoord);
			case Function.Negate:
				return Expression.Negate(input0.GetExpression(inputCoord));
			case Function.Add:
				return Expression.Add(input0.GetExpression(inputCoord), input1.GetExpression(inputCoord));
			case Function.Subtract:
				return Expression.Subtract(input0.GetExpression(inputCoord), input1.GetExpression(inputCoord));
			case Function.Multipy:
				return Expression.Multiply(input0.GetExpression(inputCoord), input1.GetExpression(inputCoord));
			case Function.Divide:
				return Expression.Divide(input0.GetExpression(inputCoord), input1.GetExpression(inputCoord));
			case Function.Max:
			{
				Expression expression3 = input0.GetExpression(inputCoord);
				Expression expression4 = input1.GetExpression(inputCoord);
				return Expression.Condition(Expression.GreaterThan(expression3, expression4), expression3, expression4);
			}
			case Function.Min:
			{
				Expression expression = input0.GetExpression(inputCoord);
				Expression expression2 = input1.GetExpression(inputCoord);
				return Expression.Condition(Expression.LessThan(expression, expression2), expression, expression2);
			}
			case Function.Translate:
				return input0.GetExpression(inputCoord);
			case Function.Rotate:
				return input0.GetExpression(inputCoord);
			case Function.Scale:
				return input0.GetExpression(inputCoord);
			default:
				return Expression.Constant(0f, typeof(float));
			}
		}
	}

	public delegate void PopulateCellDelegate(Vector2 position, float val, Color color);

	public List<Node> m_AllNodes = new List<Node>();

	[SerializeField]
	private string m_RootNodeGuid;

	private Node m_RootNode;

	private Func<Vector2, float> m_CompiledExpression;

	public static Dictionary<Function, FunctionData> s_FunctionData;

	public Node RootNode
	{
		get
		{
			if (m_RootNode == null)
			{
				m_RootNode = m_AllNodes.Find((Node x) => x.id == m_RootNodeGuid);
			}
			return m_RootNode;
		}
		set
		{
			m_RootNode = value;
			m_RootNodeGuid = ((m_RootNode != null) ? m_RootNode.id : string.Empty);
		}
	}

	static FunctionTree()
	{
		s_FunctionData = new Dictionary<Function, FunctionData>();
		s_FunctionData.Add(Function.Null, new FunctionData
		{
			m_Name = "Null",
			m_Category = "",
			m_Function = Function.Null,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Constant, new FunctionData
		{
			m_Name = "Constant",
			m_Category = "Generator/",
			m_Function = Function.Constant,
			m_NumInputs = 0,
			m_ParamNames = new string[1] { "Value : " },
			m_DefaultParamValues = new float[1]
		});
		s_FunctionData.Add(Function.Trig, new FunctionData
		{
			m_Name = "Trigonometry",
			m_Category = "Generator/",
			m_Function = Function.Trig,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Noise, new FunctionData
		{
			m_Name = "Noise",
			m_Category = "Generator/",
			m_Function = Function.Noise,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Simplex, new FunctionData
		{
			m_Name = "Simplex",
			m_Category = "Generator/",
			m_Function = Function.Simplex,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Voronoi1, new FunctionData
		{
			m_Name = "Voronoi1",
			m_Category = "Generator/",
			m_Function = Function.Voronoi1,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Voronoi2, new FunctionData
		{
			m_Name = "Voronoi2",
			m_Category = "Generator/",
			m_Function = Function.Voronoi2,
			m_NumInputs = 0
		});
		s_FunctionData.Add(Function.Pass, new FunctionData
		{
			m_Name = "Pass",
			m_Category = "Unary/",
			m_Function = Function.Pass,
			m_NumInputs = 1
		});
		s_FunctionData.Add(Function.Negate, new FunctionData
		{
			m_Name = "Negate",
			m_Category = "Unary/",
			m_Function = Function.Negate,
			m_NumInputs = 1
		});
		s_FunctionData.Add(Function.Add, new FunctionData
		{
			m_Name = "Add",
			m_Category = "Binary/",
			m_Function = Function.Add,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Subtract, new FunctionData
		{
			m_Name = "Subtract",
			m_Category = "Binary/",
			m_Function = Function.Subtract,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Multipy, new FunctionData
		{
			m_Name = "Multipy",
			m_Category = "Binary/",
			m_Function = Function.Multipy,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Divide, new FunctionData
		{
			m_Name = "Divide",
			m_Category = "Binary/",
			m_Function = Function.Divide,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Max, new FunctionData
		{
			m_Name = "Max",
			m_Category = "Binary/",
			m_Function = Function.Max,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Min, new FunctionData
		{
			m_Name = "Min",
			m_Category = "Binary/",
			m_Function = Function.Min,
			m_NumInputs = 2
		});
		s_FunctionData.Add(Function.Scale, new FunctionData
		{
			m_Name = "Scale",
			m_Category = "Transform/",
			m_Function = Function.Scale,
			m_NumInputs = 1,
			m_ParamNames = new string[2] { "x : ", "y : " },
			m_DefaultParamValues = new float[2] { 1f, 1f },
			m_BindParamValues = true
		});
		s_FunctionData.Add(Function.Translate, new FunctionData
		{
			m_Name = "Translate",
			m_Category = "Transform/",
			m_Function = Function.Translate,
			m_NumInputs = 1,
			m_ParamNames = new string[2] { "x : ", "y : " },
			m_DefaultParamValues = new float[2]
		});
		s_FunctionData.Add(Function.Rotate, new FunctionData
		{
			m_Name = "Rotate",
			m_Category = "Transform/",
			m_Function = Function.Rotate,
			m_NumInputs = 1,
			m_ParamNames = new string[1] { "Angle : " },
			m_DefaultParamValues = new float[1]
		});
	}

	private bool Compile(Node node, out ParameterExpression param, out Func<Vector2, float> compiledExpression)
	{
		if (node == null)
		{
			param = null;
			compiledExpression = (Vector2 v) => 0f;
			return false;
		}
		param = Expression.Parameter(typeof(Vector2), "v");
		Func<Vector2, float> func = Expression.Lambda<Func<Vector2, float>>(node.GetExpression(param), new ParameterExpression[1] { param }).Compile();
		compiledExpression = func;
		return true;
	}

	private void CompileRoot()
	{
		Compile(RootNode, out var _, out m_CompiledExpression);
	}

	private float Evaluate(Vector2 coords, ref Func<Vector2, float> compiledExpression)
	{
		d.Assert(compiledExpression != null);
		return compiledExpression(coords);
	}

	public Node AddNode(Function function, float value0 = 0f, float value1 = 0f)
	{
		Node node = new Node(function, value0, value1);
		AddNode(node);
		return node;
	}

	public void AddNode(Node node)
	{
		if (m_AllNodes == null)
		{
			m_AllNodes = new List<Node>();
		}
		m_AllNodes.Add(node);
	}

	public void CreateMap(Vector2 origin, Vector2 size, PopulateCellDelegate populate)
	{
		CompileRoot();
		for (float num = origin.y; num < origin.y + size.y; num += 1f)
		{
			for (float num2 = origin.x; num2 < origin.x + size.x; num2 += 1f)
			{
				float num3 = Evaluate(new Vector2(num2, num), ref m_CompiledExpression);
				populate(new Vector2(num2, num), num3, Color.white * ((num3 + 1f) * 0.5f));
			}
		}
	}

	public void CreateMap(Node viewNode, Vector2 origin, Vector2 size, PopulateCellDelegate populate)
	{
		Compile(viewNode, out var _, out var compiledExpression);
		for (float num = origin.y; num < origin.y + size.y; num += 1f)
		{
			for (float num2 = origin.x; num2 < origin.x + size.x; num2 += 1f)
			{
				float num3 = Evaluate(new Vector2(num2, num), ref compiledExpression);
				populate(new Vector2(num2, num), num3, Color.white * ((num3 + 1f) * 0.5f));
			}
		}
	}

	public void LinkNodesFromGuids()
	{
		Dictionary<string, Node> dictionary = new Dictionary<string, Node>();
		foreach (Node allNode in m_AllNodes)
		{
			dictionary.Add(allNode.id, allNode);
		}
		foreach (Node allNode2 in m_AllNodes)
		{
			if (!string.IsNullOrEmpty(allNode2.input0Guid))
			{
				allNode2.SetInput(dictionary[allNode2.input0Guid], 0);
			}
			if (!string.IsNullOrEmpty(allNode2.input1Guid))
			{
				allNode2.SetInput(dictionary[allNode2.input1Guid], 1);
			}
		}
	}

	private void OnEnable()
	{
		LinkNodesFromGuids();
	}
}
