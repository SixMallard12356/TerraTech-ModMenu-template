#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AutoSpriteCreate : Singleton.Manager<AutoSpriteCreate>
{
	private struct ItemTypeAndSpritePath
	{
		public ItemTypeInfo typeInfo;

		public string path;
	}

	public enum SpriteCaptureType
	{
		BuildOutOfDate,
		BuildMissing,
		ForceBuildAll,
		Disabled
	}

	[Serializable]
	private class TechPreviewData
	{
		public Vector3 cameraPosition;

		public Vector3 cameraRotation;

		public Vector3 cameraPanning;

		public TankPreset preset;

		public FactionSubTypes corp;
	}

	public int squareSize = 512;

	public int techPreviewImageSize = 1080;

	public Camera m_RenderCamPrefab;

	public Light m_LightPrefab;

	[SerializeField]
	private bool m_CreateScenerySprites;

	[SerializeField]
	private bool m_CreateSkinSprites;

	[SerializeField]
	private bool m_CreateItemSprites = true;

	[SerializeField]
	private string m_CustomSkinPreviewOutputDirectory = "Textures/Skins/Previews";

	[SerializeField]
	private ItemSpriteTable m_ItemSpriteTable;

	[SerializeField]
	private List<TechPreviewData> m_SkinPreviews;

	public const string DIR_NAME = "ItemSprites";

	private RenderTexture m_RenderTex;

	private Camera m_RenderCam;

	private Light m_Light;

	private Color[] m_VirtualColours;

	private Texture2D m_VirtualPhotoHolder;

	private bool m_SpritesLoaded;

	private EventNoParams m_SpritesLoadedEvent;

	public IEnumerator CreateAndLoadItemSprites()
	{
		while (!Singleton.Manager<ManSpawn>.inst.BlocksLoaded)
		{
			yield return null;
		}
		m_SpritesLoaded = true;
		m_SpritesLoadedEvent.Send();
		m_SpritesLoadedEvent.Clear();
	}

	public void DoOnceAfterSpritesLoaded(Action action)
	{
		if (m_SpritesLoaded)
		{
			action();
		}
		else
		{
			m_SpritesLoadedEvent.Subscribe(action);
		}
	}

	private IEnumerator<ItemTypeAndSpritePath> EnumerateItemSprites()
	{
		ChunkTypes[] array = Enum.GetValues(typeof(ChunkTypes)) as ChunkTypes[];
		for (int i = 0; i < array.Length; i++)
		{
			ChunkTypes chunkTypes = array[i];
			if (chunkTypes != ChunkTypes.Null)
			{
				yield return new ItemTypeAndSpritePath
				{
					typeInfo = new ItemTypeInfo(ObjectTypes.Chunk, (int)chunkTypes),
					path = "ItemSprites/Chunks/" + chunkTypes
				};
			}
		}
		BlockTypes[] loadedTankBlockNames = Singleton.Manager<ManSpawn>.inst.GetLoadedTankBlockNames();
		BlockTypes[] array2 = loadedTankBlockNames;
		for (int i = 0; i < array2.Length; i++)
		{
			BlockTypes itemType = array2[i];
			yield return new ItemTypeAndSpritePath
			{
				typeInfo = new ItemTypeInfo(ObjectTypes.Block, (int)itemType),
				path = "ItemSprites/Blocks/" + itemType
			};
		}
	}

	private void ApplyCamera(AutoSpriteRenderer targetVisible)
	{
		m_RenderCam.transform.forward = targetVisible.objectCenter - targetVisible.cameraPos;
		m_RenderCam.transform.position = targetVisible.cameraPos + targetVisible.transform.position + targetVisible.objectCenter;
		m_RenderCam.transform.Rotate(0f, 0f, targetVisible.cameraRoll, Space.Self);
	}

	private void ApplyCameraToTech(Tank tech, Vector3 cameraPosition, Vector3 cameraRotation, Vector3 cameraPanning)
	{
		m_RenderCam.transform.position = cameraPosition + tech.visible.transform.position + tech.visible.centrePosition;
		m_RenderCam.transform.LookAt(tech.visible.centrePosition);
		m_RenderCam.transform.rotation *= Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
		m_RenderCam.transform.position += m_RenderCam.transform.TransformVector(cameraPanning);
	}

	private Texture2D MakeImageFromCamera(int inpSquareSize, string debugName, bool fitImageToContent = true)
	{
		m_RenderCam.aspect = 1f;
		m_RenderCam.GetComponent<Camera>().targetTexture = m_RenderTex;
		m_RenderCam.GetComponent<Camera>().Render();
		RenderTexture.active = m_RenderTex;
		m_VirtualPhotoHolder.ReadPixels(new Rect(0f, 0f, inpSquareSize, inpSquareSize), 0, 0, recalculateMipMaps: true);
		m_VirtualPhotoHolder.Apply();
		m_VirtualColours = m_VirtualPhotoHolder.GetPixels();
		int num = int.MaxValue;
		int num2 = int.MinValue;
		int num3 = int.MaxValue;
		int num4 = int.MinValue;
		for (int i = 0; i < m_VirtualColours.Length; i++)
		{
			if (m_VirtualColours[i].a > 0f)
			{
				num = Mathf.Min(num, i % inpSquareSize);
				num2 = Mathf.Max(num2, i % inpSquareSize);
				num3 = Mathf.Min(num3, i / inpSquareSize);
				num4 = Mathf.Max(num4, i / inpSquareSize);
				Color color = m_VirtualColours[i];
				color.a = 1f;
				m_VirtualColours[i] = color;
			}
		}
		if (num == int.MaxValue)
		{
			d.LogWarning("No pixels found with alpha > 0 when rendering " + debugName);
			num = (num3 = 0);
			num2 = (num4 = 1);
		}
		int num5 = num2 - num;
		int num6 = num4 - num3;
		int num7 = 0;
		while (num5 != num6 && num7 < 5)
		{
			if (num5 > num6)
			{
				float f = (float)(num5 - num6) / 2f;
				int num8 = num3 - Mathf.FloorToInt(f);
				if (num8 < 0)
				{
					num3 = 0;
					num4 += Mathf.CeilToInt(f) + Mathf.Abs(num8);
				}
				else
				{
					num3 -= Mathf.FloorToInt(f);
					num4 += Mathf.CeilToInt(f);
				}
			}
			else
			{
				float f2 = (float)(num6 - num5) / 2f;
				int num9 = num - Mathf.FloorToInt(f2);
				if (num9 < 0)
				{
					num = 0;
					num2 += Mathf.CeilToInt(f2) + Mathf.Abs(num9);
				}
				else
				{
					num -= Mathf.FloorToInt(f2);
					num2 += Mathf.CeilToInt(f2);
				}
			}
			int num10 = num - Mathf.Max(0, num - 2) + 2;
			num = Mathf.Max(0, num - 2);
			num2 = Mathf.Min(inpSquareSize, num2 + 2 - num10);
			int num11 = num3 - Mathf.Max(0, num3 - 2) + 2;
			num3 = Mathf.Max(0, num3 - 2);
			num4 = Mathf.Min(inpSquareSize, num4 + 2 - num11);
			num5 = num2 - num;
			num6 = num4 - num3;
			num7++;
		}
		if (!fitImageToContent)
		{
			num2 = inpSquareSize;
			num4 = inpSquareSize;
			num3 = 0;
			num = 0;
			num5 = inpSquareSize;
			num6 = inpSquareSize;
		}
		Color[] array = new Color[num5 * num6];
		Texture2D texture2D = new Texture2D(num5, num6);
		int num12 = num + num3 * inpSquareSize;
		if (num6 != num5)
		{
			d.Log("Something is creating a non square texutre of size: " + num5 + "x" + num6);
		}
		for (int j = 0; j < array.Length; j++)
		{
			int num13 = j / num5;
			int num14 = j % num5;
			if (num14 + num12 + num13 * inpSquareSize > m_VirtualColours.Length)
			{
				d.LogWarning("Pixel index out of range, skipping");
			}
			else
			{
				array[j] = m_VirtualColours[num14 + num12 + num13 * inpSquareSize];
			}
		}
		texture2D.SetPixels(array);
		texture2D.Apply();
		RenderTexture.active = null;
		m_RenderCam.GetComponent<Camera>().targetTexture = null;
		m_RenderCam.ResetAspect();
		return texture2D;
	}

	private void SaveImage(Texture2D texture, string fullPathWithExtn)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(fullPathWithExtn));
			byte[] buffer = texture.EncodeToPNG();
			FileStream fileStream = File.Open(fullPathWithExtn, FileMode.Create);
			new BinaryWriter(fileStream).Write(buffer);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("Failed when saving image {0}, reason: {1}", fullPathWithExtn, ex);
		}
	}
}
