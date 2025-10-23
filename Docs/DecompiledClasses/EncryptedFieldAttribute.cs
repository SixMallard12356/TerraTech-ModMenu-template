#define UNITY_EDITOR
using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class EncryptedFieldAttribute : PropertyAttribute
{
	public Type Type { get; set; }

	public string Salt { get; set; }

	public string Encrypt(object data)
	{
		string text = string.Empty;
		if (Type == typeof(string))
		{
			text = data as string;
		}
		else if (Type == typeof(Vector3))
		{
			text = new IntVector3((Vector3)data / 40f + Vector3.one * 0.5f).ToString();
		}
		else if (Type.IsSubclassOf(typeof(TankPreset)))
		{
			text = ManSaveGame.SaveObjectToRawJson((data as TankPreset).GetTechDataRaw().m_BlockSpecs);
		}
		else if (Type.IsSubclassOf(typeof(UnityEngine.Object)))
		{
			text = (data as UnityEngine.Object).name;
		}
		else
		{
			d.LogError("Unsupported type!");
		}
		return HashCodeUtility.GetSecureHash(text, Salt);
	}
}
