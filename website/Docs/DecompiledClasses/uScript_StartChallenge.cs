using UnityEngine;

public class uScript_StartChallenge : uScriptLogic
{
	public bool Out => true;

	public void In(GameObject owner, ChallengeData data)
	{
		ManChallenge.InitParams initParams = new ManChallenge.InitParams();
		initParams.data = data;
		initParams.placementInfo = default(Challenge.PlacementInfo);
		initParams.endChallengeWhenPlayerDies = true;
		initParams.exitOnOutOfBounds = true;
		Encounter component = owner.GetComponent<Encounter>();
		Vector3 scenePos = ((!(component != null)) ? owner.transform.position : ((!component.HasNoPosition) ? component.Position : Vector3.zero));
		initParams.placementInfo.SetSpawnLocation(WorldPosition.FromScenePosition(in scenePos));
		initParams.placementInfo.yPositionsRelativeToGround = true;
		Singleton.Manager<ManChallenge>.inst.SetupChallenge(initParams);
	}
}
