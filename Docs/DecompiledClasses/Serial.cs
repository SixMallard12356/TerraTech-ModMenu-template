using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

public static class Serial
{
	public abstract class Data
	{
		public abstract void Clear();
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class SaveBasic : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class LoadBasic : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class SaveRuntime : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class LoadRuntime : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class SaveEffects : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class LoadEffects : Attribute
	{
	}

	public enum Mode
	{
		SaveBasic,
		LoadBasic,
		SaveRuntime,
		LoadRuntime,
		SaveEffects,
		LoadEffects
	}

	private struct CachedSerializer
	{
		public Type objectType;

		public Action<Mode, Dictionary<int, Data>> serializer;
	}

	public class SerializerBuilder<SerialDataType> where SerialDataType : Data, new()
	{
		public Action<Mode, Dictionary<int, Data>> Get<ObjectType>(ObjectType obj)
		{
			int hashCode = typeof(ObjectType).GUID.GetHashCode();
			if (!s_CachedSerializers.TryGetValue(hashCode, out var value))
			{
				value = new CachedSerializer
				{
					objectType = typeof(ObjectType),
					serializer = GenerateSerializer(obj)
				};
				s_CachedSerializers.Add(hashCode, value);
			}
			return value.serializer;
		}

		private static Action<Mode, Dictionary<int, Data>> GenerateSerializer<ObjectType>(ObjectType obj)
		{
			List<Action<SerialDataType, ObjectType>> list = new List<Action<SerialDataType, ObjectType>>();
			foreach (object value in Enum.GetValues(typeof(Mode)))
			{
				Action<SerialDataType, ObjectType> item = null;
				MethodInfo[] methods = typeof(SerialDataType).GetMethods();
				foreach (MethodInfo methodInfo in methods)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (methodInfo.ReturnType == typeof(void) && parameters.Length == 1 && parameters[0].ParameterType == typeof(ObjectType) && methodInfo.Name == value.ToString())
					{
						ParameterExpression parameterExpression = Expression.Parameter(typeof(SerialDataType), "data");
						ParameterExpression parameterExpression2 = Expression.Parameter(typeof(ObjectType), "obj");
						item = Expression.Lambda<Action<SerialDataType, ObjectType>>(Expression.Call(parameterExpression, methodInfo, parameterExpression2), new ParameterExpression[2] { parameterExpression, parameterExpression2 }).Compile();
						break;
					}
				}
				list.Add(item);
			}
			Action<SerialDataType, ObjectType>[] actionTable = list.ToArray();
			return delegate(Mode mode, Dictionary<int, Data> data)
			{
				GenericSerializer(mode, data, obj, actionTable);
			};
		}

		private static void GenericSerializer<ObjectType>(Mode mode, Dictionary<int, Data> data, ObjectType obj, Action<SerialDataType, ObjectType>[] table)
		{
			int hashCode = typeof(ObjectType).GUID.GetHashCode();
			SerialDataType val;
			if (mode == Mode.SaveBasic || mode == Mode.SaveRuntime || mode == Mode.SaveEffects)
			{
				val = ObjectPoolExtensions.SpawnPooled<SerialDataType>();
				val.Clear();
				data[hashCode] = val;
			}
			else
			{
				val = data[hashCode] as SerialDataType;
			}
			if (table[(int)mode] != null)
			{
				table[(int)mode](val, obj);
			}
		}
	}

	private static Dictionary<int, CachedSerializer> s_CachedSerializers = new Dictionary<int, CachedSerializer>();

	public static SerializerBuilder<SerialDataType> GetSerializer<SerialDataType>() where SerialDataType : Data, new()
	{
		return new SerializerBuilder<SerialDataType>();
	}
}
