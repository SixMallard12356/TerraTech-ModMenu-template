#define UNITY_EDITOR
using System;
using System.Globalization;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleVariableMass : Module
{
	[Serializable]
	[Obsolete("Old save data", false)]
	private new class SerialData : SerialData<SerialData>
	{
		public static readonly Vector2 kOldRange = new Vector2(1f, 24f);

		public float currentFulfillment;
	}

	[SerializeField]
	protected ModuleHUDSliderControl m_MassValueSlider;

	[SerializeField]
	protected Transform m_MassDisplayObject;

	[SerializeField]
	protected MinMaxFloat m_MassDisplayObjectScaleRange;

	[SerializeField]
	protected AnimationCurve m_MassDisplayObjectTransitionCurve;

	[Tooltip("How long it takes the cube to change from one mass scale to another")]
	[SerializeField]
	protected float m_MassDisplayObjectAnimationDuration = 0.5f;

	private float m_NextMassCubeScale;

	private float m_LastLocalScaleX;

	private float m_RemainingMassCubeAnimDuration;

	public void SetMass(float newMass, bool instantRefresh = false)
	{
		base.block.SetAdditionalMassCategory(TankBlock.MassCategoryType.VariableMass, newMass);
		if (instantRefresh)
		{
			ForceGeometryToScale();
		}
	}

	private void ForceGeometryToScale()
	{
		SetGeometryScale(instant: true);
	}

	private void SetGeometryScale(bool instant = false)
	{
		float num = Mathf.Lerp(m_MassDisplayObjectScaleRange.Min, m_MassDisplayObjectScaleRange.Max, m_MassValueSlider.AdjustableValueFulfillment01);
		if (m_NextMassCubeScale != num || instant)
		{
			m_NextMassCubeScale = num;
			if (instant)
			{
				m_MassDisplayObject.transform.localScale = Vector3.one * m_NextMassCubeScale;
				m_RemainingMassCubeAnimDuration = 0f;
			}
			else
			{
				m_LastLocalScaleX = m_MassDisplayObject.transform.localScale.x;
				m_RemainingMassCubeAnimDuration = m_MassDisplayObjectAnimationDuration;
			}
		}
	}

	private void OnSlider_ValueSet()
	{
		SetMass(m_MassValueSlider.Value);
	}

	private void OnSlider_InstantRefreshSet()
	{
		SetMass(m_MassValueSlider.Value, instantRefresh: true);
	}

	private void OnMassChanged()
	{
		SetGeometryScale();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (!saving)
		{
			SerialData serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
			if (serialData != null)
			{
				m_MassValueSlider.SetValueMultiplayerSafe(SerialData.kOldRange.Lerp(serialData.currentFulfillment));
				ForceGeometryToScale();
			}
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			return;
		}
		string text = context.Retrieve(GetType(), "moduleVariableMassCurrentFulfillment");
		if (!text.NullOrEmpty())
		{
			if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
			{
				result = Mathf.Clamp01(result);
				m_MassValueSlider.SetValueMultiplayerSafe(SerialData.kOldRange.Lerp(result));
				ForceGeometryToScale();
				return;
			}
			d.LogError("ModuleVariableMass.OnSerializeText - Failed to parse CurrentFulfillment setting from save data on block '" + base.block.name + "'. Expected float value 0-1 but got '" + text + "'. Setting to default value of 0.5!");
		}
	}

	private void PrePool()
	{
		if (m_MassDisplayObjectTransitionCurve == null || m_MassDisplayObjectTransitionCurve.Equals(new AnimationCurve()))
		{
			m_MassDisplayObjectTransitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		}
	}

	private void OnPool()
	{
		m_MassValueSlider.OptionSetEvent.Subscribe(OnSlider_ValueSet);
		m_MassValueSlider.InstantRefreshEvent.Subscribe(OnSlider_InstantRefreshSet);
		base.block.MassChangedEvent.Subscribe(OnMassChanged);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.serializeEvent.Subscribe(OnSerialze);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
	}

	private void OnSpawn()
	{
		ForceGeometryToScale();
	}

	private void OnUpdate()
	{
		if (m_RemainingMassCubeAnimDuration != 0f)
		{
			m_RemainingMassCubeAnimDuration = Mathf.Max(m_RemainingMassCubeAnimDuration - Time.deltaTime, 0f);
			float time = 1f - m_RemainingMassCubeAnimDuration / m_MassDisplayObjectAnimationDuration;
			m_MassDisplayObject.transform.localScale = Vector3.one * Mathf.Lerp(m_LastLocalScaleX, m_NextMassCubeScale, m_MassDisplayObjectTransitionCurve.Evaluate(time));
		}
	}
}
