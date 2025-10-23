using UnityEngine;

[AddComponentMenu("uScript/Graphs/FindTradingStationWithMissions")]
public class FindTradingStationWithMissions_Component : uScriptCode
{
	public FindTradingStationWithMissions ExposedVariables = new FindTradingStationWithMissions();

	public LocalisedString[] msgFindTradingStation
	{
		get
		{
			return ExposedVariables.msgFindTradingStation;
		}
		set
		{
			ExposedVariables.msgFindTradingStation = value;
		}
	}

	public LocalisedString[] msgTradingStationFound
	{
		get
		{
			return ExposedVariables.msgTradingStationFound;
		}
		set
		{
			ExposedVariables.msgTradingStationFound = value;
		}
	}

	public LocalisedString[] msgOpenTheMissionBoard
	{
		get
		{
			return ExposedVariables.msgOpenTheMissionBoard;
		}
		set
		{
			ExposedVariables.msgOpenTheMissionBoard = value;
		}
	}

	public LocalisedString[] msgAcceptAMission
	{
		get
		{
			return ExposedVariables.msgAcceptAMission;
		}
		set
		{
			ExposedVariables.msgAcceptAMission = value;
		}
	}

	private void Awake()
	{
		base.useGUILayout = false;
		ExposedVariables.Awake();
		ExposedVariables.SetParent(base.gameObject);
		if ("1.CMR" != uScript_MasterComponent.Version)
		{
			uScriptDebug.Log("The generated code is not compatible with your current uScript Runtime " + uScript_MasterComponent.Version, uScriptDebug.Type.Error);
			ExposedVariables = null;
			Debug.Break();
		}
	}

	private void Start()
	{
		ExposedVariables.Start();
	}

	private void OnEnable()
	{
		ExposedVariables.OnEnable();
	}

	private void OnDisable()
	{
		ExposedVariables.OnDisable();
	}

	private void Update()
	{
		ExposedVariables.Update();
	}

	private void OnDestroy()
	{
		ExposedVariables.OnDestroy();
	}
}
