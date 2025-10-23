using UnityEngine;

public class uScript_SpawnBaseBomb : uScriptLogic
{
	private bool m_Spawned;

	private bool m_AnimStarted;

	private bool m_Delivered;

	private bool m_UsingDOF;

	private Transform m_BombTrans;

	private Animator m_Animator;

	private Transform m_PrevParent;

	public bool Delivered => m_Delivered;

	public bool PlayingOrFinished
	{
		get
		{
			if (Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating && m_Spawned)
			{
				return m_Delivered;
			}
			return true;
		}
	}

	public void In(Transform prefab, Transform explosionPrefab, string dontDisableMeshParent = "LogoTerraTech")
	{
		if (Mode<ModeMain>.inst.DebugSkipTutorial)
		{
			if (m_Spawned)
			{
				m_Animator.StopPlayback();
				Finish(dontDisableMeshParent, explosionPrefab, spawnExplosion: false);
				m_BombTrans.Recycle();
			}
			m_Delivered = true;
			return;
		}
		if (!m_Spawned)
		{
			m_BombTrans = prefab.Spawn();
			m_BombTrans.position = Mode<ModeMain>.inst.StartPositionScene + Mode<ModeMain>.inst.m_GameStartPosOffset;
			MeshRenderer[] componentsInChildren = m_BombTrans.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
			m_Animator = m_BombTrans.GetComponentInChildren<Animator>();
			Transform transform = m_BombTrans.Find("Camera_Position_1/Camera_Position_2");
			if (transform != null)
			{
				Singleton.cameraTrans.position = transform.position;
				Singleton.cameraTrans.rotation = transform.rotation;
				m_PrevParent = Singleton.cameraTrans.parent;
				Singleton.cameraTrans.parent = transform;
			}
			Singleton.Manager<ManMusic>.inst.EnableSequencing = false;
			Singleton.Manager<ManMusic>.inst.FadeDownAll();
			m_UsingDOF = true;
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			if (currentUser != null)
			{
				m_UsingDOF = currentUser.m_GraphicsSettings.m_DOF;
			}
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: false);
			m_Spawned = true;
		}
		if (!Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating && !m_AnimStarted)
		{
			m_Animator.SetTrigger("Start");
			m_AnimStarted = true;
		}
		if (m_AnimStarted && !m_Delivered && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
		{
			Finish(dontDisableMeshParent, explosionPrefab);
		}
	}

	private void Finish(string meshParentIgnore, Transform explosionPrefab, bool spawnExplosion = true)
	{
		Singleton.cameraTrans.parent = m_PrevParent;
		Singleton.Manager<CameraManager>.inst.ResetCamera(Singleton.cameraTrans.position, Singleton.cameraTrans.rotation);
		MeshRenderer[] componentsInChildren = m_BombTrans.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			bool flag = true;
			Transform transform = meshRenderer.transform;
			while (transform != null)
			{
				if (transform.name.EqualsNoCase(meshParentIgnore))
				{
					flag = false;
					break;
				}
				transform = transform.parent;
			}
			if (flag)
			{
				meshRenderer.enabled = false;
			}
		}
		ParticleRecycler component = m_BombTrans.GetComponent<ParticleRecycler>();
		if ((bool)component)
		{
			component.StopEmitting(stopImmediately: false);
		}
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, m_UsingDOF);
		if (spawnExplosion)
		{
			Transform transform2 = explosionPrefab.Spawn(m_BombTrans.position);
			Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance((transform2.position - Singleton.cameraTrans.position).magnitude);
		}
		m_Delivered = true;
	}

	public void OnDisable()
	{
		m_Spawned = false;
		m_AnimStarted = false;
		m_Delivered = false;
		m_UsingDOF = true;
		m_BombTrans = null;
		m_Animator = null;
		m_PrevParent = null;
	}
}
