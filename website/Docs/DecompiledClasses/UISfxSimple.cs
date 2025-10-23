using UnityEngine;

public class UISfxSimple : UISfxBase
{
	[SerializeField]
	private ManSFX.UISfxType m_SfxType = ManSFX.UISfxType.Open;

	protected override ManSFX.UISfxType GetSfxType()
	{
		return m_SfxType;
	}
}
