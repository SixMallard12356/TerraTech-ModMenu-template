using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIExitChallenge : MonoBehaviour
{
	public void OnButtonClicked()
	{
		Singleton.Manager<ManChallenge>.inst.KillChallenge(ManChallenge.ChallengeEndReason.PlayerExit);
	}
}
