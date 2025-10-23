#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechWeapon : TechComponent
{
	[Serializable]
	public struct ManualTargetingSettings
	{
		public float m_ManualTargetingRadiusSP;

		public float m_ManualTargetingRadiusMP;

		public float m_ManualTargetingFieldOfView;

		public bool m_ToggleClearTarget;

		public bool m_RetargetOnLoss;

		public bool m_CursorTargeting;

		public bool m_AllowTargetingNotInFieldOfView;
	}

	private struct SortedVisible
	{
		public Visible visible;

		public float sortKey;
	}

	public enum ManualTargetingReticuleState
	{
		NotTargeted = 4,
		PotentialTarget = 1,
		Targeted = 2,
		PendingUntarget = 3
	}

	private enum ManualTargetSpace
	{
		Radius,
		Cone,
		Cursor
	}

	public struct FiredBarrel
	{
		public uint m_BlockPoolID;

		public byte m_BarrelIndex;

		public Vector3 m_FireDirection;

		public Vector3 m_FireSpin;

		public byte m_SeekingRounds;

		public int m_ProjectileID;
	}

	public struct State
	{
		public List<FiredBarrel> FiredBarrels;

		public void Append(State other)
		{
			if (FiredBarrels == null)
			{
				FiredBarrels = new List<FiredBarrel>(other.FiredBarrels);
			}
			else
			{
				FiredBarrels.AddRange(other.FiredBarrels);
			}
		}

		public void NetSerialize(NetworkWriter writer)
		{
			int num = ((FiredBarrels != null) ? FiredBarrels.Count : 0);
			writer.Write(num);
			if (num > 0)
			{
				for (int i = 0; i < FiredBarrels.Count; i++)
				{
					writer.Write(FiredBarrels[i].m_BlockPoolID);
					writer.Write(FiredBarrels[i].m_BarrelIndex);
					writer.Write(FiredBarrels[i].m_FireDirection);
					writer.Write(FiredBarrels[i].m_FireSpin);
					writer.Write(FiredBarrels[i].m_SeekingRounds);
					writer.WritePackedInt32(FiredBarrels[i].m_ProjectileID);
				}
			}
		}

		public void NetDeserialize(NetworkReader reader)
		{
			int num = reader.ReadInt32();
			if (num <= 0)
			{
				return;
			}
			if (FiredBarrels == null)
			{
				FiredBarrels = new List<FiredBarrel>(num);
			}
			else
			{
				if (num > FiredBarrels.Capacity)
				{
					FiredBarrels.Capacity = num;
				}
				FiredBarrels.Clear();
			}
			FiredBarrel item = default(FiredBarrel);
			for (int i = 0; i < num; i++)
			{
				item.m_BlockPoolID = reader.ReadUInt32();
				item.m_BarrelIndex = reader.ReadByte();
				item.m_FireDirection = reader.ReadVector3();
				item.m_FireSpin = reader.ReadVector3();
				item.m_SeekingRounds = reader.ReadByte();
				item.m_ProjectileID = reader.ReadPackedInt32();
				FiredBarrels.Add(item);
			}
		}
	}

	public interface IMeleeWeapon
	{
		bool IsActive { get; }
	}

	[SerializeField]
	public ManualTargetingSettings m_ManualTargetingSettingsMAndKB;

	[SerializeField]
	public ManualTargetingSettings m_ManualTargetingSettingsGamepad;

	public EventNoParams WeaponsFiredEvent;

	private List<ModuleWeapon> m_WeaponModules = new List<ModuleWeapon>();

	private List<IMeleeWeapon> m_MeleeModules = new List<IMeleeWeapon>();

	private TrackedVisible m_ManualTarget;

	private Visible m_NextManualTarget;

	private List<SortedVisible> m_SortedVisibles = new List<SortedVisible>();

	private List<SortedVisible> m_enemiesWithinRadius = new List<SortedVisible>();

	private Vector3 m_CameraForward2D;

	private Vector3 m_CameraRight2D;

	private List<FiredBarrel> m_FiredQueueSending = new List<FiredBarrel>();

	private List<FiredBarrel> m_FiredQueueReceiving = new List<FiredBarrel>();

	public ManualTargetingSettings ManualTargetingSetting
	{
		get
		{
			if (!Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				return m_ManualTargetingSettingsMAndKB;
			}
			return m_ManualTargetingSettingsGamepad;
		}
	}

	public int WeaponCount => m_WeaponModules.Count;

	public int MeleeWeaponCount => m_MeleeModules.Count;

	private float ManualTargetingRadius
	{
		get
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return ManualTargetingSetting.m_ManualTargetingRadiusSP;
			}
			return ManualTargetingSetting.m_ManualTargetingRadiusMP;
		}
	}

	public void AddWeapon(ModuleWeapon weapon)
	{
		m_WeaponModules.Add(weapon);
	}

	public void RemoveWeapon(ModuleWeapon weapon)
	{
		m_WeaponModules.Remove(weapon);
	}

	public void RegisterMelee(IMeleeWeapon m)
	{
		m_MeleeModules.Add(m);
	}

	public void UnregisterMelee(IMeleeWeapon m)
	{
		m_MeleeModules.Remove(m);
	}

	public ModuleWeapon GetFirstWeapon()
	{
		ModuleWeapon result = null;
		if (m_WeaponModules.Count > 0)
		{
			result = m_WeaponModules[0];
		}
		return result;
	}

	public Visible GetManualTarget()
	{
		if (m_ManualTarget == null)
		{
			return null;
		}
		return m_ManualTarget.visible;
	}

	public ManualTargetingReticuleState GetManualTargetingReticuleState(Visible visible)
	{
		bool flag = m_ManualTarget != null && m_ManualTarget.visible != null && visible == m_ManualTarget.visible;
		bool flag2 = m_NextManualTarget != null && visible == m_NextManualTarget;
		ManualTargetingReticuleState result = ManualTargetingReticuleState.NotTargeted;
		if (flag && flag2)
		{
			result = ((!(visible == m_NextManualTarget || flag)) ? ManualTargetingReticuleState.PotentialTarget : ManualTargetingReticuleState.PendingUntarget);
		}
		else if (flag)
		{
			result = ManualTargetingReticuleState.Targeted;
		}
		else if (flag2)
		{
			result = ManualTargetingReticuleState.PotentialTarget;
		}
		return result;
	}

	private float CameraAngleDot2D(Visible visible)
	{
		Vector3 normalized = m_CameraForward2D.normalized;
		Vector3 vector = base.Tech.trans.position.SetY(0f);
		Vector3 vector2 = visible.trans.position.SetY(0f);
		Vector3 vector3 = visible.Radius * m_CameraRight2D;
		return Math.Max(Vector3.Dot(normalized, (vector2 - vector3 - vector).normalized), Vector3.Dot(normalized, (vector2 + vector3 - vector).normalized));
	}

	private void FindEnemyVehiclesInRadius(List<SortedVisible> visibles)
	{
		Vector3 position = base.Tech.trans.position;
		TileManager.VisibleIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Vehicle, position, ManualTargetingRadius).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			if (current != null && current.tank.IsEnemy() && IsWithinRange(current) && current.tank.ShouldShowOverlay)
			{
				visibles.Add(new SortedVisible
				{
					visible = current
				});
			}
		}
	}

	private void SortVisiblesByCameraAngleDot2D(List<SortedVisible> sortedVisibles)
	{
		for (int num = sortedVisibles.Count - 1; num >= 0; num--)
		{
			SortedVisible value = sortedVisibles[num];
			Visible visible = value.visible;
			value.sortKey = CameraAngleDot2D(visible);
			sortedVisibles[num] = value;
		}
		sortedVisibles.Sort((SortedVisible v1, SortedVisible v2) => v2.sortKey.CompareTo(v1.sortKey));
	}

	private void SortVisiblesByScreenSpaceDistanceToCursor(List<SortedVisible> sortedVisibles)
	{
		for (int num = sortedVisibles.Count - 1; num >= 0; num--)
		{
			SortedVisible value = sortedVisibles[num];
			Visible visible = value.visible;
			value.sortKey = (Input.mousePosition - Singleton.Manager<ManUI>.inst.WorldToScreenPoint(visible.trans.position)).sqrMagnitude;
			sortedVisibles[num] = value;
		}
		sortedVisibles.Sort((SortedVisible v1, SortedVisible v2) => (int)(v1.sortKey - v2.sortKey));
	}

	private void FilterVisiblesByFieldOfView(List<SortedVisible> sortedVisibles)
	{
		float num = Mathf.Cos((float)Math.PI / 180f * ManualTargetingSetting.m_ManualTargetingFieldOfView * 0.5f);
		for (int num2 = sortedVisibles.Count - 1; num2 >= 0; num2--)
		{
			SortedVisible value = sortedVisibles[num2];
			Visible visible = value.visible;
			float num3 = CameraAngleDot2D(visible);
			if (!ManualTargetingSetting.m_AllowTargetingNotInFieldOfView)
			{
				if (num3 < num)
				{
					sortedVisibles.RemoveAt(num2);
				}
			}
			else
			{
				value.sortKey = num3;
				sortedVisibles[num2] = value;
			}
		}
		sortedVisibles.Sort((SortedVisible v1, SortedVisible v2) => v2.sortKey.CompareTo(v1.sortKey));
	}

	public bool ShouldShowHint()
	{
		if (!Singleton.Manager<DebugUtil>.inst.m_Settings.m_DisableManualTargeting)
		{
			m_enemiesWithinRadius.Clear();
			FindEnemyVehiclesInRadius(m_enemiesWithinRadius);
			return m_enemiesWithinRadius.Count > 1;
		}
		return false;
	}

	public void QueueProjectileLaunch(CannonBarrel barrel, int shortlivedUID, Vector3 fireDirection, Vector3 fireSpin, ModuleWeapon weapon, bool seekingRounds = false)
	{
		if (barrel != null)
		{
			int num = -1;
			ModuleWeaponGun component = weapon.GetComponent<ModuleWeaponGun>();
			d.Assert(component != null, "TechWeapon.QueueProjectileLaunch: weapon module not of component type ModuleWeaponGun");
			if (component != null)
			{
				num = component.GetCannonBarrelIndex(barrel);
				d.Assert(num != -1, "TechWeapon.QueueProjectileLaunch: cannon barrel not found");
				d.Assert(weapon.block.HasValidBlockPoolID(), "TechWeapon.QueueProjectileLaunch: Cannon block: " + weapon.block.name + " has an invalid block pool id=" + weapon.block.blockPoolID);
				FiredBarrel item = new FiredBarrel
				{
					m_BlockPoolID = weapon.block.blockPoolID,
					m_BarrelIndex = (byte)num,
					m_FireDirection = fireDirection,
					m_FireSpin = fireSpin,
					m_SeekingRounds = (byte)(seekingRounds ? 1 : 0),
					m_ProjectileID = shortlivedUID
				};
				m_FiredQueueSending.Add(item);
			}
		}
	}

	public void QueueIncomingProjectileLaunch(FiredBarrel firedBarrel)
	{
		m_FiredQueueReceiving.Add(firedBarrel);
	}

	public bool HasQueuedProjectiles()
	{
		return m_FiredQueueSending.Count > 0;
	}

	public List<FiredBarrel> GetQueuedProjectiles()
	{
		return m_FiredQueueSending;
	}

	public void ClearQueuedProjectiles()
	{
		m_FiredQueueSending.Clear();
	}

	private Visible GetPotentialManualTarget(ManualTargetSpace space)
	{
		m_SortedVisibles.Clear();
		FindEnemyVehiclesInRadius(m_SortedVisibles);
		if (space == ManualTargetSpace.Radius || space == ManualTargetSpace.Cone)
		{
			SortVisiblesByCameraAngleDot2D(m_SortedVisibles);
		}
		if (space == ManualTargetSpace.Cone)
		{
			FilterVisiblesByFieldOfView(m_SortedVisibles);
		}
		if (space == ManualTargetSpace.Cursor)
		{
			SortVisiblesByScreenSpaceDistanceToCursor(m_SortedVisibles);
		}
		if (m_SortedVisibles.Count <= 0)
		{
			return null;
		}
		return m_SortedVisibles[0].visible;
	}

	private bool IsWithinRange(Visible visible)
	{
		float sqrMagnitude = (base.Tech.transform.position - visible.transform.position).sqrMagnitude;
		float manualTargetingRadius = ManualTargetingRadius;
		float num = manualTargetingRadius * manualTargetingRadius;
		bool flag = sqrMagnitude <= num;
		if (!flag)
		{
			return flag;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && visible.tank != null)
		{
			NetTech netTech = visible.tank.netTech;
			if (netTech != null && netTech.InitialSpawnShieldID != 0)
			{
				flag = false;
			}
		}
		return flag;
	}

	private void UpdateManualTarget()
	{
		d.Assert(base.Tech.IsPlayer);
		if (m_ManualTarget == null)
		{
			return;
		}
		Visible visible = m_ManualTarget.visible;
		if (!visible || visible.Killed || !IsWithinRange(visible) || !visible.tank.IsEnemy() || !visible.tank.ShouldShowOverlay)
		{
			m_ManualTarget = null;
			if (ManualTargetingSetting.m_RetargetOnLoss)
			{
				ManualTargetingAction();
			}
			else
			{
				OnManualTargetLost();
			}
		}
	}

	private Visible GetPotentialTarget()
	{
		if (ManualTargetingSetting.m_CursorTargeting)
		{
			return GetPotentialCursorTarget();
		}
		return GetPotentialManualTarget((m_ManualTarget == null) ? ManualTargetSpace.Cone : ManualTargetSpace.Radius);
	}

	private void OnManualTargetAcquired()
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.TargetAcquired);
	}

	private void OnManualTargetLost()
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.TargetLost);
	}

	private void ManualTargetingAction()
	{
		m_NextManualTarget = null;
		if (ManualTargetingSetting.m_ToggleClearTarget && m_ManualTarget != null)
		{
			m_ManualTarget = null;
			OnManualTargetLost();
			return;
		}
		Visible potentialTarget = GetPotentialTarget();
		if ((bool)potentialTarget)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(potentialTarget.ID);
			if (trackedVisible == m_ManualTarget)
			{
				m_ManualTarget = null;
				OnManualTargetLost();
			}
			else
			{
				m_ManualTarget = trackedVisible;
				OnManualTargetAcquired();
			}
		}
		else if (m_ManualTarget != null)
		{
			m_ManualTarget = null;
			OnManualTargetLost();
		}
	}

	private Visible GetPotentialCursorTarget()
	{
		Visible result = null;
		Tank targetTank = Singleton.Manager<ManPointer>.inst.targetTank;
		if ((bool)targetTank && targetTank.IsEnemy() && IsWithinRange(targetTank.visible) && targetTank.ShouldShowOverlay)
		{
			result = targetTank.visible;
		}
		else
		{
			List<SortedVisible> list = new List<SortedVisible>();
			FindEnemyVehiclesInRadius(list);
			SortVisiblesByScreenSpaceDistanceToCursor(list);
			if (list.Count > 0)
			{
				result = list[0].visible;
			}
		}
		return result;
	}

	private void UpdateManualTargetting()
	{
		d.Assert(base.Tech.IsPlayer);
		Transform cameraTrans = Singleton.cameraTrans;
		m_CameraForward2D = cameraTrans.forward.SetY(0f).normalized;
		m_CameraRight2D = cameraTrans.right.SetY(0f).normalized;
		if (m_ManualTarget != null)
		{
			UpdateManualTarget();
		}
	}

	public void UpdateNextPotentialTarget()
	{
		m_NextManualTarget = GetPotentialTarget();
	}

	private void UpdateRemoteProjectiles()
	{
		while (m_FiredQueueReceiving.Count > 0)
		{
			FiredBarrel firedBarrel = m_FiredQueueReceiving[0];
			bool flag = false;
			foreach (ModuleWeapon weaponModule in m_WeaponModules)
			{
				if (weaponModule.block.blockPoolID == firedBarrel.m_BlockPoolID)
				{
					ModuleWeaponGun component = weaponModule.GetComponent<ModuleWeaponGun>();
					d.Assert(component != null, "TechWeapon.UpdateRemoteProjectiles: weapon module not of component type ModuleWeaponGun");
					if (component != null && firedBarrel.m_BarrelIndex < component.GetNumCannonBarrels())
					{
						component.FindCannonBarrelFromIndex(firedBarrel.m_BarrelIndex).OnClientFire(firedBarrel.m_FireDirection, firedBarrel.m_FireSpin, firedBarrel.m_SeekingRounds != 0, firedBarrel.m_ProjectileID);
						weaponModule.AddRemoteShotFired();
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				d.LogError("TechWeapon.UpdateRemoteProjectiles: Client weapon block pool id not found, BlockPoolID=" + firedBarrel.m_BlockPoolID + " Name=" + base.name);
			}
			m_FiredQueueReceiving.RemoveAt(0);
		}
	}

	private void OnPool()
	{
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_FiredQueueSending.Clear();
		m_FiredQueueReceiving.Clear();
	}

	private void OnRecycle()
	{
		m_ManualTarget = null;
		m_NextManualTarget = null;
	}

	private void OnDepool()
	{
		WeaponsFiredEvent.EnsureNoSubscribers();
	}

	public void OnManualTargetingEvent()
	{
		if (!Singleton.Manager<DebugUtil>.inst.m_Settings.m_DisableManualTargeting)
		{
			ManualTargetingAction();
		}
	}

	private void OnUpdate()
	{
		bool flag = false;
		foreach (ModuleWeapon weaponModule in m_WeaponModules)
		{
			if (weaponModule.Process() > 0)
			{
				flag = true;
			}
		}
		if (base.Tech.IsPlayer && !Singleton.Manager<DebugUtil>.inst.m_Settings.m_DisableManualTargeting)
		{
			UpdateManualTargetting();
		}
		UpdateRemoteProjectiles();
		if (!flag)
		{
			foreach (IMeleeWeapon meleeModule in m_MeleeModules)
			{
				if (meleeModule.IsActive)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			WeaponsFiredEvent.Send();
		}
	}
}
