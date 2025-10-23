using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class RecycleAfterTime : MonoBehaviour
{
	public float m_RecycleAfter;

	private float timer;

	private void OnSpawn()
	{
		timer = 0f;
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= m_RecycleAfter)
		{
			base.transform.Recycle();
		}
	}
}
