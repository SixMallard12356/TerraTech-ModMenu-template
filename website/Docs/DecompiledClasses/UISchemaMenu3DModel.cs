#define UNITY_EDITOR
using UnityEngine;

public class UISchemaMenu3DModel : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;

	[SerializeField]
	private Camera m_Camera;

	[SerializeField]
	[EnumArray(typeof(MovementAxis))]
	private string[] m_AxisParams;

	private const int TEXWIDTH = 320;

	private const int TEXHEIGHT = 260;

	private RenderTexture m_RenderTex;

	public void SetAnimation(MovementAxis animAxis)
	{
		EnumValuesIterator<MovementAxis> enumerator = EnumIterator<MovementAxis>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			MovementAxis current = enumerator.Current;
			if (HasAnimation(current))
			{
				m_Animator.SetBool(m_AxisParams[(int)current], animAxis == current);
			}
		}
	}

	public void StopAnimation()
	{
		EnumValuesIterator<MovementAxis> enumerator = EnumIterator<MovementAxis>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			MovementAxis current = enumerator.Current;
			if (HasAnimation(current))
			{
				m_Animator.SetBool(m_AxisParams[(int)current], value: false);
			}
		}
	}

	public RenderTexture GetRenderTex()
	{
		return m_Camera.targetTexture;
	}

	private bool HasAnimation(MovementAxis axis)
	{
		if (axis >= MovementAxis.MoveX_MoveRight && (int)axis < m_AxisParams.Length)
		{
			return !m_AxisParams[(int)axis].NullOrEmpty();
		}
		return false;
	}

	private void OnSpawn()
	{
		d.Assert(m_RenderTex == null);
		m_RenderTex = new RenderTexture(280, 200, 16);
		m_RenderTex.Create();
		m_Camera.targetTexture = m_RenderTex;
	}

	private void OnRecycle()
	{
		m_Camera.targetTexture = null;
		m_RenderTex.Release();
		m_RenderTex = null;
	}
}
