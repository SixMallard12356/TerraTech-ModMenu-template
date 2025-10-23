using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_TakeBlockFromEnemyTech")]
public class Mission_TakeBlockFromEnemyTech_Component : uScriptCode
{
	public Mission_TakeBlockFromEnemyTech ExposedVariables = new Mission_TakeBlockFromEnemyTech();

	public BlockTypes blockToGet
	{
		get
		{
			return ExposedVariables.blockToGet;
		}
		set
		{
			ExposedVariables.blockToGet = value;
		}
	}

	public LocalisedString[] msgBlockDestroyed
	{
		get
		{
			return ExposedVariables.msgBlockDestroyed;
		}
		set
		{
			ExposedVariables.msgBlockDestroyed = value;
		}
	}

	public SpawnTechData enemyTechData
	{
		get
		{
			return ExposedVariables.enemyTechData;
		}
		set
		{
			ExposedVariables.enemyTechData = value;
		}
	}

	public LocalisedString[] msgPlayerHasBlock
	{
		get
		{
			return ExposedVariables.msgPlayerHasBlock;
		}
		set
		{
			ExposedVariables.msgPlayerHasBlock = value;
		}
	}

	public LocalisedString[] msgAttackTheEnemy
	{
		get
		{
			return ExposedVariables.msgAttackTheEnemy;
		}
		set
		{
			ExposedVariables.msgAttackTheEnemy = value;
		}
	}

	public LocalisedString[] msgEnemySpotted
	{
		get
		{
			return ExposedVariables.msgEnemySpotted;
		}
		set
		{
			ExposedVariables.msgEnemySpotted = value;
		}
	}

	public LocalisedString[] msgEnemyDroppedBlock
	{
		get
		{
			return ExposedVariables.msgEnemyDroppedBlock;
		}
		set
		{
			ExposedVariables.msgEnemyDroppedBlock = value;
		}
	}

	public LocalisedString[] msgPickUpBlock
	{
		get
		{
			return ExposedVariables.msgPickUpBlock;
		}
		set
		{
			ExposedVariables.msgPickUpBlock = value;
		}
	}

	public LocalisedString QLAttackEnemy
	{
		get
		{
			return ExposedVariables.QLAttackEnemy;
		}
		set
		{
			ExposedVariables.QLAttackEnemy = value;
		}
	}

	public LocalisedString QLAttachBlock
	{
		get
		{
			return ExposedVariables.QLAttachBlock;
		}
		set
		{
			ExposedVariables.QLAttachBlock = value;
		}
	}

	public LocalisedString[] msgAttachBlock
	{
		get
		{
			return ExposedVariables.msgAttachBlock;
		}
		set
		{
			ExposedVariables.msgAttachBlock = value;
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
