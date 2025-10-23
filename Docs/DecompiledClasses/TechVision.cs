using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechVision : TechComponent
{
	public struct VisibleIterator
	{
		private int m_Index;

		private List<Visible.ConeFiltered> m_Visibles;

		private Bitfield<ObjectTypes> m_Types;

		public Visible Current => m_Visibles[m_Index].visible;

		public VisibleIterator(List<Visible.ConeFiltered> visibles, Bitfield<ObjectTypes> types)
		{
			m_Visibles = visibles;
			m_Types = types;
			m_Index = -1;
		}

		public VisibleIterator GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			if (m_Types.IsNull)
			{
				Visible current;
				do
				{
					m_Index++;
					if (m_Index == m_Visibles.Count)
					{
						return false;
					}
					current = Current;
				}
				while (current == null || !current.isActive);
			}
			else
			{
				Visible current;
				do
				{
					m_Index++;
					if (m_Index == m_Visibles.Count)
					{
						return false;
					}
					current = Current;
				}
				while (current == null || !current.isActive || !m_Types.Contains((int)current.type));
			}
			return true;
		}
	}

	public float m_RefreshInterval = 0.5f;

	public float DbgRONumVisible;

	private List<ModuleVision> m_VisionModules = new List<ModuleVision>(10);

	private Vector3 m_SearchEpicentre;

	private float m_SearchRadius;

	private bool m_SearchSphereNeedsRecalc;

	private float m_UpdateVisionTimeout;

	private float m_UpdateClosestEnemyTimeout;

	private Visible.WeakReference m_ClosestEnemy = new Visible.WeakReference();

	private List<Visible.ConeFiltered> m_Visibles = new List<Visible.ConeFiltered>();

	private static Color[] s_ColorWheel = new Color[6]
	{
		Color.cyan,
		Color.magenta,
		Color.yellow,
		Color.red,
		Color.green,
		Color.blue
	};

	public int VisibleCount => m_Visibles.Count;

	public float SearchRadius => m_SearchRadius;

	public void AddVision(ModuleVision vision)
	{
		m_VisionModules.Add(vision);
		m_SearchSphereNeedsRecalc = true;
	}

	public void RemoveVision(ModuleVision vision)
	{
		m_VisionModules.Remove(vision);
		m_SearchSphereNeedsRecalc = true;
	}

	public VisibleIterator IterateVisibles()
	{
		RefreshState();
		return new VisibleIterator(m_Visibles, Bitfield<ObjectTypes>.Null);
	}

	public VisibleIterator IterateVisibles(ObjectTypes type)
	{
		RefreshState();
		return new VisibleIterator(m_Visibles, (type == ObjectTypes.Null) ? Bitfield<ObjectTypes>.Null : new Bitfield<ObjectTypes>((int)type));
	}

	public VisibleIterator IterateVisibles(Bitfield<ObjectTypes> types)
	{
		RefreshState();
		return new VisibleIterator(m_Visibles, types);
	}

	public void SetClosestEnemy(Visible targetTech)
	{
		m_ClosestEnemy.Set(targetTech);
	}

	public Visible GetFirstVisibleTechIsEnemy(int team)
	{
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			flag = !(base.Tech.netTech == null) && ((!(base.Tech.netTech.NetPlayer == null)) ? (base.Tech.netTech.NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer) : ManNetwork.IsHost);
		}
		if (flag && Time.time > m_UpdateClosestEnemyTimeout)
		{
			m_UpdateClosestEnemyTimeout = Time.time + m_RefreshInterval;
			if (m_SearchSphereNeedsRecalc)
			{
				m_SearchSphereNeedsRecalc = false;
				RecalculateSearchSphere();
			}
			Vector3 vector = base.Tech.trans.TransformPoint(m_SearchEpicentre);
			Visible visible = null;
			float num = 0f;
			TileManager.VisibleIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Vehicle, vector, m_SearchRadius).GetEnumerator();
			while (enumerator.MoveNext())
			{
				Tank tank = enumerator.Current.tank;
				if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null)
				{
					NetTech notableTech = Singleton.Manager<ManNetwork>.inst.NetController.GetNotableTech();
					if (notableTech != null && notableTech.tech == tank && tank.IsEnemy(team) && notableTech.InitialSpawnShieldID == 0)
					{
						visible = notableTech.tech.visible;
						break;
					}
				}
				if (tank != null && tank.IsEnemy(team) && tank.ShouldShowOverlay)
				{
					float sqrMagnitude = (vector - tank.trans.position).sqrMagnitude;
					if (sqrMagnitude < m_SearchRadius * m_SearchRadius && (visible == null || sqrMagnitude < num) && AnyModuleCanSee(tank.visible, out var _))
					{
						visible = tank.visible;
						num = sqrMagnitude;
					}
				}
			}
			m_ClosestEnemy.Set(visible);
		}
		return m_ClosestEnemy.Get();
	}

	public Visible GetFirstVisible()
	{
		RefreshState();
		if (m_Visibles.Count != 0)
		{
			return m_Visibles[0].visible;
		}
		return null;
	}

	public bool CanSee(Visible item)
	{
		float distSq;
		return AnyModuleCanSee(item, out distSq);
	}

	private void RecalculateSearchSphere()
	{
		m_SearchEpicentre = Vector3.zero;
		foreach (ModuleVision visionModule in m_VisionModules)
		{
			m_SearchEpicentre += visionModule.block.centreOfMassWorld;
		}
		if (m_VisionModules.Count > 1)
		{
			m_SearchEpicentre /= (float)m_VisionModules.Count;
		}
		m_SearchRadius = 0f;
		foreach (ModuleVision visionModule2 in m_VisionModules)
		{
			float b = (visionModule2.block.centreOfMassWorld - m_SearchEpicentre).magnitude + visionModule2.Range;
			m_SearchRadius = Mathf.Max(m_SearchRadius, b);
		}
		m_SearchEpicentre = base.Tech.trans.InverseTransformPoint(m_SearchEpicentre);
	}

	private void RebuildVisibleList()
	{
		m_Visibles.Clear();
		Vector3 scenePos = base.Tech.trans.TransformPoint(m_SearchEpicentre);
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(scenePos, m_SearchRadius, new Bitfield<ObjectTypes>()))
		{
			if ((object)item != base.Tech.visible && (item.type != ObjectTypes.Block || !(item.block.tank != null)) && AnyModuleCanSee(item, out var distSq))
			{
				m_Visibles.Add(new Visible.ConeFiltered
				{
					visible = item,
					distSq = distSq
				});
			}
		}
		DbgRONumVisible = m_Visibles.Count;
		m_Visibles.Sort((Visible.ConeFiltered a, Visible.ConeFiltered b) => (int)(a.distSq * 1000f - b.distSq * 1000f));
	}

	private void RefreshState()
	{
		if (m_SearchSphereNeedsRecalc)
		{
			m_SearchSphereNeedsRecalc = false;
			RecalculateSearchSphere();
		}
		if (Time.time > m_UpdateVisionTimeout)
		{
			m_UpdateVisionTimeout += m_RefreshInterval;
			RebuildVisibleList();
		}
	}

	private bool AnyModuleCanSee(Visible item, out float distSq)
	{
		if (item.tank != null && item.tank.netTech != null && item.tank.netTech.InitialSpawnShieldID != 0)
		{
			distSq = 0f;
			return false;
		}
		for (int i = 0; i < m_VisionModules.Count; i++)
		{
			if (m_VisionModules[i].CanSee(item, out distSq))
			{
				return true;
			}
		}
		distSq = 0f;
		return false;
	}

	private void OnSpawn()
	{
		m_VisionModules.Clear();
		m_Visibles.Clear();
		m_SearchRadius = 0f;
		m_SearchSphereNeedsRecalc = false;
		m_UpdateVisionTimeout = Time.time + Random.value * m_RefreshInterval;
		m_UpdateClosestEnemyTimeout = Time.time + Random.value * m_RefreshInterval;
	}

	private void OnRecycle()
	{
		m_ClosestEnemy.Set(null);
	}

	private void OnDrawGizmosSelected()
	{
		if (!Application.IsPlaying(base.gameObject))
		{
			return;
		}
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.Tech.trans.TransformPoint(m_SearchEpicentre), m_SearchRadius);
		Gizmos.color = Color.cyan;
		foreach (ModuleVision visionModule in m_VisionModules)
		{
			Gizmos.DrawWireSphere(visionModule.block.centreOfMassWorld, visionModule.Range);
		}
		foreach (Visible.ConeFiltered item in m_Visibles.Where((Visible.ConeFiltered i) => i.visible.isActive))
		{
			Gizmos.color = s_ColorWheel[(int)item.visible.type % s_ColorWheel.Length];
			Gizmos.DrawRay(base.Tech.boundsCentreWorld, (item.visible.centrePosition - base.Tech.boundsCentreWorld).normalized * 3f);
		}
	}
}
