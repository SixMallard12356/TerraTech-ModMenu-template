using UnityEngine;
using UnityEngine.UI;

public class UIControlSchemaHUD : UIHUDElement
{
	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private UISchemaIcon m_Icon;

	[SerializeField]
	private UIHighlight m_HighlightPrefab;

	private UIHighlight m_Highlight;

	private ControlScheme m_LastScheme;

	public override void Show(object context)
	{
		base.Show(context);
		if (m_Highlight == null && m_HighlightPrefab != null && !Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_PlayerNotifications.Contains(ManProfile.kSchemeUINotification))
		{
			m_Highlight = m_HighlightPrefab.Spawn();
			m_Highlight.transform.SetParent(base.transform, worldPositionStays: false);
			m_Highlight.SetSize(GetComponent<RectTransform>().rect);
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		RemoveHighlight();
	}

	public void LanguageChanged()
	{
		if (m_LastScheme != null)
		{
			m_Text.text = m_LastScheme.GetName();
		}
	}

	public void ShowSchema(ControlScheme scheme)
	{
		m_LastScheme = scheme;
		m_Text.text = scheme.GetName();
		m_Icon.SetIcon(scheme.Category);
	}

	public void OpenSchemaUI()
	{
		if (!Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.ControlSchema))
		{
			UIScreen screen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.ControlSchema);
			Singleton.Manager<ManUI>.inst.PushScreen(screen, ManUI.PauseType.Pause);
			Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_PlayerNotifications.Add(ManProfile.kSchemeUINotification);
			RemoveHighlight();
		}
	}

	private void RemoveHighlight()
	{
		if (m_Highlight != null)
		{
			m_Highlight.transform.SetParent(null, worldPositionStays: false);
			m_Highlight.Recycle();
			m_Highlight = null;
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(LanguageChanged);
	}

	private void OnRecycle()
	{
		RemoveHighlight();
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(LanguageChanged);
	}
}
