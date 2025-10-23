using UnityEngine;

public class AutoSpriteRenderer : MonoBehaviour
{
	public Vector3 objectCenter = Vector3.zero;

	public float distanceFromObject = 3.5f;

	public Vector3 cameraPos = new Vector3(-2.238608f, 1.492405f, -2.238607f);

	public float cameraRoll;

	private void PrePool()
	{
		Object.Destroy(this);
	}
}
