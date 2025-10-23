public class UISelfDestructButton : UIHUDElement
{
	public class Context
	{
		public string m_TechName;
	}

	private LocalisedString[] m_TooltipStrings;

	private TooltipComponent m_TooltipComponent;

	public void OnButtonClicked()
	{
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			Singleton.Manager<ManNetwork>.inst.MyPlayer.SelfDestructFromUI();
		}
	}

	public override void Show(object context)
	{
		m_TooltipComponent.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 99));
		base.Show(context);
	}

	public void HasBeenDamaged(bool wasDamaged)
	{
	}

	private void OnPool()
	{
		m_TooltipComponent = GetComponent<TooltipComponent>();
	}
}
