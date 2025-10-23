using UnityEngine;

public class ScaleOnStart : MonoBehaviour
{
	public float XZRangePercent = 0.15f;

	public float YRangePercent = 0.15f;

	private Vector3 originalScale;

	private void OnPool()
	{
		originalScale = base.transform.localScale;
	}

	private void OnSpawn()
	{
		if (base.enabled)
		{
			float num = 1f.RandomVariance(XZRangePercent);
			float num2 = 1f.RandomVariance(YRangePercent);
			base.transform.localScale = new Vector3(originalScale.x * num, originalScale.y * num2, originalScale.z * num);
		}
	}
}
