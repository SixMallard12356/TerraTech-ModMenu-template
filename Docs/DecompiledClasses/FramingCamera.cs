using UnityEngine;

public class FramingCamera : CameraManager.Camera
{
	public float initialDistElev = 0.3f;

	public float rotateSpeed = 100f;

	public float elevSpeed = 100f;

	public float zoomSpeed = 100f;

	public float targetInterpSpeed = 1f;

	public float controlInterpSpeed = 1f;

	public float elevationMin = 20f;

	public float elevationMax = 60f;

	public float distanceMin = 25f;

	public float distanceMax = 70f;

	public float tankBoundsDistanceMultiplier = 2f;

	public float tankSpeedDistanceMultiplier = 10f;

	public float tankVelocityLookahead = 1f;

	public float tankVelDamping = 0.05f;

	public float mapViewDistance = 200f;

	public float transitionTime = 1f;

	public AnimationCurve transitionInterpProfile;

	public float transitionMultiplierRotation = 1.5f;

	public float spinInterpSpeed = 4f;

	public float spinSensitivity = 0.6f;

	public float spinSpeedCap = 8f;

	public float spinStopAngleCap = 10f;

	public float controlInterpSpeedSpinHold = 10f;

	[SerializeField]
	private float m_TargetMinHeightAboveGround = 2f;

	[SerializeField]
	private float m_CameraMinHeightAboveGround = 3f;

	private static FramingCamera _inst;

	private float currentAngle;

	private float currentDistance;

	private float currentElevation;

	private Vector3 smoothedTargetPosition = Vector3.zero;

	private float savedTargetDistance;

	private float savedTargetElevation;

	private float savedTargetAngle;

	private float transitionInterp = 1f;

	private Vector3 currentAveragePosition;

	private bool manualSpinningTank;

	private Vector3 spinInitialMousePos;

	private float currentSpinAngle;

	private float spinInitialCameraAngle;

	private static Vector3 prevTargetPosition;

	private static Quaternion prevCameraRotation;

	private static float prevCameraDistance;

	private static Tank prevTank;

	public static FramingCamera inst => _inst;

	public float TargetDistance { get; set; }

	public float TargetElevation { get; set; }

	public float TargetAngle { get; set; }

	public bool overviewMode { get; private set; }

	public void BeginSpinControl()
	{
		manualSpinningTank = true;
		float f = TargetAngle - currentAngle;
		TargetAngle = currentAngle + Mathf.Min(spinStopAngleCap, Mathf.Abs(f)) * Mathf.Sign(f);
		spinInitialMousePos = Input.mousePosition;
		currentSpinAngle = 0f;
		spinInitialCameraAngle = TargetAngle;
	}

	public void EndSpinControl()
	{
		manualSpinningTank = false;
	}

	private void Awake()
	{
		_inst = this;
	}

	private void Start()
	{
		TargetDistance = Mathf.Lerp(distanceMin, distanceMax, initialDistElev);
		TargetElevation = Mathf.Lerp(elevationMin, elevationMax, initialDistElev);
		Enable();
	}

	public void ClearCaches()
	{
		smoothedTargetPosition = Vector3.zero;
	}

	public override void Enable()
	{
		transitionInterp = 0f;
		prevTargetPosition = Singleton.cameraTrans.position;
		smoothedTargetPosition = prevTargetPosition;
		prevCameraRotation = Singleton.cameraTrans.rotation;
		prevCameraDistance = 0f;
		manualSpinningTank = false;
	}

	private void Update()
	{
		if (Singleton.Manager<ManTechs>.inst.Count == 0)
		{
			return;
		}
		currentAveragePosition = Vector3.zero;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			currentAveragePosition += item.boundsCentreWorld;
		}
		currentAveragePosition /= (float)Singleton.Manager<ManTechs>.inst.Count;
		Vector3 vector = currentAveragePosition - Singleton.cameraTrans.position;
		float num = 0f;
		foreach (Tank item2 in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			float b = Mathf.Tan(Mathf.Acos(Vector3.Dot((item2.boundsCentreWorld - Singleton.cameraTrans.position).normalized, vector.normalized)) * vector.magnitude);
			num = Mathf.Max(num, b);
		}
		TargetDistance = num * 2f;
		TargetDistance = 30f;
		float t = Mathf.Min(controlInterpSpeed * Time.deltaTime, 1f);
		if (manualSpinningTank)
		{
			float num2 = (0f - ((Input.mousePosition - spinInitialMousePos) / Screen.width).x) * spinSensitivity * 1000f;
			num2 *= Singleton.instance.globals.m_RuntimeCameraSpinSensHorizontal;
			if (num2 == 0f)
			{
				t = Mathf.Min(controlInterpSpeedSpinHold * Time.deltaTime, 1f);
			}
			currentSpinAngle = Mathf.Lerp(currentSpinAngle, num2, spinInterpSpeed * Time.deltaTime);
			TargetAngle = spinInitialCameraAngle - currentSpinAngle;
		}
		float num3 = Mathf.Lerp(currentAngle, TargetAngle, t) - currentAngle;
		currentAngle += num3;
		currentDistance = Mathf.Lerp(currentDistance, TargetDistance, t);
		currentElevation = Mathf.Lerp(currentElevation, TargetElevation, t);
		float t2 = targetInterpSpeed * Time.deltaTime;
		Vector3 vector2 = currentAveragePosition;
		Vector3 scenePos = vector2;
		if (m_TargetMinHeightAboveGround >= 0f && Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos))
		{
			vector2.y = Mathf.Max(vector2.y, scenePos.y + m_TargetMinHeightAboveGround);
		}
		if (smoothedTargetPosition == Vector3.zero)
		{
			smoothedTargetPosition = vector2;
		}
		else
		{
			smoothedTargetPosition = Vector3.Lerp(smoothedTargetPosition, vector2, t2);
		}
		Vector3 vector3 = smoothedTargetPosition;
		float num4 = currentDistance;
		Vector3 vector4 = vector2 - Singleton.cameraTrans.position;
		float magnitude = vector4.magnitude;
		float f = Vector3.Dot(vector4, -Vector3.up) / magnitude;
		float x = Mathf.Lerp(90f - Mathf.Acos(f) * 57.29578f, currentElevation, t2);
		float y = vector4.HorizontalAngle() + num3;
		Quaternion quaternion = Quaternion.Euler(new Vector3(x, y, 0f));
		Vector3 vector5 = vector2 + quaternion * new Vector3(0f, 0f, 0f - num4);
		scenePos = vector5;
		if (m_CameraMinHeightAboveGround >= 0f && Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos))
		{
			vector5.y = Mathf.Max(vector5.y, scenePos.y + m_CameraMinHeightAboveGround);
		}
		Vector3 forward = vector3 - vector5;
		if (forward.sqrMagnitude > 0f)
		{
			forward.Normalize();
			quaternion = Quaternion.LookRotation(forward, Vector3.up);
		}
		else
		{
			quaternion = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		}
		if (transitionInterp == 1f)
		{
			Singleton.cameraTrans.position = vector5;
			Singleton.cameraTrans.rotation = quaternion;
			prevTargetPosition = vector3;
			prevCameraRotation = quaternion;
			prevCameraDistance = num4;
		}
		else
		{
			transitionInterp = Mathf.Min(transitionInterp + Time.deltaTime / transitionTime, 1f);
			float t3 = transitionInterpProfile.Evaluate(transitionInterp);
			float t4 = transitionInterpProfile.Evaluate(transitionInterp * transitionMultiplierRotation);
			Vector3 vector6 = Vector3.Lerp(prevTargetPosition, vector3, t3);
			Quaternion quaternion2 = Quaternion.Slerp(prevCameraRotation, quaternion, t3);
			float num5 = Mathf.Lerp(prevCameraDistance, num4, t3);
			Singleton.cameraTrans.position = vector6 + quaternion2 * new Vector3(0f, 0f, 0f - num5);
			Quaternion b2 = Quaternion.LookRotation(vector3 - Singleton.cameraTrans.position);
			Singleton.cameraTrans.rotation = Quaternion.Slerp(prevCameraRotation, b2, t4);
		}
		bool flag = Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeAttract>();
		if (Singleton.Manager<CameraManager>.inst.DOF != null && !Singleton.Manager<CameraManager>.inst.DOF.enabled && !flag)
		{
			bool flag2 = true;
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			if (currentUser != null && !currentUser.m_GraphicsSettings.m_DOF)
			{
				flag2 = false;
			}
			if (flag2)
			{
				Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: true);
			}
		}
		if (Singleton.Manager<CameraManager>.inst.DOF != null && Singleton.Manager<CameraManager>.inst.DOF.enabled)
		{
			if (!Singleton.Manager<ManUI>.inst.IsStackEmpty() && !flag)
			{
				Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance(0f);
			}
			else
			{
				Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance((vector3 - Singleton.cameraTrans.position).magnitude);
			}
		}
	}
}
