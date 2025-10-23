#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ModuleHUDContextControl_ColorPickerField : ModuleHUDContextControlField, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public byte CurrentOptionIndex;

		public int CurrentOptionsBitfield;

		public int SaveDataVer;
	}

	[Header("General")]
	[Tooltip("Allow the player to select multiple options")]
	[SerializeField]
	protected bool m_MultiselectEnabled;

	[Header("Options")]
	[SerializeField]
	protected UIBlockContext_IconSelectionField.Option[] m_SelectionFieldOptions;

	[SerializeField]
	protected int m_DefaultSelectionBitflags = 1;

	[Header("Block")]
	[SerializeField]
	protected Renderer[] m_ColorSwatchRenderers = new Renderer[0];

	private const byte k_DefaultOptionIndex = 0;

	private const float k_DefaultSwatchIntensity = 0.6f;

	private NetworkedProperty<IntParamBlockMessage> m_ChoiceValueProp;

	public int CurrentOptionsBitfield { get; private set; }

	public UIBlockContext_IconSelectionField.Option CurrentOption => CurrentOptions.FirstOrDefault();

	public int FirstCurrentOptionIndex { get; private set; } = -1;

	public UIBlockContext_IconSelectionField.Option[] CurrentOptions { get; private set; }

	public UIBlockContext_IconSelectionField.Option[] SelectionFieldOptions => m_SelectionFieldOptions;

	public bool MultiselectEnabled => m_MultiselectEnabled;

	public override Type BlockContextFieldType => typeof(UIBlockContext_IconSelectionField);

	public bool IsOptionSelected(int optionIndex, int overrideBitfield = -1)
	{
		return ((1 << optionIndex) & ((overrideBitfield == -1) ? CurrentOptionsBitfield : overrideBitfield)) != 0;
	}

	public void CalculateSelectionBitfieldFromOptionSelection(byte optionIndex, ref int bitfield)
	{
		if (m_MultiselectEnabled)
		{
			int num = 1 << (int)optionIndex;
			if ((bitfield & num) != 0)
			{
				bitfield &= ~num;
			}
			else
			{
				bitfield |= num;
			}
		}
		else
		{
			bitfield = 1 << (int)optionIndex;
		}
	}

	public void SetOptionMultiplayerSafe(int bitfield)
	{
		SetOptionLocally(bitfield);
		m_ChoiceValueProp.Sync();
	}

	private void SetOptionLocally(int bitfield)
	{
		CurrentOptionsBitfield = bitfield;
		m_ChoiceValueProp.Data.value = CurrentOptionsBitfield;
		FirstCurrentOptionIndex = -1;
		List<UIBlockContext_IconSelectionField.Option> list = new List<UIBlockContext_IconSelectionField.Option>();
		for (int i = 0; i < m_SelectionFieldOptions.Length; i++)
		{
			if (IsOptionSelected(i))
			{
				list.Add(m_SelectionFieldOptions[i]);
				FirstCurrentOptionIndex = i;
			}
		}
		CurrentOptions = list.ToArray();
		d.Assert(m_MultiselectEnabled || CurrentOptions.Length <= 1, "Selecting too many");
		OptionSetEvent.Send();
		UpdateColourSwatches();
	}

	private void UpdateColourSwatches()
	{
		Color emissionColor = new Color(CurrentOption.m_Params.m_Return_Color.r * 0.6f, CurrentOption.m_Params.m_Return_Color.g * 0.6f, CurrentOption.m_Params.m_Return_Color.b * 0.6f, 1f);
		base.block.RegisterVariableColours(m_ColorSwatchRenderers, CurrentOption.m_Params.m_Return_Color, emissionColor);
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec context)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				CurrentOptionsBitfield = CurrentOptionsBitfield,
				SaveDataVer = 1
			};
			serialData.Store(context.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(context.saveState);
		if (serialData != null)
		{
			if (serialData.SaveDataVer == 0)
			{
				SetOptionMultiplayerSafe(1 << (int)serialData.CurrentOptionIndex);
			}
			else
			{
				SetOptionMultiplayerSafe(serialData.CurrentOptionsBitfield);
			}
			InstantRefreshEvent.Send();
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(this, "moduleHUDBlockContextControl_CurrentOptionsBitfield", CurrentOptionsBitfield);
			return;
		}
		if (!context.TryRetrieve(this, "moduleHUDBlockContextControl_CurrentOptionsBitfield", out var intVal, m_DefaultSelectionBitflags))
		{
			if (!context.TryRetrieve(this, "moduleHUDBlockContextControl_CurrentOptionIndex", out var intVal2, 0))
			{
				d.LogErrorFormat(this, "moduleHUDBlockContextControl.OnSerializeText - Failed to parse 'CurrentOptionIndex' or 'CurrentOptionsBitfield' from save data on block '{0}'. Setting to default", base.block.name);
			}
			intVal = 1 << intVal2;
		}
		SetOptionMultiplayerSafe(intVal);
		InstantRefreshEvent.Send();
	}

	private void OnMPChoiceValueChanged(IntParamBlockMessage msg)
	{
		d.Assert(msg.value >= 0 && msg.value < 255, "Invalid value in OnMPChoiceValueChanged!");
		SetOptionLocally((byte)msg.value);
	}

	private void OnSpawn()
	{
		SetOptionLocally(m_DefaultSelectionBitflags);
		InstantRefreshEvent.Send();
	}

	private void OnPool()
	{
		base.block.serializeEvent.Subscribe(OnSerialze);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		m_ChoiceValueProp = new NetworkedProperty<IntParamBlockMessage>(this, TTMsgType.SetHUDBlockOptionsContextMenu_CurrentOptionIndex, OnMPChoiceValueChanged);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleHUDColorPickerControl;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		m_ChoiceValueProp.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_ChoiceValueProp.Deserialise(reader);
	}
}
