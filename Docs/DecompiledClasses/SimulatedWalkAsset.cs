using UnityEngine;

[CreateAssetMenu(fileName = "NewSimulatedWalkAsset", menuName = "Asset/Tests/SimulatedWalkTest")]
public class SimulatedWalkAsset : CustomModeBehaviourAsset
{
	[SerializeField]
	private SimulatedWalkTest m_TestData;

	public override void ExitMode()
	{
		m_TestData.ExitMode();
	}

	public override void EnterPreMode()
	{
		m_TestData.EnterPreMode();
	}

	public override void UpdateMode()
	{
		m_TestData.UpdateMode();
	}
}
