using UnityEngine;

public class uScript_SpawnResourceAtPosition : uScriptLogic
{
	public bool Out => true;

	public void In(ChunkTypes chunkType, int quantity, Vector3 position)
	{
		if ((bool)Singleton.playerTank)
		{
			Plane plane = Util.CalculateFrustumPlanes(Singleton.camera)[3];
			float enter = 0f;
			plane.Raycast(new Ray(position, Vector3.up), out enter);
			enter += 1f;
			Vector3 position2 = position + enter * Vector3.up;
			Vector3 vector = new Vector3(0f, 1.5f, 0f);
			for (int i = 0; i < quantity; i++)
			{
				Singleton.Manager<ManSpawn>.inst.SpawnItem(new ItemTypeInfo(ObjectTypes.Chunk, (int)chunkType), position2, Quaternion.identity);
				position2 += vector;
			}
		}
	}
}
