using UnityEngine;

[FriendlyName("Get Named Block")]
[NodePath("TerraTech/Actions/Blocks")]
public class uScript_GetNamedBlock : uScriptLogic
{
	private TrackedVisible m_Block;

	private bool m_Exists;

	private bool m_Loaded;

	private bool m_Killed;

	private Encounter m_DataComponent;

	public bool Out => true;

	public bool Destroyed => m_Killed;

	public bool BlockExists
	{
		get
		{
			if (m_Block != null)
			{
				return m_Block.visible != null;
			}
			return false;
		}
	}

	public bool WaitingForBlock
	{
		get
		{
			if (m_Exists && !m_Killed)
			{
				if (m_Block != null)
				{
					return m_Block.visible == null;
				}
				return true;
			}
			return false;
		}
	}

	public bool NoBlock
	{
		get
		{
			if (!m_Exists)
			{
				return !m_Killed;
			}
			return false;
		}
	}

	public TankBlock In(string name, GameObject owner)
	{
		if (!m_Loaded && (bool)owner)
		{
			if (!m_DataComponent)
			{
				m_DataComponent = owner.GetComponent<Encounter>();
			}
			if ((bool)m_DataComponent)
			{
				EncounterVisibleData visible = m_DataComponent.GetVisible(name);
				if ((bool)visible)
				{
					if (visible.m_VisibleId == -2)
					{
						m_Killed = true;
						m_Block = null;
					}
					else
					{
						m_Block = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.m_VisibleId);
						m_Loaded = true;
						m_Exists = true;
						m_Killed = false;
					}
				}
				else
				{
					m_Exists = false;
				}
			}
		}
		if (m_Block != null && m_Block.wasDestroyed)
		{
			m_Killed = true;
			m_Block = null;
		}
		if (m_Killed)
		{
			m_Loaded = false;
		}
		if (m_Block != null)
		{
			if (!(m_Block.visible == null))
			{
				return m_Block.visible.block;
			}
			return null;
		}
		return null;
	}

	public void OnDisable()
	{
		m_Exists = false;
		m_Loaded = false;
		m_Killed = false;
		m_Block = null;
	}
}
