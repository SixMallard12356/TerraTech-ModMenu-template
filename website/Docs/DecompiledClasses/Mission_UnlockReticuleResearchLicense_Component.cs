using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockReticuleResearchLicense")]
public class Mission_UnlockReticuleResearchLicense_Component : uScriptCode
{
	public Mission_UnlockReticuleResearchLicense ExposedVariables = new Mission_UnlockReticuleResearchLicense();

	public string triggerVolumeNPC01
	{
		get
		{
			return ExposedVariables.triggerVolumeNPC01;
		}
		set
		{
			ExposedVariables.triggerVolumeNPC01 = value;
		}
	}

	public SpawnTechData[] NPCTech01
	{
		get
		{
			return ExposedVariables.NPCTech01;
		}
		set
		{
			ExposedVariables.NPCTech01 = value;
		}
	}

	public SpawnTechData[] NPCTech02
	{
		get
		{
			return ExposedVariables.NPCTech02;
		}
		set
		{
			ExposedVariables.NPCTech02 = value;
		}
	}

	public string triggerVolumeNPC02
	{
		get
		{
			return ExposedVariables.triggerVolumeNPC02;
		}
		set
		{
			ExposedVariables.triggerVolumeNPC02 = value;
		}
	}

	public SpawnTechData[] NPCTech03
	{
		get
		{
			return ExposedVariables.NPCTech03;
		}
		set
		{
			ExposedVariables.NPCTech03 = value;
		}
	}

	public string triggerVolumeNPC03
	{
		get
		{
			return ExposedVariables.triggerVolumeNPC03;
		}
		set
		{
			ExposedVariables.triggerVolumeNPC03 = value;
		}
	}

	public SpawnTechData[] NPCTech04
	{
		get
		{
			return ExposedVariables.NPCTech04;
		}
		set
		{
			ExposedVariables.NPCTech04 = value;
		}
	}

	public string triggerVolumeNPC04
	{
		get
		{
			return ExposedVariables.triggerVolumeNPC04;
		}
		set
		{
			ExposedVariables.triggerVolumeNPC04 = value;
		}
	}

	public SpawnTechData[] NPCTech05
	{
		get
		{
			return ExposedVariables.NPCTech05;
		}
		set
		{
			ExposedVariables.NPCTech05 = value;
		}
	}

	public string triggerVolumeNPC05
	{
		get
		{
			return ExposedVariables.triggerVolumeNPC05;
		}
		set
		{
			ExposedVariables.triggerVolumeNPC05 = value;
		}
	}

	public Transform NPCDespawnParticleEffect
	{
		get
		{
			return ExposedVariables.NPCDespawnParticleEffect;
		}
		set
		{
			ExposedVariables.NPCDespawnParticleEffect = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker speakerNPC
	{
		get
		{
			return ExposedVariables.speakerNPC;
		}
		set
		{
			ExposedVariables.speakerNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg01aNPC
	{
		get
		{
			return ExposedVariables.msg01aNPC;
		}
		set
		{
			ExposedVariables.msg01aNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg02aNPC
	{
		get
		{
			return ExposedVariables.msg02aNPC;
		}
		set
		{
			ExposedVariables.msg02aNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg03NPC
	{
		get
		{
			return ExposedVariables.msg03NPC;
		}
		set
		{
			ExposedVariables.msg03NPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg04NPC
	{
		get
		{
			return ExposedVariables.msg04NPC;
		}
		set
		{
			ExposedVariables.msg04NPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg05NPC
	{
		get
		{
			return ExposedVariables.msg05NPC;
		}
		set
		{
			ExposedVariables.msg05NPC = value;
		}
	}

	public uScript_AddMessage.MessageData msg01bGSO
	{
		get
		{
			return ExposedVariables.msg01bGSO;
		}
		set
		{
			ExposedVariables.msg01bGSO = value;
		}
	}

	public uScript_AddMessage.MessageData msg02bGSO
	{
		get
		{
			return ExposedVariables.msg02bGSO;
		}
		set
		{
			ExposedVariables.msg02bGSO = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker speakerGSO
	{
		get
		{
			return ExposedVariables.speakerGSO;
		}
		set
		{
			ExposedVariables.speakerGSO = value;
		}
	}

	public uScript_AddMessage.MessageData msg00Intro
	{
		get
		{
			return ExposedVariables.msg00Intro;
		}
		set
		{
			ExposedVariables.msg00Intro = value;
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
