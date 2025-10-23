using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class JSONModuleLoader
{
	protected delegate void ParseFunc(JToken token);

	public abstract string GetModuleKey();

	public abstract bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken data);

	public virtual bool InjectBlock(int blockID, ModdedBlockDefinition def, JToken jToken)
	{
		return true;
	}

	public T GetOrAddComponent<T>(GameObject g) where T : Component
	{
		T component = g.GetComponent<T>();
		if (component == null)
		{
			return g.AddComponent<T>();
		}
		return component;
	}

	public T GetOrAddComponent<T>(Component t) where T : Component
	{
		T component = t.gameObject.GetComponent<T>();
		if (component == null)
		{
			return t.gameObject.AddComponent<T>();
		}
		return component;
	}

	protected static Transform ChildMatching(Transform t, string name)
	{
		if (t.name == name)
		{
			return t;
		}
		foreach (Transform item in t)
		{
			Transform transform = ChildMatching(item, name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	protected static IEnumerable<Transform> ChildrenMatching(Transform t, string name)
	{
		if (t.name.StartsWith(name))
		{
			int result;
			if (name.Length == t.name.Length)
			{
				yield return t;
			}
			else if (int.TryParse(t.name.Substring(name.Length), out result))
			{
				yield return t;
			}
		}
		foreach (Transform item in t)
		{
			foreach (Transform item2 in ChildrenMatching(item, name))
			{
				yield return item2;
			}
		}
	}

	protected void TryParse(JToken token, string key, ParseFunc func)
	{
		if (token.Type == JTokenType.Object && ((JObject)token).TryGetValue(key, out var value))
		{
			func(value);
		}
	}

	protected bool TryParse(JObject obj, string key, bool defaultValue)
	{
		if (obj.TryGetValue(key, out var value) && value.Type == JTokenType.Boolean)
		{
			return value.ToObject<bool>();
		}
		return defaultValue;
	}

	protected int TryParse(JObject obj, string key, int defaultValue)
	{
		if (obj.TryGetValue(key, out var value) && value.Type == JTokenType.Integer)
		{
			return value.ToObject<int>();
		}
		return defaultValue;
	}

	protected string TryParse(JObject obj, string key, string defaultValue)
	{
		if (obj.TryGetValue(key, out var value))
		{
			if (value.Type == JTokenType.String)
			{
				return value.ToObject<string>();
			}
			return value.ToString();
		}
		return defaultValue;
	}

	protected float TryParse(JObject obj, string key, float defaultValue)
	{
		if (obj.TryGetValue(key, out var value))
		{
			if (value.Type == JTokenType.Float)
			{
				return value.ToObject<float>();
			}
			if (value.Type == JTokenType.Integer)
			{
				return value.ToObject<int>();
			}
		}
		return defaultValue;
	}

	protected T TryParseEnum<T>(JObject obj, string key, T defaultValue)
	{
		if (obj.TryGetValue(key, out var value) && value.Type == JTokenType.String)
		{
			return (T)Enum.Parse(typeof(T), value.ToObject<string>());
		}
		return defaultValue;
	}

	protected JObject TryGetObject(JObject obj, string key)
	{
		if (obj.TryGetValue(key, out var value) && value.Type == JTokenType.Object)
		{
			return (JObject)value;
		}
		return null;
	}

	protected JArray TryGetArray(JObject obj, string key)
	{
		if (obj.TryGetValue(key, out var value) && value.Type == JTokenType.Array)
		{
			return (JArray)value;
		}
		return null;
	}
}
