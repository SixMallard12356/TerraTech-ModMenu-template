using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class CameraFacingBillboard : MonoBehaviour
{
	public bool m_FreezeToOneAxis;

	public bool m_UseCameraForward;

	private Transform trans;

	private Transform camTrans;

	private void Awake()
	{
		trans = base.transform;
		camTrans = Singleton.cameraTrans;
	}

	private void LateUpdate()
	{
		if (m_FreezeToOneAxis)
		{
			trans.forward = (trans.position - camTrans.position).SetY(0f);
		}
		else if (m_UseCameraForward)
		{
			trans.forward = camTrans.forward.SetY(0f);
		}
		else
		{
			trans.LookAt(trans.position + camTrans.rotation * -Vector3.back, camTrans.rotation * Vector3.up);
		}
	}
}
