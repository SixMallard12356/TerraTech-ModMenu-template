#define UNITY_EDITOR
public class uScript_IsBlockMining : uScriptLogic
{
	private bool m_IsMining;

	private ModuleItemProducer m_ItemProducer;

	public bool True => m_IsMining;

	public bool False => !m_IsMining;

	public void In(TankBlock block)
	{
		if (block != null)
		{
			if (!m_ItemProducer)
			{
				m_ItemProducer = block.GetComponent<ModuleItemProducer>();
			}
			if (m_ItemProducer != null)
			{
				m_IsMining = m_ItemProducer.IsProducing;
			}
			else
			{
				d.LogError("uScript_IsBlockMining: block doesn't have an item producer module");
			}
		}
		else
		{
			d.LogError("uScript_IsBlockMining: block is null");
		}
	}

	public void OnEnable()
	{
		m_ItemProducer = null;
	}
}
