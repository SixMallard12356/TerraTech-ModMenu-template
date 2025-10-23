using UnityEngine;

public class HUDFPS : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private void Start()
	{
		base.gameObject.SetActive(value: false);
		Object.Destroy(base.gameObject);
	}
}
