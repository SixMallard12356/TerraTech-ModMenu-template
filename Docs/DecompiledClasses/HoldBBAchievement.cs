using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/HoldBBAchievement")]
public class HoldBBAchievement : AchievementObject
{
	[SerializeField]
	private int m_BBTarget;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnMoneyAmountChanged);
	}

	private void OnMoneyAmountChanged(int newAmount)
	{
		if (IsActive() && newAmount >= m_BBTarget)
		{
			CompleteAchievement();
			Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Unsubscribe(OnMoneyAmountChanged);
		}
	}
}
