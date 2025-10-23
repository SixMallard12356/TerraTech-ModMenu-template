using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/BlockTypesOnTechAchievement")]
public class BlockTypesOnTechAchievement : AchievementObject
{
	[SerializeField]
	private int m_MinDifferentBlockTypes = 50;

	private Dictionary<int, int> m_BlockTypesOnCurrentTech = new Dictionary<int, int>();

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockAttached);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockDetached);
	}

	private void OnBlockTypeCountChanged()
	{
		if (IsActive() && m_BlockTypesOnCurrentTech.Count >= m_MinDifferentBlockTypes)
		{
			CompleteAchievement();
			Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
			Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnBlockAttached);
			Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Unsubscribe(OnBlockDetached);
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool playerFocus)
	{
		if (IsActive() && playerFocus)
		{
			m_BlockTypesOnCurrentTech.Clear();
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				int value = 0;
				value = ((!m_BlockTypesOnCurrentTech.TryGetValue((int)current.BlockType, out value)) ? 1 : (value + 1));
				m_BlockTypesOnCurrentTech[(int)current.BlockType] = value;
			}
			OnBlockTypeCountChanged();
		}
	}

	private void OnBlockAttached(Tank tech, TankBlock block)
	{
		if (IsActive() && tech == Singleton.playerTank)
		{
			int value = 0;
			value = ((!m_BlockTypesOnCurrentTech.TryGetValue((int)block.BlockType, out value)) ? 1 : (value + 1));
			m_BlockTypesOnCurrentTech[(int)block.BlockType] = value;
			OnBlockTypeCountChanged();
		}
	}

	private void OnBlockDetached(Tank tech, TankBlock block)
	{
		if (IsActive() && tech == Singleton.playerTank)
		{
			int value = 0;
			if (m_BlockTypesOnCurrentTech.TryGetValue((int)block.BlockType, out value))
			{
				value--;
			}
			if (value > 0)
			{
				m_BlockTypesOnCurrentTech[(int)block.BlockType] = value;
			}
			else
			{
				m_BlockTypesOnCurrentTech.Remove((int)block.BlockType);
			}
		}
	}
}
