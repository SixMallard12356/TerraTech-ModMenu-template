#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITeleporterMenuHUD : UIHUDElement
{
	public delegate void SelectedDelegate(bool selected);

	[Serializable]
	public class TeleportMenuGroupInfo
	{
		public Teleporter.TeleporterGroups Group;

		public Transform GeneratedButtonsPanelObject;

		public UISelectableHoverableButton GroupButton;

		public Image DescriptionImage;

		public LocalisedString DescriptionText;

		public LocalisedString DescriptionTitle;

		[NonSerialized]
		public List<UITeleporterButton> TeleporterButtons = new List<UITeleporterButton>();
	}

	public struct Context
	{
		public Teleporter[] allTeleporters;

		public Teleporter localTeleporter;
	}

	[SerializeField]
	private UITeleporterButton m_TeleporterButtonPrefab;

	[SerializeField]
	private List<TeleportMenuGroupInfo> m_TeleporterGroupInfos = new List<TeleportMenuGroupInfo>();

	private int m_SelectedGroupIndex;

	[SerializeField]
	private Image m_GroupDescriptionImage;

	[SerializeField]
	private TextMeshProUGUI m_GroupDescriptionText;

	[SerializeField]
	private Text m_GroupDescriptionTitle;

	private List<UITeleporterButton> m_AllButtons;

	private Teleporter[] allTeleporters;

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
		}
		TankCamera.inst.FreezeCamera(freezeCamera: true);
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = true;
		Context context2 = (Context)context;
		for (int i = 0; i < m_TeleporterGroupInfos.Count; i++)
		{
			if (!(m_TeleporterGroupInfos[i].GroupButton != null) || !(m_TeleporterGroupInfos[i].GeneratedButtonsPanelObject != null))
			{
				continue;
			}
			m_TeleporterGroupInfos[i].GroupButton.Initialise();
			m_TeleporterGroupInfos[i].GeneratedButtonsPanelObject.gameObject.SetActive(value: false);
			int buttonIndex = i;
			m_TeleporterGroupInfos[i].GroupButton.onClick.AddListener(delegate
			{
				OnTeleportGroupButtonClicked(buttonIndex);
			});
			m_TeleporterGroupInfos[i].GroupButton.onPointerEnter.AddListener(delegate
			{
				OnTeleportGroupButtonPointerEntered(buttonIndex);
			});
			m_TeleporterGroupInfos[i].GroupButton.onPointerExit.AddListener(delegate
			{
				OnTeleportGroupButtonPointerExited(buttonIndex);
			});
			if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				m_TeleporterGroupInfos[i].GroupButton.onClick.AddListener(delegate
				{
					OnTeleportGroupButtonClickedGamepad(buttonIndex);
				});
				m_TeleporterGroupInfos[i].GroupButton.onSelected.AddListener(delegate
				{
					OnTeleportGroupButtonClicked(buttonIndex);
				});
			}
		}
		m_AllButtons = new List<UITeleporterButton>();
		allTeleporters = context2.allTeleporters;
		if (allTeleporters != null)
		{
			Array.Sort(allTeleporters, (Teleporter m, Teleporter n) => m.GroupSortIndex.CompareTo(n.GroupSortIndex));
			Teleporter[] array = allTeleporters;
			foreach (Teleporter teleporter in array)
			{
				List<TeleportMenuGroupInfo> list = m_TeleporterGroupInfos.FindAll((TeleportMenuGroupInfo x) => x.Group == teleporter.Group);
				d.AssertFormat(list.Count == 1, "Should be exactly one UI TeleportMenuGroupInfo per Teleporter Group ({0} for {1})", list.Count, teleporter.Group.ToString());
				if (list.Count != 1)
				{
					continue;
				}
				UITeleporterButton uITeleporterButton = m_TeleporterButtonPrefab.Spawn();
				uITeleporterButton.Initialise();
				uITeleporterButton.transform.SetParent(list[0].GeneratedButtonsPanelObject.transform, worldPositionStays: false);
				_ = teleporter;
				uITeleporterButton.GetComponentInChildren<Text>().text = teleporter.LocalisedName;
				uITeleporterButton.onClick.AddListener(delegate
				{
					OnTeleportButtonClicked(teleporter);
				});
				uITeleporterButton.onPointerEnter.AddListener(delegate
				{
					OnTeleportButtonPointerEntered(teleporter);
				});
				uITeleporterButton.onPointerExit.AddListener(delegate
				{
					OnTeleportButtonPointerExited(teleporter);
				});
				if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
				{
					uITeleporterButton.onSelected.AddListener(delegate
					{
						OnTeleportButtonSelected(teleporter);
					});
					uITeleporterButton.onDeselected.AddListener(delegate
					{
						OnTeleportButtonDeselected(teleporter);
					});
				}
				if (teleporter == context2.localTeleporter)
				{
					m_SelectedGroupIndex = m_TeleporterGroupInfos.IndexOf(list[0]);
					list[0].GroupButton.onClick.Invoke();
					if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
					{
						Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(uITeleporterButton.gameObject);
					}
					else
					{
						Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(list[0].GroupButton.gameObject);
					}
				}
				list[0].TeleporterButtons.Add(uITeleporterButton);
				m_AllButtons.Add(uITeleporterButton);
			}
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		}
		else
		{
			d.LogError("UITeleporterMenuHUD.Show was called with invalid context!");
		}
		for (int num2 = 0; num2 < m_TeleporterGroupInfos.Count; num2++)
		{
			if (m_TeleporterGroupInfos[num2].TeleporterButtons.Count > 0)
			{
				m_TeleporterGroupInfos[num2].GroupButton.SetNavigationDown(m_TeleporterGroupInfos[num2].TeleporterButtons[0]);
			}
			for (int num3 = 0; num3 < m_TeleporterGroupInfos[num2].TeleporterButtons.Count; num3++)
			{
				UITeleporterButton selectable = m_TeleporterGroupInfos[num2].TeleporterButtons[num3];
				if (num3 == 0)
				{
					selectable.SetNavigationUp(m_TeleporterGroupInfos[num2].GroupButton);
				}
				else
				{
					selectable.SetNavigationUp(m_TeleporterGroupInfos[num2].TeleporterButtons[num3 - 1]);
				}
				if (num3 + 1 < m_TeleporterGroupInfos[num2].TeleporterButtons.Count)
				{
					selectable.SetNavigationDown(m_TeleporterGroupInfos[num2].TeleporterButtons[num3 + 1]);
				}
				if (num2 > 0 && m_TeleporterGroupInfos[num2 - 1].GroupButton != null)
				{
					selectable.SetNavigationLeft(m_TeleporterGroupInfos[num2 - 1].GroupButton);
				}
				else
				{
					selectable.SetNavigationLeft(null);
				}
				if (num2 < m_TeleporterGroupInfos.Count - 1 && m_TeleporterGroupInfos[num2 + 1].GroupButton != null)
				{
					selectable.SetNavigationRight(m_TeleporterGroupInfos[num2 + 1].GroupButton);
				}
				else
				{
					selectable.SetNavigationRight(null);
				}
			}
		}
	}

	private void OnTeleportButtonDeselected(Teleporter teleporter)
	{
		OnTeleportButtonPointerExited(teleporter);
	}

	private void OnTeleportButtonSelected(Teleporter teleporter)
	{
		OnTeleportButtonPointerEntered(teleporter);
	}

	private void OnTeleportButtonPointerExited(Teleporter teleporter)
	{
		UpdateDetailsSectionToGroup(m_TeleporterGroupInfos[m_SelectedGroupIndex]);
	}

	private void OnTeleportButtonPointerEntered(Teleporter teleporter)
	{
		UpdateDetailsSectionToTeleporter(teleporter);
	}

	public override void Hide(object context)
	{
		CleanUp();
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		base.Hide(context);
	}

	public void CloseTeleporterMenu()
	{
		HideSelf();
	}

	private void RecycleButtons()
	{
		foreach (UITeleporterButton allButton in m_AllButtons)
		{
			allButton.onClick.RemoveAllListeners();
			allButton.onPointerEnter.RemoveAllListeners();
			allButton.onPointerExit.RemoveAllListeners();
			allButton.onSelected.RemoveAllListeners();
			allButton.onDeselected.RemoveAllListeners();
			allButton.Recycle(worldPosStays: false);
		}
		m_AllButtons.Clear();
		for (int i = 0; i < m_TeleporterGroupInfos.Count; i++)
		{
			m_TeleporterGroupInfos[i].GroupButton.onClick.RemoveAllListeners();
			m_TeleporterGroupInfos[i].GroupButton.onSelected.RemoveAllListeners();
			m_TeleporterGroupInfos[i].GroupButton.onPointerEnter.RemoveAllListeners();
			m_TeleporterGroupInfos[i].GroupButton.onPointerExit.RemoveAllListeners();
		}
	}

	private void CleanUp()
	{
		RecycleButtons();
		if (m_SelectedGroupIndex >= 0 && m_SelectedGroupIndex < m_TeleporterGroupInfos.Count)
		{
			m_TeleporterGroupInfos[m_SelectedGroupIndex].GeneratedButtonsPanelObject.gameObject.SetActive(value: false);
			m_TeleporterGroupInfos[m_SelectedGroupIndex].GroupButton.SetOnState(isOn: false);
		}
		m_SelectedGroupIndex = 0;
		for (int i = 0; i < m_TeleporterGroupInfos.Count; i++)
		{
			m_TeleporterGroupInfos[i].TeleporterButtons.Clear();
		}
		TankCamera.inst.FreezeCamera(freezeCamera: false);
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = false;
	}

	private void OnTeleportButtonClicked(Teleporter targetTeleporter)
	{
		HideSelf();
		Teleporter.TeleportPlayerToTarget(targetTeleporter);
	}

	private void OnTeleportGroupButtonClicked(int clickedButtonIndex)
	{
		if (m_SelectedGroupIndex >= 0 && m_SelectedGroupIndex < m_TeleporterGroupInfos.Count && clickedButtonIndex != m_SelectedGroupIndex)
		{
			m_TeleporterGroupInfos[m_SelectedGroupIndex].GeneratedButtonsPanelObject.gameObject.SetActive(value: false);
			m_TeleporterGroupInfos[m_SelectedGroupIndex].GroupButton.SetOnState(isOn: false);
		}
		m_TeleporterGroupInfos[clickedButtonIndex].GeneratedButtonsPanelObject.gameObject.SetActive(value: true);
		m_TeleporterGroupInfos[clickedButtonIndex].GroupButton.SetOnState(isOn: true);
		UpdateDetailsSectionToGroup(m_TeleporterGroupInfos[clickedButtonIndex]);
		m_SelectedGroupIndex = clickedButtonIndex;
	}

	private void OnTeleportGroupButtonClickedGamepad(int clickedButtonIndex)
	{
		if (clickedButtonIndex >= 0 && clickedButtonIndex < m_TeleporterGroupInfos.Count && m_TeleporterGroupInfos[clickedButtonIndex].TeleporterButtons.Count > 0)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_TeleporterGroupInfos[clickedButtonIndex].TeleporterButtons[0].gameObject);
		}
	}

	private void OnTeleportGroupButtonPointerEntered(int clickedButtonIndex)
	{
		UpdateDetailsSectionToGroup(m_TeleporterGroupInfos[clickedButtonIndex]);
	}

	private void OnTeleportGroupButtonPointerExited(int clickedButtonIndex)
	{
		UpdateDetailsSectionToGroup(m_TeleporterGroupInfos[m_SelectedGroupIndex]);
	}

	private void UpdateDetailsSectionToGroup(TeleportMenuGroupInfo groupInfo)
	{
		if (m_GroupDescriptionImage != null && groupInfo.DescriptionImage != null)
		{
			m_GroupDescriptionImage.sprite = groupInfo.DescriptionImage.sprite;
		}
		if (m_GroupDescriptionTitle != null && groupInfo.DescriptionTitle != null)
		{
			m_GroupDescriptionTitle.text = groupInfo.DescriptionTitle.Value;
		}
		if (m_GroupDescriptionText != null && groupInfo.DescriptionText != null)
		{
			m_GroupDescriptionText.text = groupInfo.DescriptionText.Value;
		}
	}

	private void UpdateDetailsSectionToTeleporter(Teleporter teleporter)
	{
		if (m_GroupDescriptionTitle != null)
		{
			m_GroupDescriptionTitle.text = ((teleporter == null) ? string.Empty : teleporter.LocalisedName);
		}
		if (m_GroupDescriptionImage != null)
		{
			m_GroupDescriptionText.text = ((teleporter == null) ? string.Empty : teleporter.LocalisedDescription);
		}
		if (m_GroupDescriptionImage != null)
		{
			m_GroupDescriptionImage.sprite = ((teleporter == null || teleporter.DescriptionImage == null) ? null : teleporter.DescriptionImage);
		}
	}

	private void OnPool()
	{
	}

	private void OnRecycle()
	{
		if (base.IsVisible)
		{
			CleanUp();
		}
	}
}
