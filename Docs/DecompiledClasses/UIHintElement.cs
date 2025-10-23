using TMPro;
using UnityEngine;

public class UIHintElement : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_Text;

	[SerializeField]
	private string m_AnimTransitionBool = "Show";

	[SerializeField]
	private string m_HiddenAnimName = "Hidden";

	private GameHints.HintID m_HintID;

	private Animator m_Animator;

	public Transform trans { get; private set; }

	public void SetupHint(GameHints.HintID hintId, string text)
	{
		m_HintID = hintId;
		m_Text.text = text;
		if (m_Animator != null)
		{
			m_Animator.SetBool(m_AnimTransitionBool, value: true);
		}
	}

	public void PlayHideAnimation()
	{
		m_HintID = GameHints.HintID.Invalid;
		if (m_Animator != null)
		{
			m_Animator.SetBool(m_AnimTransitionBool, value: false);
		}
	}

	public void HideSelf()
	{
		Singleton.Manager<ManHints>.inst.HideHint(m_HintID);
	}

	public bool HasFinished()
	{
		bool result = true;
		if ((bool)m_Animator)
		{
			result = m_Animator.GetCurrentAnimatorStateInfo(0).IsName(m_HiddenAnimName);
		}
		return result;
	}

	private void OnPool()
	{
		m_Animator = GetComponent<Animator>();
		trans = base.transform;
	}

	private void OnSpawn()
	{
		PlayHideAnimation();
	}
}
