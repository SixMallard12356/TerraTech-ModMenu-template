using UnityEngine;

[AddComponentMenu("uScript/Graphs/Challenge_GauntletTutorial")]
public class Challenge_GauntletTutorial_Component : uScriptCode
{
	public Challenge_GauntletTutorial ExposedVariables = new Challenge_GauntletTutorial();

	public LocalisedString[] tutorialMsg01RotateCamera
	{
		get
		{
			return ExposedVariables.tutorialMsg01RotateCamera;
		}
		set
		{
			ExposedVariables.tutorialMsg01RotateCamera = value;
		}
	}

	public LocalisedString[] tutorialMsg03aAttachWheels
	{
		get
		{
			return ExposedVariables.tutorialMsg03aAttachWheels;
		}
		set
		{
			ExposedVariables.tutorialMsg03aAttachWheels = value;
		}
	}

	public LocalisedString[] tutorialMsg04AttachGun
	{
		get
		{
			return ExposedVariables.tutorialMsg04AttachGun;
		}
		set
		{
			ExposedVariables.tutorialMsg04AttachGun = value;
		}
	}

	public LocalisedString[] tutorialMsg05ExitBeam
	{
		get
		{
			return ExposedVariables.tutorialMsg05ExitBeam;
		}
		set
		{
			ExposedVariables.tutorialMsg05ExitBeam = value;
		}
	}

	public LocalisedString[] tutorialMsg02AttachBodyBlock
	{
		get
		{
			return ExposedVariables.tutorialMsg02AttachBodyBlock;
		}
		set
		{
			ExposedVariables.tutorialMsg02AttachBodyBlock = value;
		}
	}

	public LocalisedString[] tutorialMsg03aRotateToFindWheels
	{
		get
		{
			return ExposedVariables.tutorialMsg03aRotateToFindWheels;
		}
		set
		{
			ExposedVariables.tutorialMsg03aRotateToFindWheels = value;
		}
	}

	public BlockTypes tutorialWheelBlock
	{
		get
		{
			return ExposedVariables.tutorialWheelBlock;
		}
		set
		{
			ExposedVariables.tutorialWheelBlock = value;
		}
	}

	public float tutorialCamTurnDegrees
	{
		get
		{
			return ExposedVariables.tutorialCamTurnDegrees;
		}
		set
		{
			ExposedVariables.tutorialCamTurnDegrees = value;
		}
	}

	public BlockTypes tutorialGunBlock
	{
		get
		{
			return ExposedVariables.tutorialGunBlock;
		}
		set
		{
			ExposedVariables.tutorialGunBlock = value;
		}
	}

	public Vector3[] tutorialBodyPlacementFilter
	{
		get
		{
			return ExposedVariables.tutorialBodyPlacementFilter;
		}
		set
		{
			ExposedVariables.tutorialBodyPlacementFilter = value;
		}
	}

	public Vector3[] tutorialWheelPlacementFilter
	{
		get
		{
			return ExposedVariables.tutorialWheelPlacementFilter;
		}
		set
		{
			ExposedVariables.tutorialWheelPlacementFilter = value;
		}
	}

	public Vector3[] tutorialGunPlacementFilter
	{
		get
		{
			return ExposedVariables.tutorialGunPlacementFilter;
		}
		set
		{
			ExposedVariables.tutorialGunPlacementFilter = value;
		}
	}

	public LocalisedString[] tutorialMsg07aTryShooting
	{
		get
		{
			return ExposedVariables.tutorialMsg07aTryShooting;
		}
		set
		{
			ExposedVariables.tutorialMsg07aTryShooting = value;
		}
	}

	public LocalisedString[] tutorialMsg06TryDriving
	{
		get
		{
			return ExposedVariables.tutorialMsg06TryDriving;
		}
		set
		{
			ExposedVariables.tutorialMsg06TryDriving = value;
		}
	}

	public float tutorialDistToDrive
	{
		get
		{
			return ExposedVariables.tutorialDistToDrive;
		}
		set
		{
			ExposedVariables.tutorialDistToDrive = value;
		}
	}

	public float tutorialDistToDriveAway
	{
		get
		{
			return ExposedVariables.tutorialDistToDriveAway;
		}
		set
		{
			ExposedVariables.tutorialDistToDriveAway = value;
		}
	}

	public LocalisedString[] tutorialMsg08BlockPalette
	{
		get
		{
			return ExposedVariables.tutorialMsg08BlockPalette;
		}
		set
		{
			ExposedVariables.tutorialMsg08BlockPalette = value;
		}
	}

	public BlockTypes tutorialBodyBlock
	{
		get
		{
			return ExposedVariables.tutorialBodyBlock;
		}
		set
		{
			ExposedVariables.tutorialBodyBlock = value;
		}
	}

	public Vector3[] tutorialBoosterPlacementFilter
	{
		get
		{
			return ExposedVariables.tutorialBoosterPlacementFilter;
		}
		set
		{
			ExposedVariables.tutorialBoosterPlacementFilter = value;
		}
	}

	public LocalisedString[] tutorialMsg03bAttachBoosters
	{
		get
		{
			return ExposedVariables.tutorialMsg03bAttachBoosters;
		}
		set
		{
			ExposedVariables.tutorialMsg03bAttachBoosters = value;
		}
	}

	public BlockTypes tutorialBoosterBlock
	{
		get
		{
			return ExposedVariables.tutorialBoosterBlock;
		}
		set
		{
			ExposedVariables.tutorialBoosterBlock = value;
		}
	}

	public int tutorialWheelAmount
	{
		get
		{
			return ExposedVariables.tutorialWheelAmount;
		}
		set
		{
			ExposedVariables.tutorialWheelAmount = value;
		}
	}

	public int tutorialBoosterAmount
	{
		get
		{
			return ExposedVariables.tutorialBoosterAmount;
		}
		set
		{
			ExposedVariables.tutorialBoosterAmount = value;
		}
	}

	public int tutorialGunAmount
	{
		get
		{
			return ExposedVariables.tutorialGunAmount;
		}
		set
		{
			ExposedVariables.tutorialGunAmount = value;
		}
	}

	public int tutorialBodyBlockAmount
	{
		get
		{
			return ExposedVariables.tutorialBodyBlockAmount;
		}
		set
		{
			ExposedVariables.tutorialBodyBlockAmount = value;
		}
	}

	public LocalisedString[] tutorialMsg07bTryBoosting
	{
		get
		{
			return ExposedVariables.tutorialMsg07bTryBoosting;
		}
		set
		{
			ExposedVariables.tutorialMsg07bTryBoosting = value;
		}
	}

	public float TimeBeforeTutorialStart
	{
		get
		{
			return ExposedVariables.TimeBeforeTutorialStart;
		}
		set
		{
			ExposedVariables.TimeBeforeTutorialStart = value;
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
