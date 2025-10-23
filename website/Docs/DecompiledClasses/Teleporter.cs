using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public enum TeleporterGroups
	{
		Battle,
		Race,
		Experimental,
		GrasslandBiomes,
		DesertBiomes,
		MountainBiomes,
		ExoticBiomes,
		Other
	}

	[SerializeField]
	private Vector3 m_DestinationOffset = new Vector3(10f, 0f, 0f);

	[Tooltip("World space facing direction of tech")]
	[SerializeField]
	private Vector3 m_DestinationTechFacingDirection = Vector3.forward;

	[Tooltip("World space camera view direction. If left at (0,0,0) it will use the tech facing direction instead")]
	[SerializeField]
	private Vector3 m_CameraViewDirection = Vector3.zero;

	[SerializeField]
	private LocalisedString m_LocalisedName;

	[SerializeField]
	private LocalisedString m_LocalisedDescription;

	[SerializeField]
	private Sprite m_DescriptionImage;

	private float m_PlayerTriggerEnterDistance = float.MinValue;

	private static Teleporter[] s_AllTeleporters;

	[SerializeField]
	private TeleporterGroups m_Group = TeleporterGroups.Other;

	[SerializeField]
	private uint m_GroupSortIndex;

	public string LocalisedName => m_LocalisedName.Value;

	public string LocalisedDescription => m_LocalisedDescription.Value;

	public Sprite DescriptionImage => m_DescriptionImage;

	public TeleporterGroups Group => m_Group;

	public uint GroupSortIndex => m_GroupSortIndex;

	public static void TeleportPlayerToTarget(Teleporter targetTeleporter)
	{
		if (Singleton.Manager<CameraManager>.inst.TeleportEffect.TryStartTeleportationEffect(targetTeleporter.TeleportPlayer))
		{
			Singleton.Manager<ManGameMode>.inst.LockPlayerControls = true;
			Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: true, ManPauseGame.DisablePauseReason.TeleportScreenFX);
		}
	}

	private void TeleportPlayer()
	{
		float num = 5f;
		if (Singleton.playerTank.IsNotNull())
		{
			num = (Singleton.cameraTrans.position - Singleton.playerTank.boundsCentreWorld).magnitude;
			Vector3 vector = m_DestinationOffset;
			float magnitude = (Singleton.playerTank.blockBounds.extents + Vector3.one).SetY(0f).magnitude;
			float magnitude2 = vector.magnitude;
			if (magnitude > magnitude2)
			{
				vector = vector.normalized * magnitude;
			}
			Vector3 vector2 = base.transform.position + vector;
			Singleton.playerTank.visible.Teleport(vector2, Quaternion.LookRotation(m_DestinationTechFacingDirection, Vector3.up));
			if (Mode<ModeMisc>.inst.GetGameType() == ManGameMode.GameType.RaD)
			{
				Mode<ModeMisc>.inst.CurrentMode.m_LastCheckpointSpawnDestination.position = vector2 + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
				Mode<ModeMisc>.inst.CurrentMode.m_LastCheckpointSpawnDestination.forward = m_DestinationTechFacingDirection;
			}
		}
		Vector3 obj = (Singleton.playerTank.IsNotNull() ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
		Vector3 vector3 = ((m_CameraViewDirection == Vector3.zero) ? m_DestinationTechFacingDirection : m_CameraViewDirection);
		if (vector3 == Vector3.zero)
		{
			vector3 = Vector3.forward;
		}
		Vector3 vector4 = obj - vector3 * num;
		Quaternion rotation = Quaternion.LookRotation(vector3, Vector3.up);
		Singleton.Manager<CameraManager>.inst.ResetCamera(vector4, rotation);
		if (Mode<ModeMisc>.inst.GetGameType() == ManGameMode.GameType.RaD)
		{
			Mode<ModeMisc>.inst.CurrentMode.m_LastCheckpointCameraDestination.position = vector4 + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
			Mode<ModeMisc>.inst.CurrentMode.m_LastCheckpointCameraDestination.forward = vector3;
		}
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = false;
		Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: false, ManPauseGame.DisablePauseReason.TeleportScreenFX);
	}

	private static Teleporter[] GetAllTeleporters()
	{
		if (s_AllTeleporters == null)
		{
			s_AllTeleporters = Object.FindObjectsOfType<Teleporter>();
		}
		return s_AllTeleporters;
	}

	private void OnTriggerEnter(Collider other)
	{
		Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(other);
		if (visible.IsNotNull() && Singleton.playerTank != null && (visible.tank == Singleton.playerTank || visible.block?.tank == Singleton.playerTank) && m_PlayerTriggerEnterDistance < 0f)
		{
			Vector3 boundsCentreWorld = Singleton.playerTank.boundsCentreWorld;
			m_PlayerTriggerEnterDistance = Vector3.Distance(base.transform.position, boundsCentreWorld);
			ManHUD.HUDElementType hudElemType = ManHUD.HUDElementType.TeleportMenu;
			if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hudElemType))
			{
				Teleporter[] allTeleporters = GetAllTeleporters();
				UITeleporterMenuHUD.Context context = new UITeleporterMenuHUD.Context
				{
					allTeleporters = allTeleporters,
					localTeleporter = this
				};
				Singleton.Manager<ManHUD>.inst.ShowHudElement(hudElemType, context);
			}
		}
	}

	private void OnRecycle()
	{
		Singleton.Manager<CameraManager>.inst.TeleportEffect.CancelTeleportationEffect();
		s_AllTeleporters = null;
	}

	private void Update()
	{
		if (m_PlayerTriggerEnterDistance > 0f)
		{
			ManHUD.HUDElementType hudElemType = ManHUD.HUDElementType.TeleportMenu;
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hudElemType) && Singleton.playerTank.IsNotNull())
			{
				Singleton.playerTank.rbody.velocity = Vector3.zero;
			}
			Vector3 a = (Singleton.playerTank.IsNotNull() ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
			if (Singleton.playerTank.IsNull() || Vector3.Distance(a, base.transform.position) > m_PlayerTriggerEnterDistance)
			{
				m_PlayerTriggerEnterDistance = float.MinValue;
			}
		}
	}
}
