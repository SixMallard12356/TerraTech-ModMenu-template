#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ControlScheme
{
	[SerializeField]
	[JsonProperty]
	[HideInInspector]
	private int m_ID;

	[JsonProperty]
	[SerializeField]
	private ControlSchemeCategory m_Category;

	[SerializeField]
	[JsonProperty]
	private string m_CustomName;

	[JsonProperty]
	[SerializeField]
	private int m_FormatVersion;

	private Color m_IconColour;

	private int m_Icon;

	[JsonProperty]
	[SerializeField]
	private bool m_ReverseSteering;

	[EnumArray(typeof(MovementAxis))]
	[JsonProperty]
	[SerializeField]
	private AxisMapping[] m_InputMapping;

	private static int s_IDGenerator;

	[JsonIgnore]
	public bool IsCustom => m_Category == ControlSchemeCategory.Custom;

	[JsonIgnore]
	public ControlSchemeCategory Category
	{
		get
		{
			return m_Category;
		}
		set
		{
			m_Category = value;
		}
	}

	[JsonIgnore]
	public string CustomName
	{
		get
		{
			return m_CustomName;
		}
		set
		{
			m_CustomName = value;
		}
	}

	[JsonIgnore]
	public int Icon
	{
		get
		{
			return m_Icon;
		}
		set
		{
			m_Icon = value;
		}
	}

	[JsonIgnore]
	public Color IconColour
	{
		get
		{
			return m_IconColour;
		}
		set
		{
			m_IconColour = value;
		}
	}

	[JsonIgnore]
	public bool ReverseSteering
	{
		get
		{
			return m_ReverseSteering;
		}
		set
		{
			m_ReverseSteering = value;
		}
	}

	[JsonIgnore]
	public AxisMapping[] Mappings => m_InputMapping;

	[JsonIgnore]
	public int ID => m_ID;

	[JsonIgnore]
	public int FormatVersion
	{
		get
		{
			return m_FormatVersion;
		}
		set
		{
			m_FormatVersion = value;
		}
	}

	public ControlScheme()
	{
		m_ID = GetNextUID();
		m_InputMapping = new AxisMapping[EnumValuesIterator<MovementAxis>.Count];
	}

	public string GetDefaultName()
	{
		return m_Category switch
		{
			ControlSchemeCategory.Custom => string.Empty, 
			ControlSchemeCategory.Car => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 7), 
			ControlSchemeCategory.Aeroplane => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 8), 
			ControlSchemeCategory.Helicopter => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 9), 
			ControlSchemeCategory.Hovercraft => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 10), 
			ControlSchemeCategory.Rocket => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 11), 
			ControlSchemeCategory.AntiGrav => Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 0), 
			_ => string.Empty, 
		};
	}

	public string GetName()
	{
		if (!m_CustomName.NullOrEmpty())
		{
			return m_CustomName;
		}
		return GetDefaultName();
	}

	public void SetName(string newName)
	{
		if (newName == GetDefaultName())
		{
			m_CustomName = string.Empty;
		}
		else
		{
			m_CustomName = newName;
		}
	}

	public static string GetAxisName(InputAxisMapping axis)
	{
		switch (axis)
		{
		case InputAxisMapping.Unmapped:
			return "-";
		case InputAxisMapping.Axis1:
		case InputAxisMapping.Axis2:
		case InputAxisMapping.Axis3:
		case InputAxisMapping.Axis4:
		case InputAxisMapping.Axis5:
		case InputAxisMapping.Axis6:
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 29), (int)(1 + axis - 1));
		case InputAxisMapping.Boost1:
		case InputAxisMapping.Boost2:
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 36), (int)(1 + axis - 7));
		default:
			return "???";
		}
	}

	public int GetAxisMappingBitfield()
	{
		int num = 0;
		for (int i = 0; i < m_InputMapping.Length; i++)
		{
			if (m_InputMapping[i].GetRewiredAction() != -1)
			{
				num |= 1 << i;
			}
		}
		return num;
	}

	public bool HasAxisMapping(MovementAxis axis)
	{
		AxisMapping axisMapping = GetAxisMapping(axis);
		if (axisMapping.m_InputAxis == InputAxisMapping.Unmapped)
		{
			return axisMapping.m_InputAxis2 != InputAxisMapping.Unmapped;
		}
		return true;
	}

	public AxisMapping GetAxisMapping(MovementAxis axis)
	{
		if (axis >= MovementAxis.MoveX_MoveRight && (int)axis < m_InputMapping.Length)
		{
			return m_InputMapping[(int)axis];
		}
		d.LogErrorFormat("Unexpected axis {0} passed to GetAxisMapping", axis);
		return default(AxisMapping);
	}

	public void SetAxisMapping(MovementAxis axis, int bindingIndex, InputAxisMapping inputAxis, bool invert)
	{
		if (axis >= MovementAxis.MoveX_MoveRight && (int)axis < m_InputMapping.Length)
		{
			m_InputMapping[(int)axis].SetMapping(inputAxis, invert, bindingIndex);
			return;
		}
		d.LogErrorFormat("Unexpected axis {0} passed to AddAxisMapping", axis);
	}

	public void ClearAxisMapping(MovementAxis axis)
	{
		if (axis >= MovementAxis.MoveX_MoveRight && (int)axis < m_InputMapping.Length)
		{
			m_InputMapping[(int)axis] = default(AxisMapping);
			return;
		}
		d.LogErrorFormat("Unexpected axis {0} passed to ClearAxisMapping", axis);
	}

	public void CopyDataFrom(ControlScheme from)
	{
		m_Category = from.m_Category;
		m_CustomName = from.m_CustomName;
		m_FormatVersion = from.m_FormatVersion;
		m_Icon = from.m_Icon;
		m_IconColour = from.m_IconColour;
		ReverseSteering = from.ReverseSteering;
		m_InputMapping = (AxisMapping[])from.m_InputMapping.Clone();
	}

	public ControlScheme CreateCopy()
	{
		ControlScheme obj = (ControlScheme)MemberwiseClone();
		obj.m_InputMapping = (AxisMapping[])m_InputMapping.Clone();
		return obj;
	}

	public ControlScheme CreateCopyAsNewCustom(string newName)
	{
		ControlScheme controlScheme = CreateCopy();
		controlScheme.m_Category = ControlSchemeCategory.Custom;
		controlScheme.m_ID = GetNextUID();
		controlScheme.SetName(newName);
		return controlScheme;
	}

	public bool IsInputMappingUsed(InputAxisMapping mapping)
	{
		AxisMapping[] mappings = Mappings;
		for (int i = 0; i < mappings.Length; i++)
		{
			AxisMapping axisMapping = mappings[i];
			if (axisMapping.m_InputAxis == mapping || axisMapping.m_InputAxis2 == mapping)
			{
				return true;
			}
		}
		return false;
	}

	public void ClearMappingsWithInput(InputAxisMapping mapping)
	{
		for (int i = 0; i < Mappings.Length; i++)
		{
			if (Mappings[i].m_InputAxis == mapping)
			{
				Mappings[i] = new AxisMapping
				{
					m_InputAxis = Mappings[i].m_InputAxis2,
					m_Invert = Mappings[i].m_Invert2
				};
			}
			if (Mappings[i].m_InputAxis2 == mapping)
			{
				Mappings[i].m_InputAxis2 = InputAxisMapping.Unmapped;
			}
		}
	}

	public bool Equals(ControlScheme other, bool requireNameMatch = true)
	{
		if (requireNameMatch)
		{
			if (m_Category != other.m_Category)
			{
				return false;
			}
			if (m_FormatVersion != other.m_FormatVersion)
			{
				return false;
			}
			if (m_CustomName != other.m_CustomName)
			{
				return false;
			}
			if (IsCustom)
			{
				if (m_IconColour != other.m_IconColour)
				{
					return false;
				}
				if (m_Icon != other.m_Icon)
				{
					return false;
				}
			}
		}
		if (m_ReverseSteering != other.m_ReverseSteering)
		{
			return false;
		}
		if (m_InputMapping.Length != other.m_InputMapping.Length)
		{
			return false;
		}
		for (int i = 0; i < m_InputMapping.Length; i++)
		{
			if (m_InputMapping[i] != other.m_InputMapping[i])
			{
				return false;
			}
		}
		return true;
	}

	public void OnSerialise(NetworkWriter writer)
	{
		writer.WritePackedInt32((int)m_Category);
		if (m_Category == ControlSchemeCategory.Custom)
		{
			writer.Write((m_CustomName == null) ? string.Empty : m_CustomName);
			writer.WritePackedInt32(m_FormatVersion);
			writer.Write(m_ReverseSteering);
			writer.WritePackedInt32(m_Icon);
			writer.Write(m_IconColour);
			writer.WritePackedInt32(m_InputMapping.Length);
			for (int i = 0; i < m_InputMapping.Length; i++)
			{
				m_InputMapping[i].OnSerialise(writer);
			}
		}
	}

	public void OnDeserialise(NetworkReader reader)
	{
		m_Category = (ControlSchemeCategory)reader.ReadPackedInt32();
		if (m_Category == ControlSchemeCategory.Custom)
		{
			m_CustomName = reader.ReadString();
			m_FormatVersion = reader.ReadPackedInt32();
			m_ReverseSteering = reader.ReadBoolean();
			m_Icon = reader.ReadPackedInt32();
			m_IconColour = reader.ReadColor();
			m_InputMapping = new AxisMapping[reader.ReadPackedInt32()];
			for (int i = 0; i < m_InputMapping.Length; i++)
			{
				m_InputMapping[i].OnDeserialise(reader);
			}
		}
	}

	private static int GetNextUID()
	{
		int a = (int)DateTime.Now.Ticks + s_IDGenerator;
		s_IDGenerator++;
		return Maths.HashInt(a) ^ Environment.MachineName.GetHashCode();
	}
}
