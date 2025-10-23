#define UNITY_EDITOR
using UnityEngine;

internal class ArcEffect
{
	private LineRenderer line;

	private UVTextureAnimator animator;

	public ArcEffect(LineRenderer line)
	{
		this.line = line;
		animator = line.GetComponent<UVTextureAnimator>();
		d.Assert((bool)line && (bool)animator);
		animator.FinishedEvent.Subscribe(OnAnimationFinished);
	}

	public void Fire(int variant)
	{
		if (line.gameObject.activeSelf)
		{
			animator.Reset();
		}
		else
		{
			line.gameObject.SetActive(value: true);
		}
		line.material.SetFloat("_Chanel", variant);
	}

	public void UpdatePositionIfActive(Vector3 start, Vector3 end)
	{
		if (line.gameObject.activeSelf)
		{
			line.SetPosition(0, start);
			line.SetPosition(1, end);
		}
	}

	public void Hide()
	{
		if (line.gameObject.activeSelf)
		{
			line.gameObject.SetActive(value: false);
		}
	}

	private void OnAnimationFinished(UVTextureAnimator animator)
	{
		animator.gameObject.SetActive(value: false);
	}

	public Transform GetLineTransform()
	{
		return line.transform;
	}
}
