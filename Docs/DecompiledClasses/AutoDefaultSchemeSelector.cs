using System.Collections.Generic;
using UnityEngine;

internal class AutoDefaultSchemeSelector
{
	private ControlSchemeLibrary m_Library;

	private List<ControlScheme> m_Schemes;

	public List<ControlScheme> Schemes => m_Schemes;

	public static bool HasUprightWing(Tank tank)
	{
		bool result = false;
		BlockManager.BlockIterator<ModuleWing>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleWing>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleWing current = enumerator.Current;
			if (IsUpright(current.block, tank.rootBlockTrans) && IsForwardFacing(current.block, tank.rootBlockTrans))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public AutoDefaultSchemeSelector(ControlSchemeLibrary library)
	{
		m_Library = library;
		m_Schemes = new List<ControlScheme>();
	}

	public void UpdateFromTech(Tank tank)
	{
		Transform rootBlockTrans = tank.rootBlockTrans;
		Add(ControlSchemeCategory.Car);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = HasUprightWing(tank);
		BlockManager.BlockIterator<ModuleBooster>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleBooster>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleBooster current = enumerator.Current;
			if (current.IsRotor && IsAligned(current.RotorDefaultDirection, rootBlockTrans.up))
			{
				flag = true;
				break;
			}
		}
		BlockManager.BlockIterator<ModuleHover>.Enumerator enumerator2 = tank.blockman.IterateBlockComponents<ModuleHover>().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			if (IsUpright(enumerator2.Current.block, tank.rootBlockTrans))
			{
				flag2 = true;
				break;
			}
		}
		if (flag3)
		{
			Add(ControlSchemeCategory.Aeroplane);
		}
		if (flag)
		{
			Add(ControlSchemeCategory.Helicopter);
		}
		if (flag2)
		{
			Add(ControlSchemeCategory.Hovercraft);
		}
	}

	private static bool IsAligned(Vector3 a, Vector3 b)
	{
		if (!(a - b).IsZeroEpsilon(0.1f))
		{
			return (a + b).IsZeroEpsilon(0.1f);
		}
		return true;
	}

	private static bool IsUpright(TankBlock block, Transform rootTrans)
	{
		return (block.trans.up - rootTrans.up).IsZeroEpsilon(0.1f);
	}

	private static bool IsForwardFacing(TankBlock block, Transform rootTrans)
	{
		return (block.trans.forward - rootTrans.forward).IsZeroEpsilon(0.1f);
	}

	public void Add(ControlSchemeCategory controlSchemeCategory)
	{
		if (m_Schemes.Count < m_Library.GetMaxSchemesPerTech())
		{
			ControlScheme controlScheme = m_Library.GetControlScheme(controlSchemeCategory);
			if (controlScheme != null && !m_Schemes.Contains(controlScheme))
			{
				m_Schemes.Add(controlScheme);
			}
		}
	}
}
