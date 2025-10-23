using UnityEngine;

public class PlayerParticleEnabler : MonoBehaviour
{
	private ParticleSystem m_Particles;

	private Visible m_Visible;

	private void OnPlayerTankSwitched(Tank tank, bool setNotCleared)
	{
		if (m_Particles != null)
		{
			bool flag = setNotCleared && (bool)m_Visible && m_Visible.isActive && m_Visible.type == ObjectTypes.Block && m_Visible.block.tank == tank;
			m_Particles.SetEmissionEnabled(flag);
		}
	}

	private void OnPool()
	{
		m_Particles = GetComponent<ParticleSystem>();
		m_Visible = Visible.FindVisibleUpwards(this);
	}

	private void OnSpawn()
	{
		OnPlayerTankSwitched(Singleton.playerTank, setNotCleared: true);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankSwitched);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankSwitched);
	}
}
