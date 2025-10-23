using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_2_BuildFirstTech")]
public class Mission_GSO_1_2_BuildFirstTech_Component : uScriptCode
{
	public Mission_GSO_1_2_BuildFirstTech ExposedVariables = new Mission_GSO_1_2_BuildFirstTech();

	public GhostBlockSpawnData[] ghostBlockStandard
	{
		get
		{
			return ExposedVariables.ghostBlockStandard;
		}
		set
		{
			ExposedVariables.ghostBlockStandard = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockWheels
	{
		get
		{
			return ExposedVariables.ghostBlockWheels;
		}
		set
		{
			ExposedVariables.ghostBlockWheels = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockGun
	{
		get
		{
			return ExposedVariables.ghostBlockGun;
		}
		set
		{
			ExposedVariables.ghostBlockGun = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockDrill
	{
		get
		{
			return ExposedVariables.ghostBlockDrill;
		}
		set
		{
			ExposedVariables.ghostBlockDrill = value;
		}
	}

	public uScript_AddMessage.MessageData msg01CabExplanation
	{
		get
		{
			return ExposedVariables.msg01CabExplanation;
		}
		set
		{
			ExposedVariables.msg01CabExplanation = value;
		}
	}

	public uScript_AddMessage.MessageData msg04AttachWheels
	{
		get
		{
			return ExposedVariables.msg04AttachWheels;
		}
		set
		{
			ExposedVariables.msg04AttachWheels = value;
		}
	}

	public uScript_AddMessage.MessageData msg06AttachGun
	{
		get
		{
			return ExposedVariables.msg06AttachGun;
		}
		set
		{
			ExposedVariables.msg06AttachGun = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachDrill
	{
		get
		{
			return ExposedVariables.msg07AttachDrill;
		}
		set
		{
			ExposedVariables.msg07AttachDrill = value;
		}
	}

	public BlockTypes blockTypeGun
	{
		get
		{
			return ExposedVariables.blockTypeGun;
		}
		set
		{
			ExposedVariables.blockTypeGun = value;
		}
	}

	public BlockTypes blockTypeWheel
	{
		get
		{
			return ExposedVariables.blockTypeWheel;
		}
		set
		{
			ExposedVariables.blockTypeWheel = value;
		}
	}

	public BlockTypes blockTypeDrill
	{
		get
		{
			return ExposedVariables.blockTypeDrill;
		}
		set
		{
			ExposedVariables.blockTypeDrill = value;
		}
	}

	public BlockTypes blockTypeStandard
	{
		get
		{
			return ExposedVariables.blockTypeStandard;
		}
		set
		{
			ExposedVariables.blockTypeStandard = value;
		}
	}

	public BlockTypes blockTypeLaser
	{
		get
		{
			return ExposedVariables.blockTypeLaser;
		}
		set
		{
			ExposedVariables.blockTypeLaser = value;
		}
	}

	public uScript_AddMessage.MessageData msg00aSkipTutorialIntro
	{
		get
		{
			return ExposedVariables.msg00aSkipTutorialIntro;
		}
		set
		{
			ExposedVariables.msg00aSkipTutorialIntro = value;
		}
	}

	public BlockTypes blockTypeWheelStabiliser
	{
		get
		{
			return ExposedVariables.blockTypeWheelStabiliser;
		}
		set
		{
			ExposedVariables.blockTypeWheelStabiliser = value;
		}
	}

	public uScript_AddMessage.MessageData msg02RotateCamera_Pad
	{
		get
		{
			return ExposedVariables.msg02RotateCamera_Pad;
		}
		set
		{
			ExposedVariables.msg02RotateCamera_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg02RotateCamera
	{
		get
		{
			return ExposedVariables.msg02RotateCamera;
		}
		set
		{
			ExposedVariables.msg02RotateCamera = value;
		}
	}

	public uScript_AddMessage.MessageData msg08ExitBuildBeam
	{
		get
		{
			return ExposedVariables.msg08ExitBuildBeam;
		}
		set
		{
			ExposedVariables.msg08ExitBuildBeam = value;
		}
	}

	public uScript_AddMessage.MessageData msg08ExitBuildBeam_Pad
	{
		get
		{
			return ExposedVariables.msg08ExitBuildBeam_Pad;
		}
		set
		{
			ExposedVariables.msg08ExitBuildBeam_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg00bSkipTutorialExitBeam
	{
		get
		{
			return ExposedVariables.msg00bSkipTutorialExitBeam;
		}
		set
		{
			ExposedVariables.msg00bSkipTutorialExitBeam = value;
		}
	}

	public uScript_AddMessage.MessageData msg00bSkipTutorialExitBeam_Pad
	{
		get
		{
			return ExposedVariables.msg00bSkipTutorialExitBeam_Pad;
		}
		set
		{
			ExposedVariables.msg00bSkipTutorialExitBeam_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AttachBlock_Pad2
	{
		get
		{
			return ExposedVariables.msg03AttachBlock_Pad2;
		}
		set
		{
			ExposedVariables.msg03AttachBlock_Pad2 = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AttachBlock
	{
		get
		{
			return ExposedVariables.msg03AttachBlock;
		}
		set
		{
			ExposedVariables.msg03AttachBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AttachBlock_Pad1
	{
		get
		{
			return ExposedVariables.msg03AttachBlock_Pad1;
		}
		set
		{
			ExposedVariables.msg03AttachBlock_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeaker
	{
		get
		{
			return ExposedVariables.messageSpeaker;
		}
		set
		{
			ExposedVariables.messageSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData msg05RotateToFindWheels
	{
		get
		{
			return ExposedVariables.msg05RotateToFindWheels;
		}
		set
		{
			ExposedVariables.msg05RotateToFindWheels = value;
		}
	}

	public uScript_AddMessage.MessageData msg05RotateToFindWheels_Pad
	{
		get
		{
			return ExposedVariables.msg05RotateToFindWheels_Pad;
		}
		set
		{
			ExposedVariables.msg05RotateToFindWheels_Pad = value;
		}
	}

	public float TimeBeforeBeam
	{
		get
		{
			return ExposedVariables.TimeBeforeBeam;
		}
		set
		{
			ExposedVariables.TimeBeforeBeam = value;
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
