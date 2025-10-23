using UnityEngine;

public class UIHintFloatingElement : MonoBehaviour
{
	[SerializeField]
	private UIHintFloating.HintFloatTypes m_HintType;

	public UIHintFloating.HintFloatTypes HintType => m_HintType;

	public virtual void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHints>.inst.AddHintToFloatContainer(this);
	}
}
