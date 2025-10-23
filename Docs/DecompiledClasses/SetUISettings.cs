using UnityEngine;

public class SetUISettings : MonoBehaviour
{
	public enum UIChangeElement
	{
		ButtonColours,
		TextSize,
		TextColour,
		RectSize
	}

	public UIChangeElement m_State;
}
