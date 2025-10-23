using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class MapGenerator : MonoBehaviour
{
	[Serializable]
	public class Layer
	{
		public delegate float GeneratorDelegate(float x, float y);

		public enum Function
		{
			Trig,
			Simplex,
			FastNoise,
			Voronoi1,
			Voronoi2,
			Voronoi3,
			Zero,
			One,
			MinusOne,
			FastNoiseQuant8,
			FastNoiseQuant8Smooth1,
			FastNoiseQuant8Smooth2,
			FastNoiseQuant8Smooth3,
			FastNoiseQuant16,
			FastNoiseQuant16Smooth1,
			FastNoiseQuant16Smooth2,
			FastNoiseQuant16Smooth3,
			FastNoiseAbs,
			SimplexAbs
		}

		public Vector2 offset = Vector2.zero;

		public float scale = 1f;

		public float rotation;

		public float scaleX;

		public float scaleY;

		public float amplitude = 1f;

		public float bias;

		public float weight = 1f;

		public bool invert;

		[FormerlySerializedAs("_func")]
		[SerializeField]
		private Function function;

		public Operation[] operations;

		public Operation applyOperation;

		public GeneratorDelegate generator;

		private static Dictionary<Function, GeneratorDelegate> functionTable;

		static Layer()
		{
			functionTable = new Dictionary<Function, GeneratorDelegate>();
			functionTable.Add(Function.Zero, (float a, float b) => 0f);
			functionTable.Add(Function.One, (float a, float b) => 1f);
			functionTable.Add(Function.MinusOne, (float a, float b) => -1f);
			functionTable.Add(Function.Trig, (float a, float b) => (Mathf.Cos(a) + Mathf.Sin(b)) * 0.5f);
			functionTable.Add(Function.Simplex, MapFuncSimplexNoise.value);
			functionTable.Add(Function.SimplexAbs, MapFuncSimplexNoise.valueAbs);
			functionTable.Add(Function.FastNoise, MapFuncFastNoise.value);
			functionTable.Add(Function.FastNoiseAbs, MapFuncFastNoise.valueAbs);
			functionTable.Add(Function.FastNoiseQuant8, MapFuncFastNoise.valueQuantised8);
			functionTable.Add(Function.FastNoiseQuant8Smooth1, MapFuncFastNoise.valueQuantised8Smooth1);
			functionTable.Add(Function.FastNoiseQuant8Smooth2, MapFuncFastNoise.valueQuantised8Smooth2);
			functionTable.Add(Function.FastNoiseQuant8Smooth3, MapFuncFastNoise.valueQuantised8Smooth3);
			functionTable.Add(Function.FastNoiseQuant16, MapFuncFastNoise.valueQuantised16);
			functionTable.Add(Function.FastNoiseQuant16Smooth1, MapFuncFastNoise.valueQuantised16Smooth1);
			functionTable.Add(Function.FastNoiseQuant16Smooth2, MapFuncFastNoise.valueQuantised16Smooth2);
			functionTable.Add(Function.FastNoiseQuant16Smooth3, MapFuncFastNoise.valueQuantised16Smooth3);
			functionTable.Add(Function.Voronoi1, MapFuncVoronoi.valuePreset1);
			functionTable.Add(Function.Voronoi2, MapFuncVoronoi.valuePreset2);
			functionTable.Add(Function.Voronoi3, MapFuncVoronoi.valuePreset3);
		}

		public void InitGenerator()
		{
			if (generator == null)
			{
				generator = functionTable[function];
			}
		}

		public void ConvertLegacyFunction(List<Operation> opsList)
		{
			switch (function)
			{
			case Function.SimplexAbs:
				opsList.Add(Operation.New(Operation.Code.Abs, 0f));
				function = Function.Simplex;
				break;
			case Function.FastNoiseAbs:
				opsList.Add(Operation.New(Operation.Code.Abs, 0f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant8:
				opsList.Add(Operation.New(Operation.Code.Quant, 8f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant8Smooth1:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 8f));
				opsList.Add(Operation.New(Operation.Code.Mul, 3f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.25f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant8Smooth2:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 8f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.5f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant8Smooth3:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 8f));
				opsList.Add(Operation.New(Operation.Code.Mul, 1f / 3f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.75f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant16:
				opsList.Add(Operation.New(Operation.Code.Quant, 16f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant16Smooth1:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 16f));
				opsList.Add(Operation.New(Operation.Code.Mul, 3f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.25f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant16Smooth2:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 16f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.5f));
				function = Function.FastNoise;
				break;
			case Function.FastNoiseQuant16Smooth3:
				opsList.Add(Operation.NewBuffered(Operation.Code.Store, 0));
				opsList.Add(Operation.New(Operation.Code.Quant, 16f));
				opsList.Add(Operation.New(Operation.Code.Mul, 1f / 3f));
				opsList.Add(Operation.NewBuffered(Operation.Code.Add, 0));
				opsList.Add(Operation.New(Operation.Code.Mul, 0.75f));
				function = Function.FastNoise;
				break;
			case Function.One:
				opsList.Add(Operation.New(Operation.Code.Add, 1f));
				function = Function.Zero;
				break;
			case Function.MinusOne:
				opsList.Add(Operation.New(Operation.Code.Add, -1f));
				function = Function.Zero;
				break;
			}
		}

		public bool LegacyShouldRemove()
		{
			if (function == Function.Zero)
			{
				return weight == 0f;
			}
			return false;
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct Operation
	{
		public enum Code : byte
		{
			Null,
			Add,
			Sub,
			Mul,
			Div,
			Min,
			Max,
			Abs,
			Sign,
			Quant,
			Store,
			Modify
		}

		public class ParamBuffer
		{
			private float[] param = new float[1];

			public void StoreParam(int index, float value)
			{
				param[index] = value;
			}

			public float RecallParam(int index)
			{
				return param[index];
			}

			public void Expand(int dimension)
			{
				if (dimension > param.Length)
				{
					Array.Resize(ref param, dimension);
				}
			}

			public void Clear()
			{
				Array.Clear(param, 0, param.Length);
			}
		}

		[FieldOffset(0)]
		public float param;

		[FieldOffset(0)]
		public int index;

		[FieldOffset(4)]
		public Code code;

		[FieldOffset(5)]
		public bool buffered;

		private static readonly bool[] s_HasParameter;

		private static readonly Code[] s_ParameterlessOpCodes;

		static Operation()
		{
			s_ParameterlessOpCodes = new Code[3]
			{
				Code.Null,
				Code.Abs,
				Code.Sign
			};
			Code[] array = (Code[])Enum.GetValues(typeof(Code));
			s_HasParameter = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!s_ParameterlessOpCodes.Contains(array[i]))
				{
					s_HasParameter[i] = true;
				}
			}
		}

		public static Operation New(Code code, float param)
		{
			return new Operation
			{
				code = code,
				param = param,
				buffered = false
			};
		}

		public static Operation NewBuffered(Code code, int bufferIndex)
		{
			return new Operation
			{
				code = code,
				index = bufferIndex,
				buffered = true
			};
		}

		public float Evaluate(float input, ParamBuffer buffer)
		{
			if (code == Code.Store)
			{
				buffer.StoreParam(index, input);
				return input;
			}
			return Evaluate(input, buffered ? buffer.RecallParam(index) : param);
		}

		public float Evaluate(float input, float useParam)
		{
			switch (code)
			{
			default:
				return input;
			case Code.Add:
				return input + useParam;
			case Code.Sub:
				return input - useParam;
			case Code.Mul:
				return input * useParam;
			case Code.Div:
				return input / useParam;
			case Code.Min:
				if (!(input < useParam))
				{
					return useParam;
				}
				return input;
			case Code.Max:
				if (!(input > useParam))
				{
					return useParam;
				}
				return input;
			case Code.Abs:
				if (!(input < 0f))
				{
					return input;
				}
				return 0f - input;
			case Code.Sign:
				return (!(input < 0f)) ? 1 : (-1);
			case Code.Quant:
				return (float)((int)((input + 1f) * useParam) >> 1 << 1) / useParam - 1f;
			case Code.Modify:
				return useParam;
			}
		}

		public static bool HasParameter(Code opCode)
		{
			return s_HasParameter[(uint)opCode];
		}
	}

	public class GenerationContext
	{
		public MapGenerator generator;

		public Vector2 originOffset;

		public float totalWeight;

		public float[] mat00;

		public float[] mat01;

		public float[] mat10;

		public float[] mat11;

		public float[] preTransOffX;

		public float[] preTransOffY;

		public Operation.ParamBuffer buffer = new Operation.ParamBuffer();

		public void Setup(MapGenerator generator, int seed)
		{
			this.generator = generator;
			originOffset = Vector2.zero;
			float rotAdjust = 0f;
			if (seed != 0)
			{
				GenerateOffsetRotAdjust(seed, new WorldGenVersionData(int.MaxValue, (BiomeMap.WorldGenVersioningType)2147483647), out originOffset, out rotAdjust);
			}
			if (mat00 == null || mat00.Length < generator.m_Layers.Length)
			{
				mat00 = new float[generator.m_Layers.Length];
			}
			if (mat01 == null || mat01.Length < generator.m_Layers.Length)
			{
				mat01 = new float[generator.m_Layers.Length];
			}
			if (mat10 == null || mat10.Length < generator.m_Layers.Length)
			{
				mat10 = new float[generator.m_Layers.Length];
			}
			if (mat11 == null || mat11.Length < generator.m_Layers.Length)
			{
				mat11 = new float[generator.m_Layers.Length];
			}
			if (preTransOffX == null || preTransOffX.Length < generator.m_Layers.Length)
			{
				preTransOffX = new float[generator.m_Layers.Length];
			}
			if (preTransOffY == null || preTransOffY.Length < generator.m_Layers.Length)
			{
				preTransOffY = new float[generator.m_Layers.Length];
			}
			totalWeight = 0f;
			int num = 0;
			for (int i = 0; i < generator.m_Layers.Length; i++)
			{
				Layer layer = generator.m_Layers[i];
				float num2 = Mathf.Cos(layer.rotation * 0.5f + rotAdjust);
				float num3 = Mathf.Sin(layer.rotation * 0.5f + rotAdjust);
				if (layer.scaleX == 0f && layer.scaleY == 0f)
				{
					layer.scaleX = (layer.scaleY = Mathf.Pow(2f, layer.scale + generator.m_ScaleAll));
					if (Thread.CurrentThread.ManagedThreadId == Singleton.instance.MainThreadID)
					{
						EditorHooks.SetDirty(generator);
					}
				}
				float num4 = 1f / layer.scaleX;
				float num5 = 1f / layer.scaleY;
				if (generator.m_UseLegacy)
				{
					num4 = (num5 = Mathf.Pow(2f, 0f - layer.scale - generator.m_ScaleAll));
				}
				mat00[i] = num2 * num4;
				mat01[i] = (0f - num3) * num4;
				mat10[i] = num3 * num5;
				mat11[i] = num2 * num5;
				float num6 = layer.offset.x * 10f;
				float num7 = layer.offset.y * 10f;
				preTransOffX[i] = num6 * mat00[i] + num7 * mat01[i];
				preTransOffY[i] = num6 * mat10[i] + num7 * mat11[i];
				totalWeight += layer.weight;
				layer.InitGenerator();
				if (layer.operations == null)
				{
					continue;
				}
				for (int j = 0; j < layer.operations.Length; j++)
				{
					if (layer.operations[j].buffered)
					{
						num = Mathf.Max(num, layer.operations[j].index);
					}
				}
			}
			buffer.Expand(num + 1);
			if (totalWeight != 0f)
			{
				totalWeight = 1f / totalWeight;
			}
		}
	}

	private struct RNGCache
	{
		public Vector2 offset;

		public float rotAdjust;
	}

	public delegate void PopulateCellDelegate(Vector2 position, float val, Color color);

	public struct LayerXForm
	{
		public Vector2 offset;

		public float rotation;

		public float scale;

		public LayerXForm(Vector2 offset, float rotation, float scale)
		{
			Vector2 zero = Vector2.zero;
			float num = 0f;
			this.offset = offset + zero;
			this.rotation = rotation + num;
			this.scale = scale;
		}

		public LayerXForm(Vector2 offset, float rotation, float scale, int seed, WorldGenVersionData worldGenVersionData)
		{
			Vector2 zero = Vector2.zero;
			float rotAdjust = 0f;
			if (seed != 0)
			{
				GenerateOffsetRotAdjust(seed, worldGenVersionData, out zero, out rotAdjust);
			}
			this.offset = offset + zero;
			this.rotation = rotation + rotAdjust;
			this.scale = scale;
		}

		public CompoundExpression.ElementTransform ToMatrix()
		{
			return CompoundExpression.ElementTransform.identity.Translate(offset.x, offset.y).Rotate(rotation).Scale(scale, scale);
		}
	}

	public delegate void DoCellDelegate(IntVector2 inds, Vector2 coords);

	[FormerlySerializedAs("_layers")]
	[SerializeField]
	private Layer[] m_Layers = new Layer[0];

	public bool m_UseLegacy = true;

	[FormerlySerializedAs("scaleAll")]
	[SerializeField]
	private float m_ScaleAll = 1f;

	[FormerlySerializedAs("cutoffThreshold")]
	[SerializeField]
	private float m_CutoffThreshold;

	[SerializeField]
	private Gradient m_RenderGradient = CreateDefaultRenderGradient();

	[SerializeField]
	private Color m_RenderColThreshold = Color.green.SetAlpha(0f);

	private const float k_OffsetScale = 10f;

	private const float k_RotationScale = 0.5f;

	private static DRNG s_DRNG = new DRNG();

	private static Dictionary<int, RNGCache> s_RNGCacheLookup = new Dictionary<int, RNGCache>();

	private static int s_OverrideRenderLayer = -1;

	public static bool forceFlatTerrain = false;

	public float CutoffThreshold => m_CutoffThreshold;

	private static void GenerateOffsetRotAdjust(int seed, WorldGenVersionData worldGenVersionData, out Vector2 offset, out float rotAdjust)
	{
		lock (s_RNGCacheLookup)
		{
			if (s_RNGCacheLookup.TryGetValue(seed, out var value))
			{
				offset = value.offset;
				rotAdjust = value.rotAdjust;
				return;
			}
			if (worldGenVersionData < WorldGenVersionData.kLegacy_8808_FixedHash)
			{
				UnityEngine.Random.InitState(seed);
				offset = new Vector2(UnityEngine.Random.Range(int.MinValue, int.MaxValue) & 0xFFFF, UnityEngine.Random.Range(int.MinValue, int.MaxValue) & 0xFFFF);
				rotAdjust = UnityEngine.Random.Range(-(float)Math.PI, (float)Math.PI);
				UnityEngine.Random.InitState(Environment.TickCount);
			}
			else
			{
				s_DRNG.SetSeed((uint)seed);
				int num = s_DRNG.IntPosNeg();
				offset = new Vector2(num & 0xFFFF, num >> 16);
				rotAdjust = s_DRNG.Range(0f, (float)Math.PI * 2f);
			}
			s_RNGCacheLookup.Add(seed, new RNGCache
			{
				offset = offset,
				rotAdjust = rotAdjust
			});
		}
	}

	private static Gradient CreateDefaultRenderGradient()
	{
		Gradient gradient = new Gradient();
		gradient.SetKeys(new GradientColorKey[2]
		{
			new GradientColorKey(Color.black, 0f),
			new GradientColorKey(Color.white, 1f)
		}, new GradientAlphaKey[0]);
		return gradient;
	}

	public void EditorInitFromLegacyParams()
	{
		float num = 0f;
		List<Operation> list = new List<Operation>();
		List<Layer> list2 = new List<Layer>();
		for (int i = 0; i < m_Layers.Length; i++)
		{
			if (m_Layers[i].LegacyShouldRemove())
			{
				continue;
			}
			list2.Add(m_Layers[i]);
			Layer layer = list2.Last();
			layer.applyOperation.code = Operation.Code.Add;
			list.Clear();
			layer.ConvertLegacyFunction(list);
			if (layer.amplitude != 1f)
			{
				list.Add(Operation.New(Operation.Code.Mul, layer.amplitude));
			}
			if (layer.bias != 0f)
			{
				list.Add(Operation.New(Operation.Code.Add, layer.bias));
			}
			list.Add(Operation.New(Operation.Code.Max, -1f));
			list.Add(Operation.New(Operation.Code.Min, 1f));
			if (layer.weight != 1f)
			{
				if (layer.weight == 0f)
				{
					layer.applyOperation.code = Operation.Code.Null;
					if (layer.invert)
					{
						list.Add(Operation.New(Operation.Code.Mul, -1f));
					}
				}
				else if (layer.invert)
				{
					list.Add(Operation.New(Operation.Code.Mul, 0f - layer.weight));
				}
				else
				{
					list.Add(Operation.New(Operation.Code.Mul, layer.weight));
				}
			}
			else if (layer.invert)
			{
				list.Add(Operation.New(Operation.Code.Mul, -1f));
			}
			layer.operations = list.ToArray();
			layer.scaleX = (layer.scaleY = Mathf.Pow(2f, layer.scale + m_ScaleAll));
			num += layer.weight;
		}
		Layer layer2 = new Layer();
		list2.Add(layer2);
		m_Layers = list2.ToArray();
		layer2.applyOperation = Operation.New(Operation.Code.Modify, 0f);
		layer2.operations = new Operation[1] { Operation.New(Operation.Code.Mul, 1f / num) };
	}

	public void RenderMap(GenerationContext context, Vector2 origin, Vector2 size, float cellStep, PopulateCellDelegate populate, int overrideLayer = -1)
	{
		s_OverrideRenderLayer = overrideLayer;
		for (float num = origin.y; num < origin.y + size.y; num += cellStep)
		{
			for (float num2 = origin.x; num2 < origin.x + size.x; num2 += cellStep)
			{
				Vector2 vector = new Vector2(num2, num);
				float num3 = GeneratePoint(context, vector);
				Color color = m_RenderGradient.Evaluate((num3 + 1f) * 0.5f);
				if (num3 >= m_CutoffThreshold)
				{
					color = Color.Lerp(color, m_RenderColThreshold, m_RenderColThreshold.a);
				}
				populate(vector, num3, color);
			}
		}
		s_OverrideRenderLayer = -1;
	}

	public float GeneratePointLegacy(GenerationContext context, Vector2 v)
	{
		float num = 0f;
		float num2 = v.x + context.originOffset.x;
		float num3 = v.y + context.originOffset.y;
		for (int i = 0; i < m_Layers.Length; i++)
		{
			float x = num2 * context.mat00[i] + num3 * context.mat01[i] + context.preTransOffX[i];
			float y = num2 * context.mat10[i] + num3 * context.mat11[i] + context.preTransOffY[i];
			float num4 = m_Layers[i].generator(x, y) * m_Layers[i].amplitude + m_Layers[i].bias;
			float num5 = ((num4 < -1f) ? (-1f) : ((num4 > 1f) ? 1f : num4));
			num += num5 * m_Layers[i].weight * (float)((!m_Layers[i].invert) ? 1 : (-1));
		}
		return num * context.totalWeight;
	}

	public float GeneratePoint(GenerationContext context, Vector2 v)
	{
		if (forceFlatTerrain)
		{
			return 0f;
		}
		if (m_UseLegacy)
		{
			return GeneratePointLegacy(context, v);
		}
		float num = 0f;
		context.buffer.Clear();
		float num2 = v.x + context.originOffset.x;
		float num3 = v.y + context.originOffset.y;
		for (int i = 0; i < m_Layers.Length; i++)
		{
			if (m_Layers[i].applyOperation.code != Operation.Code.Null)
			{
				float num4 = 0f;
				if (m_Layers[i].applyOperation.code == Operation.Code.Modify)
				{
					num4 = num;
				}
				else
				{
					float x = num2 * context.mat00[i] + num3 * context.mat01[i] + context.preTransOffX[i];
					float y = num2 * context.mat10[i] + num3 * context.mat11[i] + context.preTransOffY[i];
					num4 = m_Layers[i].generator(x, y);
				}
				for (int j = 0; j < m_Layers[i].operations.Length; j++)
				{
					num4 = m_Layers[i].operations[j].Evaluate(num4, context.buffer);
				}
				num = m_Layers[i].applyOperation.Evaluate(num, num4);
			}
		}
		if (!(num < -1f))
		{
			if (!(num > 1f))
			{
				return num;
			}
			return 1f;
		}
		return -1f;
	}

	public static void GenerateMapAbstract(LayerXForm baseXForm, Vector2 origin, Vector2 size, float cellStep, DoCellDelegate doCell)
	{
		CompoundExpression.ElementTransform elementTransform = baseXForm.ToMatrix();
		int num = (int)(size.y / cellStep);
		int num2 = (int)(size.x / cellStep);
		float num3 = origin.y;
		for (int i = 0; i < num; i++)
		{
			float num4 = origin.x;
			for (int j = 0; j < num2; j++)
			{
				doCell(new IntVector2(j, i), elementTransform * new Vector2(num4, num3));
				num4 += cellStep;
			}
			num3 += cellStep;
		}
	}

	public static void GenerateOnePoint(LayerXForm baseXForm, Vector2 point, DoCellDelegate doCell)
	{
		CompoundExpression.ElementTransform elementTransform = baseXForm.ToMatrix();
		doCell(IntVector2.zero, elementTransform * point);
	}
}
