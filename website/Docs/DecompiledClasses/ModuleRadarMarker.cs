#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleAnimator))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleRadarMarker : Module
{
	[SerializeField]
	[Tooltip("Set the mesh that will be replaced at runtime with the appropriate flag icon")]
	private MeshFilter IconMeshFilter;

	[SerializeField]
	[Tooltip("Set the mesh renderers that should have their colours changed on a player flag update")]
	private MeshRenderer[] RecolorMeshRenderers = new MeshRenderer[0];

	private List<Material> m_RecolorMeshMats = new List<Material>();

	private RadarMarker m_DisplayedMarker = RadarMarker.DefaultMarker;

	private ModuleAnimator m_AnimatorController;

	private AnimatorBool m_IsUsedAnimBool = new AnimatorBool("RadarMarkerUsed");

	public RadarMarker DisplayedMarker => m_DisplayedMarker;

	public void RefreshRadarMarkerDisplay()
	{
		SetRadarMarkerToDisplay(m_DisplayedMarker);
	}

	public void SetRadarMarkerToDisplay(RadarMarker radarMarkerToDisplay)
	{
		m_DisplayedMarker = radarMarkerToDisplay;
		Color radarMarkerColor = Singleton.Manager<ManRadar>.inst.GetRadarMarkerColor(radarMarkerToDisplay.Color, adjustForHDR: true);
		foreach (Material recolorMeshMat in m_RecolorMeshMats)
		{
			recolorMeshMat.SetColor("_TintColor", radarMarkerColor);
		}
		if (IconMeshFilter != null)
		{
			IconMeshFilter.mesh = Singleton.Manager<ManRadar>.inst.GetRadarMarkerMesh(radarMarkerToDisplay.Icon);
		}
		SetActive(m_DisplayedMarker.IsUsed);
	}

	private void SetActive(bool showMarker)
	{
		m_DisplayedMarker.IsUsed = showMarker;
		if (m_AnimatorController != null)
		{
			m_AnimatorController.Set(m_IsUsedAnimBool, m_DisplayedMarker.IsUsed);
		}
	}

	private void OnAttached()
	{
		base.block.tank.RadarMarker.AddRadarMarkerModule(this);
	}

	private void OnDetaching()
	{
		base.block.tank.RadarMarker.RemoveRadarMarkerModule(this);
		SetActive(showMarker: false);
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(this, "IconID", (int)m_DisplayedMarker.Icon);
			context.Store(this, "ColourID", (int)m_DisplayedMarker.Color);
			return;
		}
		int intVal;
		bool num = context.TryRetrieve(this, "IconID", out intVal, 0);
		int intVal2;
		bool flag = context.TryRetrieve(this, "ColourID", out intVal2, 0);
		d.AssertFormat(num && flag, this, "Unable to load radar marker icon or colour from savedata! {0}", base.name);
		SetRadarMarkerToDisplay(new RadarMarker((ManRadar.IconType)intVal, (ManRadar.RadarMarkerColorType)intVal2, DisplayedMarker.IsUsed));
	}

	private void OnGraphicOptionChanged(CameraManager.GraphicOption option, bool value)
	{
		if (option == CameraManager.GraphicOption.HDR)
		{
			RefreshRadarMarkerDisplay();
		}
	}

	private void OnPool()
	{
		m_AnimatorController = GetComponent<ModuleAnimator>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		for (int i = 0; i < RecolorMeshRenderers.Length; i++)
		{
			for (int j = 0; j < RecolorMeshRenderers[i].materials.Length; j++)
			{
				RecolorMeshRenderers[i].materials[j] = Object.Instantiate(RecolorMeshRenderers[i].materials[j]);
				m_RecolorMeshMats.Add(RecolorMeshRenderers[i].materials[j]);
			}
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<CameraManager>.inst.OnGraphicOptionChanged.Subscribe(OnGraphicOptionChanged);
		m_DisplayedMarker = RadarMarker.DefaultMarker_Disabled;
	}

	private void OnRecycle()
	{
		Singleton.Manager<CameraManager>.inst.OnGraphicOptionChanged.Unsubscribe(OnGraphicOptionChanged);
	}
}
