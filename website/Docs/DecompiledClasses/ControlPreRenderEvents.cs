using UnityEngine;

public class ControlPreRenderEvents : MonoBehaviour
{
	private void OnPreRender()
	{
		Singleton.Manager<ManTimedEvents>.inst.PreRender();
	}
}
