#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompoundExpression : MonoBehaviour
{
	public struct ElementTransform
	{
		public float a;

		public float b;

		public float c;

		public float d;

		public float x;

		public float y;

		public static ElementTransform identity;

		public ElementTransform(float a, float b, float c, float d, float x, float y)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
			this.x = x;
			this.y = y;
		}

		public static ElementTransform operator *(ElementTransform e, ElementTransform f)
		{
			return new ElementTransform(e.a * f.a + e.b * f.c, e.a * f.b + e.b * f.d, e.c * f.a + e.d * f.c, e.c * f.b + e.d * f.d, e.x * f.a + e.y * f.c + f.x, e.x * f.b + e.y * f.d + f.y);
		}

		public static Vector2 operator *(ElementTransform t, Vector2 v)
		{
			return new Vector2(t.a * v.x + t.b * v.y + t.x, t.c * v.x + t.d * v.y + t.y);
		}

		static ElementTransform()
		{
			identity = new ElementTransform(1f, 0f, 0f, 1f, 0f, 0f);
		}

		public ElementTransform Translate(float p, float q)
		{
			return new ElementTransform(a, b, c, d, x + p, y + q);
		}

		public ElementTransform Rotate(float p)
		{
			float f = p * ((float)Math.PI / 180f);
			return this * new ElementTransform(Mathf.Cos(f), Mathf.Sin(f), 0f - Mathf.Sin(f), Mathf.Cos(f), 0f, 0f);
		}

		public ElementTransform Scale(float p, float q)
		{
			return new ElementTransform(a * p, b * q, c * p, d * q, x * p, y * q);
		}

		public override string ToString()
		{
			return $"{a} {b}, {c} {d}, {x} {y}";
		}
	}

	[Serializable]
	public class EEInstance
	{
		[Serializable]
		public class Target
		{
			public string name;

			public EEInstance instance;

			public float constant;

			public EE.del evaluator;

			public override string ToString()
			{
				if (instance != null)
				{
					return name;
				}
				return constant.ToString();
			}
		}

		public string name;

		public string fn;

		[NonSerialized]
		public Target A;

		[NonSerialized]
		public Target B;

		[NonSerialized]
		public Target C;

		public EE function;

		public bool edited;

		public EEInstance(string name)
		{
			this.name = name;
			fn = "";
			A = new Target
			{
				name = "",
				constant = 0f
			};
			B = new Target
			{
				name = "",
				constant = 0f
			};
			C = new Target
			{
				name = "",
				constant = 0f
			};
			A.evaluator = (ElementTransform t) => A.constant;
			B.evaluator = (ElementTransform t) => B.constant;
			C.evaluator = (ElementTransform t) => C.constant;
		}

		public void LookupFunction()
		{
			if (allEEs.TryGetValue(fn, out function))
			{
				edited = true;
			}
		}

		public bool UpdateName(List<EEInstance> allInstances)
		{
			bool result = false;
			foreach (EEInstance allInstance in allInstances)
			{
				if (allInstance.A.instance == this)
				{
					allInstance.A.name = name;
					result = true;
				}
				if (allInstance.B.instance == this)
				{
					allInstance.B.name = name;
					result = true;
				}
				if (allInstance.C.instance == this)
				{
					allInstance.C.name = name;
					result = true;
				}
			}
			return result;
		}

		public float Evaluate(ElementTransform t)
		{
			if (function == null)
			{
				return 0f;
			}
			return function.evl(t, A.evaluator, B.evaluator, C.evaluator);
		}
	}

	public class EE
	{
		public delegate float del(ElementTransform t);

		public string name;

		public int numArgs;

		public Func<ElementTransform, del, del, del, float> evl;
	}

	public delegate void PopulateCellDelegate(Vector2 position, float val, Color color);

	[SerializeField]
	public List<EEInstance> eeInstances = new List<EEInstance>();

	public int resultIndex = -1;

	public static Dictionary<string, EE> allEEs;

	public EEInstance Resultant
	{
		get
		{
			if (resultIndex != -1)
			{
				return eeInstances[resultIndex];
			}
			return null;
		}
		set
		{
			resultIndex = eeInstances.FindIndex((EEInstance i) => i == value);
		}
	}

	public void UpdateInstanceTarget(EEInstance instance, EEInstance.Target target)
	{
		if (target.instance != null && !target.instance.edited)
		{
			eeInstances.Remove(target.instance);
		}
		if (target.name.NullOrEmpty())
		{
			target.evaluator = (ElementTransform t) => target.constant;
		}
		else
		{
			EEInstance eEInstance = eeInstances.Find((EEInstance i) => i.name == target.name);
			if (eEInstance == null)
			{
				target.instance = new EEInstance(target.name);
				eeInstances.Add(target.instance);
			}
			else
			{
				target.instance = eEInstance;
			}
			target.evaluator = target.instance.Evaluate;
		}
		instance.edited = true;
	}

	public void CheckConsistency()
	{
		if (eeInstances == null || eeInstances.Count == 0 || eeInstances.First().A.evaluator != null)
		{
			return;
		}
		d.Log("rebuilding EE instance targets");
		foreach (EEInstance eeInstance in eeInstances)
		{
			eeInstance.LookupFunction();
			UpdateInstanceTarget(eeInstance, eeInstance.A);
			UpdateInstanceTarget(eeInstance, eeInstance.B);
			UpdateInstanceTarget(eeInstance, eeInstance.C);
		}
	}

	private static EE NewEE(string name, int numArgs)
	{
		EE eE = new EE
		{
			name = name,
			numArgs = numArgs
		};
		allEEs.Add(name, eE);
		return eE;
	}

	static CompoundExpression()
	{
		allEEs = new Dictionary<string, EE>();
		NewEE("one", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => 1f;
		NewEE("zero", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => 0f;
		NewEE("trig", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => Mathf.Sin(t.x) + Mathf.Cos(t.y);
		NewEE("noise", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => MapFuncFastNoise.value(t.x, t.y);
		NewEE("simplex", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => MapFuncSimplexNoise.value(t.x, t.y);
		NewEE("voronoi1", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => MapFuncVoronoi.valuePreset1(t.x, t.y);
		NewEE("voronoi2", 0).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => MapFuncVoronoi.valuePreset2(t.x, t.y);
		NewEE("pass", 1).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t);
		NewEE("neg", 1).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => 0f - a(t);
		NewEE("thresh", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => (a(t) > b(t)) ? 1 : (-1);
		NewEE("add", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t) + b(t);
		NewEE("sub", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t) - b(t);
		NewEE("mul", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t) * b(t);
		NewEE("div", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t) / b(t);
		NewEE("tra", 3).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t.Translate(b(t), c(t)));
		NewEE("rot", 2).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t.Rotate(b(t)));
		NewEE("sca", 3).evl = (ElementTransform t, EE.del a, EE.del b, EE.del c) => a(t.Scale(b(t), c(t)));
	}

	public void GenerateMap(int seed, Vector2 origin, Vector2 size, float cellStep, PopulateCellDelegate populate, bool hexGrid = false)
	{
		float num = (hexGrid ? (Mathf.Sqrt(3f) / 2f) : 1f);
		float num2 = (hexGrid ? (cellStep / 2f) : 0f);
		EEInstance resultant = Resultant;
		if (resultant == null)
		{
			return;
		}
		int num3 = 0;
		for (float num4 = origin.y; num4 < origin.y + size.y; num4 += cellStep * num)
		{
			for (float num5 = origin.x + num2 * (float)(num3++ % 2); num5 < origin.x + size.x; num5 += cellStep)
			{
				ElementTransform identity = ElementTransform.identity;
				identity.x = num5;
				identity.y = num4;
				float num6 = resultant.Evaluate(identity);
				Color color = Color.white * ((num6 + 1f) * 0.5f);
				populate(new Vector2(num5, num4), num6, color);
			}
		}
	}
}
