using System.Globalization;

public static class BlockSpecTextSerialisationExtensions
{
	public static void Store(this TankPreset.BlockSpec blockSpec, Module module, string paramName, string strVal)
	{
		blockSpec.Store(module.GetType(), paramName, strVal);
	}

	public static bool TryRetrieve(this TankPreset.BlockSpec blockSpec, Module module, string paramName, out string strVal, string defaultValue = null)
	{
		strVal = blockSpec.Retrieve(module.GetType(), paramName);
		if (!strVal.NullOrEmpty())
		{
			return true;
		}
		strVal = defaultValue ?? string.Empty;
		return false;
	}

	public static void Store(this TankPreset.BlockSpec blockSpec, Module module, string paramName, int intVal)
	{
		blockSpec.Store(module.GetType(), paramName, intVal.ToString());
	}

	public static bool TryRetrieve(this TankPreset.BlockSpec blockSpec, Module module, string paramName, out int intVal, int defaultValue = 0)
	{
		string s = blockSpec.Retrieve(module.GetType(), paramName);
		if (!s.NullOrEmpty() && int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out intVal))
		{
			return true;
		}
		intVal = defaultValue;
		return false;
	}

	public static void Store(this TankPreset.BlockSpec blockSpec, Module module, string paramName, float floatVal)
	{
		blockSpec.Store(module.GetType(), paramName, floatVal.ToString());
	}

	public static bool TryRetrieve(this TankPreset.BlockSpec blockSpec, Module module, string paramName, out float floatVal, float defaultValue = 0f)
	{
		string s = blockSpec.Retrieve(module.GetType(), paramName);
		if (!s.NullOrEmpty() && float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out floatVal))
		{
			return true;
		}
		floatVal = defaultValue;
		return false;
	}

	public static void Store(this TankPreset.BlockSpec blockSpec, Module module, string paramName, bool boolVal)
	{
		blockSpec.Store(module.GetType(), paramName, boolVal ? "true" : "false");
	}

	public static bool TryRetrieve(this TankPreset.BlockSpec blockSpec, Module module, string paramName, out bool boolVal, bool defaultValue = false)
	{
		string text = blockSpec.Retrieve(module.GetType(), paramName);
		if (text == "true")
		{
			boolVal = true;
			return true;
		}
		if (text == "false")
		{
			boolVal = false;
			return true;
		}
		boolVal = defaultValue;
		return false;
	}
}
