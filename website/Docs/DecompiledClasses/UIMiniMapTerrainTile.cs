using UnityEngine;
using UnityEngine.UI;

public class UIMiniMapTerrainTile : MonoBehaviour
{
	[SerializeField]
	private RawImage m_TileImage;

	[SerializeField]
	private Transform m_LoadingTransform;

	private RectTransform m_RectTrans;

	public RectTransform RectTrans => m_RectTrans;

	public RawImage TileImage => m_TileImage;

	public void SetIsLoading(bool isLoading)
	{
		if (m_LoadingTransform != null)
		{
			m_LoadingTransform.gameObject.SetActive(isLoading);
		}
	}

	private void OnPool()
	{
		m_RectTrans = base.transform as RectTransform;
	}

	private void OnRecycle()
	{
		m_TileImage.texture = null;
	}
}
