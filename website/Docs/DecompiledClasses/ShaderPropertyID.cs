using UnityEngine;

public class ShaderPropertyID
{
	private int m_ID = -1;

	private string m_PropertyName;

	public ShaderPropertyID(string name)
	{
		m_PropertyName = name;
	}

	public int GetID()
	{
		if (m_ID == -1)
		{
			m_ID = Shader.PropertyToID(m_PropertyName);
		}
		return m_ID;
	}
}
