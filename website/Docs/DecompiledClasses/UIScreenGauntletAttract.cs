using UnityEngine;

public class UIScreenGauntletAttract : UIScreen
{
	[SerializeField]
	private UILeaderboard m_Leaderboard;

	public UILeaderboard Leaderboard => m_Leaderboard;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if ((bool)Leaderboard)
		{
			Leaderboard.SpawnEntries();
		}
	}

	public override void Hide()
	{
		base.Hide();
		if ((bool)Leaderboard)
		{
			Leaderboard.RecycleEntries();
		}
	}
}
