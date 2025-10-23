using UnityEngine;

public class RotateSign : MonoBehaviour
{
	[SerializeField]
	private float RotateSpeed;

	private void Update()
	{
		base.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
	}
}
