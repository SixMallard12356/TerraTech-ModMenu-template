#define UNITY_EDITOR
using UnityEngine;

public class UITechAITargetSelect : UIHUDElement
{
	public struct AITargetSelectContext
	{
		public Tank targetTank;

		public UIRadialTechControlMenu.PlayerCommands selectedCommand;
	}

	[SerializeField]
	private RectTransform m_TargetLine;

	[Tooltip("Visible while 'dragging' the cursor")]
	[SerializeField]
	private Transform m_GroundTargetMarkerPrefab;

	[SerializeField]
	[Tooltip("Spawned when clicking to confirm the selection. This object should be able to manage (remove) itself")]
	private Transform m_GroundTargetConfirmedPrefab;

	private Tank m_SubjectTank;

	private UIRadialTechControlMenu.PlayerCommands m_PlayerCommand;

	private Transform m_GroundTargetMarker;

	public override void Show(object context)
	{
		base.Show(context);
		AITargetSelectContext aITargetSelectContext = (AITargetSelectContext)context;
		InitMenu(aITargetSelectContext.targetTank, aITargetSelectContext.selectedCommand);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		DeInitMenu();
	}

	private void Update()
	{
		if (base.gameObject.activeSelf)
		{
			UpdateGroundMarker();
			UpdateTargetLine();
			if (Input.GetMouseButtonUp(0))
			{
				ConfirmTargetSelection();
				HideSelf();
			}
		}
	}

	private void InitMenu(Tank targetTank, UIRadialTechControlMenu.PlayerCommands command)
	{
		d.Assert(targetTank != null, "targetTank should not be null entering UITechAITargetSelect!");
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: false, ManPointer.DragDisableReason.AITargetSelect);
		m_SubjectTank = targetTank;
		m_PlayerCommand = command;
		if (m_GroundTargetMarker == null && m_GroundTargetMarkerPrefab != null)
		{
			m_GroundTargetMarker = m_GroundTargetMarkerPrefab.Spawn();
			m_GroundTargetMarker.gameObject.SetActive(value: false);
		}
	}

	private void DeInitMenu()
	{
		ResetChildren();
		m_SubjectTank = null;
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: true, ManPointer.DragDisableReason.AITargetSelect);
	}

	private void UpdateGroundMarker()
	{
		if (m_GroundTargetMarker != null)
		{
			bool flag = Singleton.Manager<ManPointer>.inst.targetVisible == null;
			if (flag)
			{
				m_GroundTargetMarker.SetPositionIfChanged(Singleton.Manager<ManPointer>.inst.targetPosition);
				Vector3 forward = Singleton.cameraTrans.right.SetY(0f);
				m_GroundTargetMarker.SetRotationIfChanged(Quaternion.LookRotation(forward));
			}
			if (m_GroundTargetMarker.gameObject.activeSelf != flag)
			{
				m_GroundTargetMarker.gameObject.SetActive(flag);
			}
		}
	}

	private void UpdateTargetLine()
	{
		if (m_TargetLine != null)
		{
			Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(m_SubjectTank.boundsCentreWorld);
			Vector2 vector2 = new Vector2(vector.x - (float)Screen.width * 0.5f, vector.y - (float)Screen.height * 0.5f);
			Vector2 vector3 = Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen() - vector2;
			m_TargetLine.anchoredPosition = vector2;
			float magnitude = vector3.magnitude;
			m_TargetLine.sizeDelta = new Vector2(magnitude, m_TargetLine.sizeDelta.y);
			if (magnitude > 0f)
			{
				Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(vector3.y, vector3.x) * 57.29578f, Vector3.forward);
				m_TargetLine.rotation = rotation;
			}
			if (!m_TargetLine.gameObject.activeSelf)
			{
				m_TargetLine.gameObject.SetActive(value: true);
			}
		}
	}

	private void ConfirmTargetSelection()
	{
		AITreeType.AITypes behaviorType;
		if (m_PlayerCommand == UIRadialTechControlMenu.PlayerCommands.AIGuard)
		{
			if ((bool)Singleton.Manager<ManPointer>.inst.targetTank)
			{
				behaviorType = ((!Singleton.Manager<ManPointer>.inst.targetTank.IsEnemy(0)) ? AITreeType.AITypes.Escort : AITreeType.AITypes.Escort);
			}
			else
			{
				behaviorType = AITreeType.AITypes.Guard;
				m_GroundTargetConfirmedPrefab.Spawn(Singleton.Manager<ManPointer>.inst.targetPosition);
			}
		}
		else if (m_PlayerCommand == UIRadialTechControlMenu.PlayerCommands.AIHarvest)
		{
			behaviorType = AITreeType.AITypes.Harvest;
			m_GroundTargetConfirmedPrefab.Spawn(Singleton.Manager<ManPointer>.inst.targetPosition);
			if (!Singleton.Manager<ManPointer>.inst.targetTank && !Singleton.Manager<ManPointer>.inst.targetVisible)
			{
			}
		}
		else
		{
			d.LogError("Unexpected AI Control type in TargetSelection");
			behaviorType = AITreeType.AITypes.Idle;
		}
		m_SubjectTank.AI.SetBehaviorType(behaviorType);
	}

	private void ResetChildren()
	{
		if (m_GroundTargetMarker != null)
		{
			m_GroundTargetMarker.Recycle();
			m_GroundTargetMarker = null;
		}
		if (m_TargetLine != null)
		{
			m_TargetLine.gameObject.SetActive(value: false);
		}
	}

	private void OnSpawn()
	{
		ResetChildren();
	}
}
