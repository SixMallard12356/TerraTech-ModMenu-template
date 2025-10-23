using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleLightColour : Module
{
	private enum BlockSide
	{
		Left,
		Right,
		Centre
	}

	[SerializeField]
	private Light[] m_Lights;

	[SerializeField]
	[EnumArray(typeof(BlockSide))]
	private Color[] m_Colours;

	private void SetLightColours()
	{
		Vector3 direction = base.block.visible.centrePosition - base.block.tank.visible.centrePosition;
		direction = base.block.tank.rootBlockTrans.InverseTransformDirection(direction);
		BlockSide blockSide = ((!(direction.x < -0.1f)) ? ((direction.x > 0.1f) ? BlockSide.Right : BlockSide.Centre) : BlockSide.Left);
		for (int i = 0; i < m_Lights.Length; i++)
		{
			m_Lights[i].color = m_Colours[(int)blockSide];
		}
	}

	private void OnAttached()
	{
		SetLightColours();
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
	}
}
