using UnityEngine;

public class uScript_SpawnResourceListOnHolder : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, ChunkTypes[] chunks, [DefaultValue(BlockTypes.GSOReceiver_111)] BlockTypes blockType)
	{
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (current.BlockType != blockType)
			{
				continue;
			}
			ModuleItemHolder component = current.GetComponent<ModuleItemHolder>();
			if ((bool)component)
			{
				Vector3 position = tech.trans.position + (tech.blockBounds.extents.y + 0.5f) * Vector3.up;
				for (int i = 0; i < chunks.Length; i++)
				{
					Visible visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(new ItemTypeInfo(ObjectTypes.Chunk, (int)chunks[i]), position, Quaternion.identity);
					position += Vector3.up;
					component.SingleStack.Take(visible, force: true);
					visible.trans.position = component.SingleStack.BasePosWorldOffsetLocal(Vector3.up * (1f + (float)component.SingleStack.NumItems * 0.7f));
				}
			}
			break;
		}
	}
}
