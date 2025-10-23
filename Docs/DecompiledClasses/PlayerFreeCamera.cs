using UnityEngine;

public class PlayerFreeCamera : FirstPersonFlyCam
{
	private Tank boundToTank;

	[SerializeField]
	private float maxDistanceFromTank = 100f;

	[SerializeField]
	private float groundClearance = 2f;

	public override void Enable()
	{
		boundToTank = Singleton.playerTank;
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: false);
		base.Enable();
	}

	protected override void PreApplyTransform(ref Vector3 newPos, ref Vector3 newRotation)
	{
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(newPos) + Vector3.up * groundClearance;
		if (newPos.y < vector.y)
		{
			newPos.y = vector.y;
		}
		if (boundToTank != null)
		{
			Vector3 vector2 = newPos - boundToTank.trans.position;
			if (vector2.sqrMagnitude > maxDistanceFromTank * maxDistanceFromTank)
			{
				newPos = boundToTank.trans.position + vector2.normalized * maxDistanceFromTank;
			}
		}
	}
}
