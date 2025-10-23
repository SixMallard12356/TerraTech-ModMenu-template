using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_PursueFleeingTech")]
public class Mission_PursueFleeingTech_Component : uScriptCode
{
	public Mission_PursueFleeingTech ExposedVariables = new Mission_PursueFleeingTech();

	public SpawnTechData fleeingTechData
	{
		get
		{
			return ExposedVariables.fleeingTechData;
		}
		set
		{
			ExposedVariables.fleeingTechData = value;
		}
	}

	public LocalisedString[] msgCaughtUp
	{
		get
		{
			return ExposedVariables.msgCaughtUp;
		}
		set
		{
			ExposedVariables.msgCaughtUp = value;
		}
	}

	public LocalisedString[] msgTechDestroyed
	{
		get
		{
			return ExposedVariables.msgTechDestroyed;
		}
		set
		{
			ExposedVariables.msgTechDestroyed = value;
		}
	}

	public bool keepFleeingTechAlive
	{
		get
		{
			return ExposedVariables.keepFleeingTechAlive;
		}
		set
		{
			ExposedVariables.keepFleeingTechAlive = value;
		}
	}

	public LocalisedString[] msgIntro
	{
		get
		{
			return ExposedVariables.msgIntro;
		}
		set
		{
			ExposedVariables.msgIntro = value;
		}
	}

	public float caughtUpDistance
	{
		get
		{
			return ExposedVariables.caughtUpDistance;
		}
		set
		{
			ExposedVariables.caughtUpDistance = value;
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
