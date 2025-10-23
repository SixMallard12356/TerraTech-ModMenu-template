public class uScript_ShowHintFloating : uScriptLogic
{
	public bool Out => true;

	public void In(UIHintFloating.HintFloatTypes hintAnimation)
	{
		UIHintFloating.HintFloatingContext hintFloatingContext = new UIHintFloating.HintFloatingContext
		{
			m_HintType = hintAnimation
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.HintFloating, hintFloatingContext);
	}
}
