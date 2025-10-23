using UnityEngine;

[ExecuteInEditMode]
public class TempFilledCell : MonoBehaviour
{
	private Vector3 position;

	private void Start()
	{
		position = base.transform.localPosition;
	}

	private void Update()
	{
		base.transform.localPosition = position;
		base.transform.localScale = Vector3.one;
		base.transform.localRotation = Quaternion.identity;
	}
}
