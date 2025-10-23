using UnityEngine;

public class TeyonPS4SafeAreaHandler : MonoBehaviour
{
	[SerializeField]
	private ManHUD.HUDElementType[] m_SafeHudItems;

	[SerializeField]
	private RectTransform m_Prefab;

	[SerializeField]
	private UIHUD m_Hud;

	[SerializeField]
	private PrefabSpawner m_Spawner;

	private RectTransform m_FrameInst;

	private bool Active
	{
		get
		{
			if (SKU.PS4UI)
			{
				return SKU.IsTeyon;
			}
			return false;
		}
	}

	public void Update()
	{
		if (!m_Spawner.IsSettingUp)
		{
			ReparentHudItems();
			base.enabled = false;
		}
	}

	public void ReparentHudItems()
	{
		if (Active)
		{
			m_FrameInst = Object.Instantiate(m_Prefab, base.transform, worldPositionStays: false);
			m_FrameInst.SetAsFirstSibling();
			for (int i = 0; i < m_SafeHudItems.Length; i++)
			{
				m_Hud.GetHudElement(m_SafeHudItems[i]).transform.SetParent(m_FrameInst, worldPositionStays: false);
			}
		}
	}

	public void OnSpawn()
	{
		base.enabled = true;
	}

	public void OnRecycle()
	{
		if (Active)
		{
			while (m_FrameInst.childCount > 0)
			{
				m_FrameInst.GetChild(m_FrameInst.childCount - 1).SetParent(null, worldPositionStays: true);
			}
			Object.Destroy(m_FrameInst.gameObject);
		}
	}
}
