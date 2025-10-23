#define UNITY_EDITOR
using System;
using Rewired;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct AxisMapping
{
	public InputAxisMapping m_InputAxis;

	public bool m_Invert;

	public InputAxisMapping m_InputAxis2;

	public bool m_Invert2;

	private static int[] s_RewiredMapping = new int[9] { -1, 0, 1, 68, 69, 70, 71, 4, 72 };

	public int GetRewiredAction()
	{
		return GetRewiredAction(m_InputAxis);
	}

	public static int GetRewiredAction(InputAxisMapping axis)
	{
		if ((int)axis >= s_RewiredMapping.Length)
		{
			return -1;
		}
		return s_RewiredMapping[(int)axis];
	}

	public static InputAxisMapping GetAxisMappingForRewiredAction(int rewiredAction)
	{
		for (int i = 0; i < s_RewiredMapping.Length; i++)
		{
			if (s_RewiredMapping[i] == rewiredAction)
			{
				return (InputAxisMapping)i;
			}
		}
		return InputAxisMapping.Unmapped;
	}

	public static void GetRewiredActionElements(InputAxisMapping axis, bool invert, out ActionElementMap pos, out ActionElementMap neg)
	{
		int rewiredAction = GetRewiredAction(axis);
		if (rewiredAction != -1)
		{
			Pole axisContribution = (invert ? Pole.Negative : Pole.Positive);
			Pole axisContribution2 = ((!invert) ? Pole.Negative : Pole.Positive);
			pos = Singleton.Manager<ManInput>.inst.GetRewiredActionElement(rewiredAction, axisContribution);
			neg = Singleton.Manager<ManInput>.inst.GetRewiredActionElement(rewiredAction, axisContribution2);
			if (pos != null && neg == null && pos.elementType == ControllerElementType.Axis && pos.axisRange == AxisRange.Full)
			{
				neg = new ActionElementMap(rewiredAction, pos.elementType, pos.elementIdentifierId, axisContribution2, pos.axisRange, pos.invert);
			}
			else if (neg != null && pos == null && neg.elementType == ControllerElementType.Axis && neg.axisRange == AxisRange.Full)
			{
				pos = new ActionElementMap(rewiredAction, neg.elementType, neg.elementIdentifierId, axisContribution, neg.axisRange, neg.invert);
			}
		}
		else
		{
			pos = null;
			neg = null;
		}
	}

	public void GetRewiredActionElements(out ActionElementMap pos, out ActionElementMap neg, out ActionElementMap pos2, out ActionElementMap neg2)
	{
		GetRewiredActionElements(m_InputAxis, m_Invert, out pos, out neg);
		GetRewiredActionElements(m_InputAxis2, m_Invert2, out pos2, out neg2);
	}

	public static float ReadRewiredInput(Player player, InputAxisMapping inputAxis, bool invert)
	{
		float num = 0f;
		if (inputAxis != InputAxisMapping.Unmapped)
		{
			int rewiredAction = GetRewiredAction(inputAxis);
			if (rewiredAction != -1)
			{
				num = player.GetAxis(rewiredAction);
				if (invert)
				{
					num = 0f - num;
				}
			}
		}
		return num;
	}

	public float ReadRewiredInput(Player player)
	{
		return Mathf.Clamp(ReadRewiredInput(player, m_InputAxis, m_Invert) + ReadRewiredInput(player, m_InputAxis2, m_Invert2), -1f, 1f);
	}

	public void RemoveMapping(InputAxisMapping inputAxis)
	{
		if (m_InputAxis == inputAxis)
		{
			SetMapping(InputAxisMapping.Unmapped, invert: false, 0);
		}
		if (m_InputAxis2 == inputAxis)
		{
			SetMapping(InputAxisMapping.Unmapped, invert: false, 1);
		}
	}

	public void SetMapping(InputAxisMapping inputAxis, bool invert, int bindingIndex = -1)
	{
		switch (bindingIndex)
		{
		case -1:
			if (inputAxis == InputAxisMapping.Unmapped)
			{
				SetMapping(InputAxisMapping.Unmapped, invert: false, 0);
				SetMapping(InputAxisMapping.Unmapped, invert: false, 1);
				break;
			}
			RemoveMapping(inputAxis);
			if (m_InputAxis == InputAxisMapping.Unmapped)
			{
				m_InputAxis = inputAxis;
				m_Invert = invert;
				break;
			}
			if (m_InputAxis2 == InputAxisMapping.Unmapped)
			{
				m_InputAxis2 = inputAxis;
				m_Invert2 = invert;
				break;
			}
			m_InputAxis = m_InputAxis2;
			m_Invert = m_Invert2;
			m_InputAxis2 = inputAxis;
			m_Invert2 = invert;
			break;
		case 0:
			if (inputAxis != InputAxisMapping.Unmapped && inputAxis == m_InputAxis2)
			{
				SetMapping(InputAxisMapping.Unmapped, invert: false, 1);
			}
			m_InputAxis = inputAxis;
			m_Invert = invert;
			break;
		case 1:
			if (inputAxis != InputAxisMapping.Unmapped && inputAxis == m_InputAxis)
			{
				SetMapping(InputAxisMapping.Unmapped, invert: false, 0);
			}
			m_InputAxis2 = inputAxis;
			m_Invert2 = invert;
			break;
		default:
			d.LogError("AxisMapping: illegal bindingIndex");
			break;
		}
	}

	public void OnSerialise(NetworkWriter writer)
	{
		d.Assert((InputAxisMapping)(byte)m_InputAxis == m_InputAxis);
		d.Assert((InputAxisMapping)(byte)m_InputAxis2 == m_InputAxis2);
		writer.Write((byte)m_InputAxis);
		writer.Write((byte)m_InputAxis2);
		if (m_InputAxis != InputAxisMapping.Unmapped)
		{
			writer.Write(m_Invert);
		}
		if (m_InputAxis2 != InputAxisMapping.Unmapped)
		{
			writer.Write(m_Invert2);
		}
	}

	public void OnDeserialise(NetworkReader reader)
	{
		m_InputAxis = (InputAxisMapping)reader.ReadByte();
		m_InputAxis2 = (InputAxisMapping)reader.ReadByte();
		if (m_InputAxis != InputAxisMapping.Unmapped)
		{
			m_Invert = reader.ReadBoolean();
		}
		if (m_InputAxis2 != InputAxisMapping.Unmapped)
		{
			m_Invert2 = reader.ReadBoolean();
		}
	}

	public override int GetHashCode()
	{
		return (m_InputAxis.GetHashCode() ^ m_Invert.GetHashCode()) + (m_InputAxis2.GetHashCode() ^ m_Invert2.GetHashCode());
	}

	public override bool Equals(object obj)
	{
		if (obj is AxisMapping)
		{
			return (AxisMapping)obj == this;
		}
		return false;
	}

	public static bool operator ==(AxisMapping a, AxisMapping b)
	{
		if (a.m_InputAxis != b.m_InputAxis || a.m_Invert != b.m_Invert || a.m_InputAxis2 != b.m_InputAxis2 || a.m_Invert2 != b.m_Invert2)
		{
			if (a.m_InputAxis == b.m_InputAxis2 && a.m_Invert == b.m_Invert2 && a.m_InputAxis2 == b.m_InputAxis)
			{
				return a.m_Invert2 == b.m_Invert;
			}
			return false;
		}
		return true;
	}

	public static bool operator !=(AxisMapping a, AxisMapping b)
	{
		return !(a == b);
	}
}
