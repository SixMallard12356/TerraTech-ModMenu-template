using UnityEngine;

public class FirstPersonFlyCam : CameraManager.Camera
{
	public float travelSpeed = 1f;

	public float strafeSpeed = 1f;

	public float rotateSpeed = 1f;

	public float elevateSpeed = 1f;

	public float slowFactor = 0.2f;

	public float fastFactor = 5f;

	private Vector3 prevMousePosition;

	private float lastUpdateTime;

	private bool locked;

	private Vector3 lockPosition;

	private Quaternion lockRotation;

	public bool IsLocked => locked;

	public override void Enable()
	{
		prevMousePosition = Input.mousePosition;
	}

	private void Update()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Vector2 axis2D = Singleton.Manager<ManInput>.inst.GetAxis2D(1, 0);
		float axis = Singleton.Manager<ManInput>.inst.GetAxis(68);
		float axis2 = Singleton.Manager<ManInput>.inst.GetAxis(4);
		float axis3 = Singleton.Manager<ManInput>.inst.GetAxis(72);
		if (axis2D.x < 0f)
		{
			num--;
		}
		if (axis2D.x > 0f)
		{
			num++;
		}
		if (axis2D.y > 0f)
		{
			num2++;
		}
		if (axis2D.y < 0f)
		{
			num2--;
		}
		if (axis > 0f)
		{
			num3++;
		}
		if (axis < 0f)
		{
			num3--;
		}
		float num4 = 1f;
		if (axis2 > 0f)
		{
			num4 = fastFactor;
		}
		else if (axis3 > 0f)
		{
			num4 = slowFactor;
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			bool isGraphicOptionEnabled = Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.DOF);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, !isGraphicOptionEnabled);
		}
		Vector3 direction = new Vector3((float)num * travelSpeed, (float)num3 * strafeSpeed, (float)num2 * strafeSpeed);
		Vector3 vector = Vector3.zero;
		if (Input.GetMouseButtonDown(1))
		{
			prevMousePosition = Input.mousePosition;
		}
		else if (Input.GetMouseButton(1) && Time.realtimeSinceStartup - lastUpdateTime < 1f)
		{
			vector = Input.mousePosition - prevMousePosition;
		}
		lastUpdateTime = Time.realtimeSinceStartup;
		prevMousePosition = Input.mousePosition;
		Vector3 newRotation = new Vector3((0f - vector.y) * elevateSpeed, vector.x * rotateSpeed, 0f);
		direction *= num4;
		newRotation *= Mathf.Lerp(num4, 1f, 0.5f);
		if (Input.GetKeyDown(KeyCode.L))
		{
			locked = !locked;
			if (locked)
			{
				lockPosition = Singleton.cameraTrans.position;
				lockRotation = Singleton.cameraTrans.rotation;
			}
		}
		if (locked)
		{
			Singleton.cameraTrans.position = lockPosition;
			Singleton.cameraTrans.rotation = lockRotation;
			return;
		}
		Vector3 newPos = Singleton.cameraTrans.position + Singleton.cameraTrans.TransformDirection(direction);
		PreApplyTransform(ref newPos, ref newRotation);
		Singleton.cameraTrans.position = newPos;
		Quaternion identity = Quaternion.identity;
		identity.SetLookRotation(Singleton.cameraTrans.rotation * Quaternion.Euler(newRotation) * Vector3.forward, Vector3.up);
		Singleton.cameraTrans.rotation = identity;
	}

	protected virtual void PreApplyTransform(ref Vector3 newPos, ref Vector3 newRotation)
	{
	}
}
