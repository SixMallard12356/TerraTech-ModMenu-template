#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Visible))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Crate : MonoBehaviour
{
	[Serializable]
	public struct ItemDefinition
	{
		public BlockTypes m_BlockType;
	}

	[Serializable]
	public struct Definition
	{
		public ItemDefinition[] m_Contents;

		public bool m_Locked;
	}

	[Serializable]
	public struct SaveData
	{
		public State m_State;

		public int m_SpawnIndex;
	}

	public enum State
	{
		Falling,
		Locked,
		Unlocking,
		Closed,
		Spawning,
		deprecated_EjectingItem,
		Exhausted,
		FadingOut
	}

	public enum AnimState
	{
		Locked,
		Closed,
		Open
	}

	[SerializeField]
	[Tooltip("Clearance radius needed when spawned to fit crate within")]
	private float m_Radius;

	[SerializeField]
	[Tooltip("Range within which the crate will open")]
	private float m_OpenTriggerRange;

	[SerializeField]
	[Tooltip("Time taken to unlock chest")]
	private float m_UnlockDelay;

	[SerializeField]
	[Tooltip("Time delay before first object spawns")]
	private float m_FirstSpawnDelay;

	[SerializeField]
	[Tooltip("Target duration between first and last spawn")]
	private float m_TargetSpawnDuration;

	[Tooltip("Maximum delay between spawns")]
	[SerializeField]
	private float m_MaxDelayBetweenSpawns;

	[Tooltip("Locators for spawning contents")]
	[SerializeField]
	private Transform[] m_ContentsLocators;

	[SerializeField]
	[Tooltip("Duration after exhausting contents that the crate hangs around for (before fade out)")]
	private float m_PauseTimeBeforeFadeOut = 5f;

	[Tooltip("How long the crate takes to fade out")]
	[SerializeField]
	private float m_FadeOutTime = 2f;

	[Tooltip("Material that we use for fading out")]
	[SerializeField]
	private Material m_FadeMatPrefab;

	[SerializeField]
	private BlockEjector m_Ejector;

	[SerializeField]
	private FactionSubTypes m_CorpType;

	private Definition m_Definition;

	private SaveData m_SaveData;

	private Rigidbody m_RigidBody;

	private Animator m_Animator;

	private float m_StateTimer;

	private bool m_UnlockQueued;

	private bool m_AutoSpawnContentsAfterUnlock;

	private float m_SpawnDelay;

	private Renderer[] m_Renderers;

	private Material m_OriginalMat;

	private Material m_FadeMatInst;

	public float Radius => m_Radius;

	public Visible visible { get; private set; }

	public Definition Def => m_Definition;

	public SaveData Save => m_SaveData;

	public FactionSubTypes CorpType
	{
		get
		{
			return m_CorpType;
		}
		set
		{
			m_CorpType = value;
		}
	}

	public bool IsLocked
	{
		get
		{
			if (m_SaveData.m_State != State.Unlocking && m_SaveData.m_State != State.Closed)
			{
				return m_Definition.m_Locked;
			}
			return false;
		}
	}

	public Rigidbody rbody => m_RigidBody;

	public float OpenTriggerRange => m_OpenTriggerRange;

	public NetCrate netCrate { get; private set; }

	public void SetDefinition(Definition def)
	{
		m_Definition = def;
		if (!def.m_Locked)
		{
			SetAnimState(AnimState.Closed);
		}
		if (def.m_Contents.Length > 1)
		{
			int num = def.m_Contents.Length - 1;
			float num2 = m_Ejector.ItemExpelTime * (float)num;
			float value = (m_TargetSpawnDuration - num2) / (float)num;
			m_SpawnDelay = Mathf.Clamp(value, 0f, m_MaxDelayBetweenSpawns);
		}
		else
		{
			m_SpawnDelay = 0f;
		}
		List<ItemDefinition> list = new List<ItemDefinition>(2);
		for (int i = 0; i < def.m_Contents.Length; i++)
		{
			if (Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(def.m_Contents[i].m_BlockType))
			{
				list.Add(def.m_Contents[i]);
			}
		}
		if (list.Count != def.m_Contents.Length)
		{
			def.m_Contents = list.ToArray();
		}
	}

	public void SetSaveData(SaveData saveData)
	{
		m_SaveData = saveData;
		if (saveData.m_State == State.deprecated_EjectingItem)
		{
			saveData.m_State = State.Spawning;
		}
		AnimState animState = ((saveData.m_State != State.Spawning && saveData.m_State != State.Exhausted) ? ((!IsLocked) ? AnimState.Closed : AnimState.Locked) : AnimState.Open);
		SetAnimState(animState);
		if (saveData.m_State == State.FadingOut)
		{
			SetupFadableMaterial();
			m_StateTimer = 0f;
		}
	}

	public void Unlock(bool autoSpawnContents = false)
	{
		m_UnlockQueued = true;
		m_AutoSpawnContentsAfterUnlock = autoSpawnContents;
	}

	public void PlayUnlockAnimation()
	{
		m_Animator.SetTrigger("Unlock");
	}

	public void PlayOpeningAnimation()
	{
		m_Animator.SetTrigger("Open");
	}

	public void PlayFadeOutAnimation()
	{
		m_SaveData.m_State = State.FadingOut;
		SetupFadableMaterial();
		m_StateTimer = 0f;
		visible.ForceAsKilled();
	}

	private void SetAnimState(AnimState animState)
	{
		int value;
		switch (animState)
		{
		case AnimState.Locked:
			value = 0;
			break;
		case AnimState.Closed:
			value = 1;
			break;
		case AnimState.Open:
			value = 2;
			break;
		default:
			d.Assert(condition: false, "Crate.SetAnimState: unhandled anim state type " + animState);
			value = 0;
			break;
		}
		m_Animator.SetInteger("InitialState", value);
		m_Animator.SetTrigger("Inited");
	}

	private void SetupFadableMaterial()
	{
		if (m_FadeMatInst != null)
		{
			m_FadeMatInst.color = m_FadeMatInst.color.SetAlpha(1f);
			_resetMaterials(m_FadeMatInst);
		}
	}

	private void _resetMaterials(Material theMat)
	{
		for (int i = 0; i < m_Renderers.Length; i++)
		{
			m_Renderers[i].sharedMaterial = theMat;
		}
	}

	private void OnVisibleRecycled(Visible vis)
	{
		if (vis.Killed)
		{
			Singleton.Manager<ManVisible>.inst.StopTrackingVisible(vis.ID);
		}
		vis.RecycledEvent.Unsubscribe(OnVisibleRecycled);
	}

	private void OnPool()
	{
		visible = GetComponent<Visible>();
		m_RigidBody = GetComponent<Rigidbody>();
		if (m_RigidBody == null)
		{
			m_RigidBody = null;
		}
		m_Animator = GetComponent<Animator>();
		netCrate = GetComponent<NetCrate>();
		m_Renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
		if (m_Renderers.Length != 0)
		{
			m_OriginalMat = m_Renderers[0].sharedMaterial;
			if (m_FadeMatPrefab != null)
			{
				Renderer renderer = m_Renderers[0];
				renderer.material = m_FadeMatPrefab;
				m_FadeMatInst = renderer.material;
				renderer.sharedMaterial = m_OriginalMat;
				m_FadeMatInst.SetFloat("_ZWrite", 1f);
			}
			else
			{
				d.LogError("ERROR: no fade material prefab set in crate " + base.name);
			}
		}
	}

	private void OnSpawn()
	{
		visible.RecycledEvent.Subscribe(OnVisibleRecycled);
		m_StateTimer = 0f;
		m_UnlockQueued = false;
		m_AutoSpawnContentsAfterUnlock = false;
		m_SaveData = new SaveData
		{
			m_State = State.Falling,
			m_SpawnIndex = 0
		};
		_resetMaterials(m_OriginalMat);
		if (m_FadeMatInst != null)
		{
			m_FadeMatInst.color = m_FadeMatInst.color.SetAlpha(1f);
		}
		SetAnimState(AnimState.Locked);
	}

	private void OnRecycle()
	{
		_resetMaterials(m_OriginalMat);
		m_Ejector.StopEjecting();
	}

	private void Update()
	{
		switch (m_SaveData.m_State)
		{
		case State.Falling:
		{
			Vector3 scenePos = visible.trans.position;
			if (Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos, out var outNormal))
			{
				visible.trans.position = scenePos;
				Vector3 forward = Vector3.Cross(visible.trans.right, outNormal);
				visible.trans.rotation = Quaternion.LookRotation(forward, outNormal);
				m_SaveData.m_State = (m_Definition.m_Locked ? State.Locked : State.Closed);
			}
			break;
		}
		case State.Locked:
			if (m_UnlockQueued)
			{
				m_SaveData.m_State = State.Unlocking;
				m_Animator.SetTrigger("Unlock");
			}
			break;
		case State.Unlocking:
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				m_SaveData.m_State = State.Closed;
				m_StateTimer = 0f;
				break;
			}
			m_StateTimer += Time.deltaTime;
			if (m_StateTimer >= m_UnlockDelay)
			{
				m_SaveData.m_State = State.Closed;
				m_StateTimer = 0f;
			}
			break;
		case State.Closed:
			if (!ManNetwork.IsHost)
			{
				break;
			}
			if (m_AutoSpawnContentsAfterUnlock)
			{
				m_SaveData.m_State = State.Spawning;
				m_StateTimer = 0f;
				m_Animator.SetTrigger("Open");
				break;
			}
			{
				foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
				{
					float radius = allPlayerTech.visible.Radius;
					Vector3 position = visible.trans.position;
					Vector3 boundsCentreWorldNoCheck = allPlayerTech.boundsCentreWorldNoCheck;
					float sqrMagnitude = (position - boundsCentreWorldNoCheck).sqrMagnitude;
					float num = m_OpenTriggerRange + radius;
					if (sqrMagnitude <= num * num)
					{
						m_SaveData.m_State = State.Spawning;
						m_StateTimer = 0f;
						m_Animator.SetTrigger("Open");
						break;
					}
				}
				break;
			}
		case State.Spawning:
			if (!m_Ejector.Ejecting)
			{
				if (m_SaveData.m_SpawnIndex < m_Definition.m_Contents.Length)
				{
					float num2 = ((m_SaveData.m_SpawnIndex <= 0) ? m_FirstSpawnDelay : m_SpawnDelay);
					m_StateTimer += Time.deltaTime;
					if (!(m_StateTimer >= num2))
					{
						break;
					}
					Transform locator = m_ContentsLocators[m_SaveData.m_SpawnIndex % m_ContentsLocators.Length];
					BlockTypes blockType = m_Definition.m_Contents[m_SaveData.m_SpawnIndex].m_BlockType;
					if (Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType))
					{
						m_Ejector.Eject(locator, blockType);
						if (ManNetwork.IsHost)
						{
							Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockType);
						}
					}
					m_SaveData.m_SpawnIndex++;
				}
				else
				{
					Singleton.Manager<ManVisible>.inst.StopTrackingVisible(visible.ID);
					m_SaveData.m_State = State.Exhausted;
					m_StateTimer = 0f;
				}
			}
			else
			{
				m_Ejector.Update();
			}
			break;
		case State.deprecated_EjectingItem:
			m_SaveData.m_State = State.Spawning;
			break;
		case State.Exhausted:
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				m_SaveData.m_State = State.FadingOut;
				SetupFadableMaterial();
				m_StateTimer = 0f;
				break;
			}
			if (m_StateTimer >= m_PauseTimeBeforeFadeOut)
			{
				m_SaveData.m_State = State.FadingOut;
				SetupFadableMaterial();
				m_StateTimer = 0f;
			}
			m_StateTimer += Time.deltaTime;
			break;
		case State.FadingOut:
			if (m_StateTimer >= m_FadeOutTime)
			{
				if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					if (Singleton.Manager<ManNetwork>.inst.IsServer)
					{
						NetworkServer.UnSpawn(base.gameObject);
					}
					visible.RemoveFromGame();
				}
			}
			else if (m_FadeMatInst != null)
			{
				float alpha = 1f - m_StateTimer / Mathf.Max(m_FadeOutTime, Mathf.Epsilon);
				m_FadeMatInst.color = m_FadeMatInst.color.SetAlpha(alpha);
			}
			m_StateTimer += Time.deltaTime;
			break;
		default:
			d.Assert(condition: false, "Unhandled crate state: " + m_SaveData.m_State);
			m_SaveData.m_State = State.Exhausted;
			m_StateTimer = 0f;
			break;
		}
	}
}
