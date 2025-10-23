#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zlib;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ManScreenshot : Singleton.Manager<ManScreenshot>
{
	public delegate void OnTechRendered(TechData techData, Texture2D texture);

	[Serializable]
	private class BlockSpecSerial
	{
		public IntVector3 pos;

		public int rot;

		public string block;

		public BlockTypes m_BlockType;

		public BlockSpecSerial(TankPreset.BlockSpec spec)
		{
			pos = spec.position;
			rot = spec.orthoRotation;
			block = spec.block;
			m_BlockType = spec.m_BlockType;
		}

		public TankPreset.BlockSpec ToSpec(bool overrideBlockType)
		{
			BlockTypes blockType = m_BlockType;
			if (overrideBlockType && !TankBlock.BlockNameToType(block, out blockType))
			{
				d.LogError("Can't find block type for: " + block);
			}
			return new TankPreset.BlockSpec
			{
				position = pos,
				orthoRotation = rot,
				block = block,
				m_BlockType = blockType
			};
		}
	}

	[SerializeField]
	private LayerMask m_UILayerMask;

	public int cropRectPad = 25;

	public AudioClip snapshotSound;

	public GUIManager.LabelWrapper supersizeDebugSize;

	public float maxImageWidth = 800f;

	public float maxImageHeight = 600f;

	public StaticTechSnapshotCamera m_TechPresetCameraPrefab;

	public const string k_SnapshotDirName = "Snapshots";

	public const string k_CacheDirName = "Cache";

	public const string k_SuffixTechData = ".tdc";

	private const string kFormatIdentifier = "TTTechData";

	private const byte kCurrentFormatVersion = 0;

	private const int k_BitsStoredPerPixel = 3;

	private StaticTechSnapshotCamera m_TechPresetCamera;

	private int m_TechRenderLayer;

	private List<int> m_RenderableTechStoredLayers = new List<int>(20);

	private const int k_SteamThumbnailWidth = 640;

	private const int k_SteamThumbnailHeight = 360;

	private float m_supersizeTextTimer;

	private int m_supersizeCachedSize;

	public static Event<string> SnapshotConversionStartedEvent;

	public static Event<string, bool> SnapshotConversionCompletedEvent;

	private static string s_LastConvertedSnapshotPath = null;

	private static bool s_LastConversionSuccess = false;

	private static string s_SnapshotConverterPath = null;

	public bool TakingSnapshot { get; set; }

	public IntVector2 DefaultSnapshotSize => new IntVector2(640, 400);

	public static bool s_BlockExecutionDuringConversion { get; set; } = true;

	public static bool s_IsConvertingSnapshotAsync { get; private set; } = false;

	public void TakeSnapshotAndShowUI(TrackedVisible targetOverride = null)
	{
		if (Singleton.Manager<ManTechSwapper>.inst.CheckOperationInProgress())
		{
			return;
		}
		if (Singleton.Manager<ManSnapshots>.inst.ServiceDisk.GetSnapshotCollectionDisk().Snapshots.Count >= Singleton.Manager<ManSnapshots>.inst.MaxDiskSnapshots)
		{
			Singleton.Manager<ManUI>.inst.ShowErrorPopup(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Notifications.SnapshotMaxSnapshotsReached));
			return;
		}
		TakingSnapshot = true;
		UIScreenSnapshot nameVehicleScreen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NameVehicle) as UIScreenSnapshot;
		RenderTechImage(targetOverride, nameVehicleScreen.SnapshotSize, encodeTechData: true, delegate(TechData techData, Texture2D techImage)
		{
			if (techImage == null || techData == null)
			{
				TakingSnapshot = false;
				Singleton.Manager<ManUI>.inst.ShowErrorPopup(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.Notifications.SnapshotSaveFail));
			}
			else
			{
				nameVehicleScreen.InitialiseSnapshotData(techImage, techData, targetOverride);
				Singleton.Manager<ManUI>.inst.GoToScreen(nameVehicleScreen, ManUI.PauseType.Pause);
			}
		});
	}

	public static string GetSnapshotPath()
	{
		return Path.GetFullPath(Path.Combine(ManSaveGame.GetSaveDataFolder(), "Snapshots"));
	}

	private IEnumerator RenderSnapshotFromScreenCoroutine(bool encodeTechData, OnTechRendered callback)
	{
		yield return new WaitForEndOfFrame();
		Texture2D texture2D = null;
		TechData techData = null;
		if ((bool)Singleton.playerTank)
		{
			techData = new TechData();
			techData.SaveTech(Singleton.playerTank);
			Singleton.Manager<ManUI>.inst.m_UICamera.enabled = false;
			int cullingMask = Singleton.camera.cullingMask;
			int num = 1 << LayerMask.NameToLayer("UI");
			Singleton.camera.cullingMask &= ~num;
			bool isGraphicOptionEnabled = Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.DOF);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: false);
			bool isGraphicOptionEnabled2 = Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, enabled: false);
			RenderTexture temporary = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
			RenderTexture targetTexture = Singleton.camera.targetTexture;
			RenderTexture.active = temporary;
			Singleton.camera.targetTexture = temporary;
			Singleton.camera.Render();
			TechData.SerializedSnapshotData serializedSnapshotData = new TechData.SerializedSnapshotData(techData);
			byte[] array = null;
			int pixelsRequired = 0;
			if (encodeTechData && Singleton.Manager<ManSnapshots>.inst.EmbedSnapshotsInPNGs)
			{
				array = GetCompressedPreset(serializedSnapshotData);
				pixelsRequired = CalcPixelsNeededToStorePresetData(array);
			}
			Rect croppedScreenshotRect = GetCroppedScreenshotRect(Singleton.playerTank, Singleton.camera, Screen.width, Screen.height, pixelsRequired);
			texture2D = new Texture2D((int)croppedScreenshotRect.width, (int)croppedScreenshotRect.height, TextureFormat.RGB24, mipChain: false);
			texture2D.ReadPixels(new Rect(croppedScreenshotRect.xMin, croppedScreenshotRect.yMin, croppedScreenshotRect.width, croppedScreenshotRect.height), 0, 0);
			texture2D.Apply();
			Singleton.camera.targetTexture = targetTexture;
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
			Singleton.Manager<ManUI>.inst.m_UICamera.enabled = true;
			Singleton.camera.cullingMask = cullingMask;
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, isGraphicOptionEnabled);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, isGraphicOptionEnabled2);
			if (encodeTechData && Singleton.Manager<ManSnapshots>.inst.EmbedSnapshotsInPNGs && array != null)
			{
				EncodeCompressedPreset(array, texture2D);
			}
		}
		callback(techData, texture2D);
	}

	public void RenderTechImage(Tank tech, IntVector2 size, bool encodeTechData, OnTechRendered callback)
	{
		StartCoroutine(RenderTechImageCoroutine(tech, null, size, encodeTechData, callback));
	}

	public void RenderTechImage(TechData techData, IntVector2 size, bool encodeTechData, OnTechRendered callback)
	{
		StartCoroutine(RenderTechImageCoroutine(null, techData, size, encodeTechData, callback));
	}

	public void RenderTechImage(TrackedVisible targetOverride, IntVector2 size, bool encodeTechData, OnTechRendered callback)
	{
		if (targetOverride == null)
		{
			StartCoroutine(RenderSnapshotFromScreenCoroutine(encodeTechData, callback));
			return;
		}
		if ((bool)targetOverride.visible && (bool)targetOverride.visible.tank)
		{
			RenderTechImage(targetOverride.visible.tank, size, encodeTechData, callback);
			return;
		}
		TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(targetOverride);
		if (storedTechData != null)
		{
			RenderTechImage(storedTechData, size, encodeTechData, callback);
			return;
		}
		d.LogErrorFormat("RenderSnapshot unable to load stored tech data for ID {0}", targetOverride.ID);
		callback(null, null);
	}

	private IEnumerator RenderTechImageCoroutine(Tank tech, TechData techData, IntVector2 size, bool encodeTechData, OnTechRendered callback)
	{
		if (tech == null && techData == null)
		{
			d.LogError("RenderTechImage called with neither tech nor techData");
			callback(null, null);
			yield break;
		}
		if (techData == null)
		{
			techData = new TechData();
			techData.SaveTech(tech);
		}
		yield return new WaitForEndOfFrame();
		Texture2D texture;
		if (tech == null)
		{
			tech = CreateRenderableTech(techData, m_TechRenderLayer);
			texture = RenderSnapshotFromTechDataInternal(tech, techData, encodeTechData, size);
			RecycleRenderableTech(tech);
		}
		else
		{
			SetRenderLayer(tech, m_TechRenderLayer);
			texture = RenderSnapshotFromTechDataInternal(tech, techData, encodeTechData, size);
			ResetRenderLayer(tech);
		}
		callback(techData, texture);
	}

	public Texture2D RenderThumbnailSteam(Texture2D source)
	{
		return RenderThumbnail(source, 640, 360);
	}

	public static void EncodeSnapshotRender(TechData techData, Texture2D texture)
	{
		EncodeCompressedPreset(GetCompressedPreset(new TechData.SerializedSnapshotData(techData)), texture);
	}

	public static bool TryDecodeSnapshotRender(Texture2D snapshotRender, out TechData.SerializedSnapshotData techSnapshotData, string filename = null, bool convertLegacy = true)
	{
		bool flag = false;
		if (TryDecodeSnapshotRenderInternal(snapshotRender, out techSnapshotData, filename))
		{
			flag = true;
		}
		else if (convertLegacy)
		{
			if (filename == null)
			{
				filename = Path.GetTempFileName();
				FileUtils.SaveTexture(snapshotRender, filename);
			}
			if (!filename.NullOrEmpty() && RunSnapshotConversionTool(filename))
			{
				snapshotRender = FileUtils.LoadTexture(filename);
				if (TryDecodeSnapshotRenderInternal(snapshotRender, out techSnapshotData, filename))
				{
					flag = true;
				}
			}
		}
		if (!flag)
		{
			techSnapshotData = default(TechData.SerializedSnapshotData);
		}
		return flag;
	}

	private static bool RunSnapshotConversionTool(string snapshotPath)
	{
		d.Assert(s_BlockExecutionDuringConversion || !s_IsConvertingSnapshotAsync, "RunSnapshotConversionTool Called while already converting another snapshot! Aborting conversion!");
		if (!s_BlockExecutionDuringConversion && s_IsConvertingSnapshotAsync)
		{
			return false;
		}
		s_IsConvertingSnapshotAsync = !s_BlockExecutionDuringConversion;
		SnapshotConversionStartedEvent.Send(snapshotPath);
		if (s_SnapshotConverterPath == null)
		{
			s_SnapshotConverterPath = Path.GetFullPath(new Uri(Application.dataPath + "/../Tools/SnapshotLegacyFormatConverter/bin/x64/SnapshotLegacyFormatConverter.exe").LocalPath);
		}
		bool flag;
		try
		{
			d.LogFormat("Attempting to run snapshot conversion tool from '{0}' for snapshot located at '{1}'", s_SnapshotConverterPath, snapshotPath);
			Process converterProcess = new Process();
			converterProcess.StartInfo.FileName = s_SnapshotConverterPath;
			converterProcess.StartInfo.Arguments = "-batchmode -nographics -snapshotPath \"" + snapshotPath + "\"";
			converterProcess.StartInfo.CreateNoWindow = true;
			converterProcess.StartInfo.RedirectStandardError = true;
			converterProcess.StartInfo.RedirectStandardOutput = true;
			converterProcess.StartInfo.UseShellExecute = false;
			if (!s_BlockExecutionDuringConversion)
			{
				converterProcess.EnableRaisingEvents = true;
				converterProcess.Exited += delegate
				{
					s_LastConvertedSnapshotPath = snapshotPath;
					s_LastConversionSuccess = converterProcess.ExitCode == 0;
				};
			}
			converterProcess.Start();
			if (s_BlockExecutionDuringConversion)
			{
				converterProcess.WaitForExit(5000);
				int exitCode = converterProcess.ExitCode;
				flag = exitCode == 0;
				if (!flag)
				{
					d.LogError("Snapshot conversion failed with code: " + exitCode + " output: " + converterProcess.StandardOutput.ReadToEnd());
				}
				s_IsConvertingSnapshotAsync = false;
				SnapshotConversionCompletedEvent.Send(snapshotPath, flag);
			}
			else
			{
				flag = false;
			}
		}
		catch (Exception ex)
		{
			d.LogError("RunSnapshotConversionTool - failed with exception: " + ex);
			flag = false;
			s_IsConvertingSnapshotAsync = false;
			SnapshotConversionCompletedEvent.Send(snapshotPath, paramB: false);
		}
		return flag;
	}

	private static bool TryDecodeSnapshotRenderInternal(Texture2D snapshotRender, out TechData.SerializedSnapshotData techSnapshotData, string filename)
	{
		bool flag = !SKU.ConsoleUI;
		d.Assert(flag, "TryDecodeSnapshotRender not supported when running console-style");
		if (snapshotRender == null || !flag)
		{
			techSnapshotData = default(TechData.SerializedSnapshotData);
			return false;
		}
		bool flag2 = false;
		Color32[] pixels = snapshotRender.GetPixels32();
		try
		{
			byte[] bytes = Encoding.ASCII.GetBytes("TTTechData");
			int num = 0;
			num += DecodeFromPixels(pixels, num, bytes.Length, out var messageBytes);
			if (messageBytes.SequenceEqual(bytes))
			{
				num += DecodeFromPixels(pixels, num, 1, out messageBytes);
				_ = messageBytes[0];
				num += DecodeFromPixels(pixels, num, 4, out messageBytes);
				int numBytes = BitConverter.ToInt32(messageBytes, 0);
				num += DecodeFromPixels(pixels, num, numBytes, out messageBytes);
				string rawJson;
				using (MemoryStream stream = new MemoryStream(messageBytes, writable: false))
				{
					using GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
					using StreamReader streamReader = new StreamReader(stream2);
					rawJson = streamReader.ReadLine();
				}
				TechData.SerializedSnapshotData objectToLoad = default(TechData.SerializedSnapshotData);
				ManSaveGame.LoadObjectFromRawJson(ref objectToLoad, rawJson);
				flag2 = true;
				techSnapshotData = objectToLoad;
			}
			else
			{
				d.Log("TryDecodeSnapshotRenderInternal - Skipping potential snapshot file '" + filename + "' as it does not have the correct format identifier.");
				flag2 = false;
				techSnapshotData = default(TechData.SerializedSnapshotData);
			}
		}
		catch (Exception ex)
		{
			d.LogWarning("Deserialisation exception: " + ex.ToString());
			flag2 = false;
			techSnapshotData = default(TechData.SerializedSnapshotData);
		}
		return flag2;
	}

	public void PlayFlash()
	{
		Singleton.Manager<ManUI>.inst.DoFlash(0.1f, 0.2f);
		GetComponent<AudioSource>().PlayOneShot(snapshotSound);
	}

	private Texture2D RenderSnapshotFromTechDataInternal(Tank tech, TechData techData, bool encodeTechData, IntVector2 size)
	{
		int x = size.x;
		int y = size.y;
		m_TechPresetCamera.gameObject.SetActive(value: true);
		m_TechPresetCamera.FrameTech(tech);
		RenderTexture temporary = RenderTexture.GetTemporary(x, y, 24, RenderTextureFormat.ARGB32);
		RenderTexture targetTexture = Singleton.camera.targetTexture;
		RenderTexture.active = temporary;
		m_TechPresetCamera.Camera.targetTexture = temporary;
		m_TechPresetCamera.Camera.Render();
		Texture2D texture2D;
		if (encodeTechData && Singleton.Manager<ManSnapshots>.inst.EmbedSnapshotsInPNGs)
		{
			byte[] compressedPreset = GetCompressedPreset(new TechData.SerializedSnapshotData(techData));
			Rect rect = GetCroppedScreenshotRect(pixelsRequired: CalcPixelsNeededToStorePresetData(compressedPreset), tech: tech, camera: m_TechPresetCamera.Camera, viewWidth: x, viewHeight: y);
			texture2D = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.ARGB32, mipChain: false);
			texture2D.ReadPixels(new Rect(rect.xMin, rect.yMin, rect.width, rect.height), 0, 0);
			texture2D.Apply();
			EncodeCompressedPreset(compressedPreset, texture2D);
		}
		else
		{
			texture2D = new Texture2D(x, y, TextureFormat.ARGB32, mipChain: false);
			texture2D.ReadPixels(new Rect(0f, 0f, x, y), 0, 0);
			texture2D.Apply();
		}
		m_TechPresetCamera.Camera.targetTexture = targetTexture;
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(temporary);
		m_TechPresetCamera.gameObject.SetActive(value: false);
		return texture2D;
	}

	public static Texture2D RenderThumbnail(Texture2D source, int width, int height)
	{
		float num = (float)width / (float)height;
		float num2 = (float)source.width / (float)source.height;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (num >= num2)
		{
			num3 = source.width;
			num4 = Mathf.RoundToInt((float)num3 / num);
			num5 = 0;
			num6 = Mathf.RoundToInt((float)(source.height - num4) / 2f);
		}
		else
		{
			num4 = source.height;
			num3 = Mathf.RoundToInt((float)num4 * num);
			num5 = Mathf.RoundToInt((float)(source.width - num3) / 2f);
			num6 = 0;
		}
		Texture2D texture2D = new Texture2D(num3, num4, TextureFormat.RGBA32, mipChain: false);
		texture2D.SetPixels(source.GetPixels(num5, num6, num3, num4));
		TextureScale.Bilinear(texture2D, width, height);
		return texture2D;
	}

	private Tank CreateRenderableTech(TechData techData, int renderLayer)
	{
		ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
		{
			techData = techData,
			teamID = -2,
			position = Vector3.zero,
			rotation = Quaternion.identity,
			grounded = false
		};
		Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnUnmanagedTank(param);
		InitRenderableTech(tank, renderLayer);
		return tank;
	}

	private void SetRenderLayer(Tank renderableTech, int renderLayer)
	{
		IterateAllChildren(renderableTech.trans, delegate(Transform t)
		{
			m_RenderableTechStoredLayers.Add(t.gameObject.layer);
			t.gameObject.layer = renderLayer;
		});
	}

	private void ResetRenderLayer(Tank tech)
	{
		int listIdx = 0;
		IterateAllChildren(tech.trans, delegate(Transform t)
		{
			t.gameObject.layer = m_RenderableTechStoredLayers[listIdx];
			int num = listIdx + 1;
			listIdx = num;
		});
		m_RenderableTechStoredLayers.Clear();
	}

	private void InitRenderableTech(Tank renderableTech, int renderLayer)
	{
		renderableTech.EnableGravity = false;
		renderableTech.rbody.isKinematic = true;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = renderableTech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.visible.EnablePhysics(enable: false);
		}
		renderableTech.AI.enabled = false;
		SetRenderLayer(renderableTech, renderLayer);
	}

	private void RecycleRenderableTech(Tank tech)
	{
		tech.EnableGravity = true;
		tech.rbody.isKinematic = false;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.visible.EnablePhysics(enable: true);
		}
		tech.AI.enabled = true;
		ResetRenderLayer(tech);
		tech.visible.RemoveFromGame();
	}

	private void IterateAllChildren(Transform transform, UnityAction<Transform> childHandler)
	{
		foreach (Transform item in transform)
		{
			childHandler(item);
			IterateAllChildren(item, childHandler);
		}
	}

	private IEnumerator DoScreenGrab(int supersize = 1)
	{
		TakeScreenshot("screens", "screen", supersize);
		yield return null;
		PlayFlash();
	}

	private Rect GetCroppedScreenshotRect(Tank tech, Camera camera, int viewWidth, int viewHeight, int pixelsRequired)
	{
		Rect correctedRect = GetCorrectedRect(tech, camera, viewWidth, viewHeight);
		int num = (int)correctedRect.height;
		int num2 = (int)correctedRect.width;
		float num3 = (float)num2 / (float)num;
		int num4 = Mathf.CeilToInt(Mathf.Sqrt((float)pixelsRequired / num3));
		int b = Mathf.CeilToInt((float)num4 * num3);
		if ((float)num2 > maxImageWidth)
		{
			float num5 = Mathf.Max(num2, b);
			float num6 = maxImageWidth / num5;
			num = (int)((float)num * num6);
			num2 = (int)((float)num2 * num6);
		}
		if ((float)num > maxImageHeight)
		{
			float num7 = Mathf.Max(num, num4);
			float num8 = maxImageHeight / num7;
			num2 = (int)((float)num2 * num8);
			num = (int)((float)num * num8);
		}
		if (num * num2 < pixelsRequired)
		{
			num2 = Mathf.CeilToInt((float)num4 * num3);
			num = Mathf.CeilToInt(num4);
			d.LogError("ManScreenshot.GetCroppedScreenshot - Pixels Required > Cropped Pixels. Resizing.");
		}
		Vector3 vector = camera.WorldToScreenPoint(tech.boundsCentreWorldNoCheck);
		vector.y = (float)viewHeight - vector.y;
		correctedRect.x = Mathf.Clamp(vector.x - (float)num2 * 0.5f, correctedRect.xMin, correctedRect.xMax - (float)num2);
		correctedRect.y = Mathf.Clamp(vector.y - (float)num * 0.5f, correctedRect.yMin, correctedRect.yMax - (float)num);
		correctedRect.width = num2;
		correctedRect.height = num;
		return correctedRect;
	}

	public Rect GetCorrectedRect(Tank tech, Camera camera, int viewWidth, int viewHeight)
	{
		Bounds blockBounds = tech.blockBounds;
		Vector3 center = blockBounds.center;
		Vector3 a = blockBounds.extents + Vector3.one * 0.5f;
		Vector3[] obj = new Vector3[8]
		{
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(-1f, 1f, 1f),
			new Vector3(1f, 1f, 1f)
		};
		Vector2 vector = new Vector2(viewWidth, viewHeight);
		Vector2 vector2 = vector;
		Vector2 vector3 = Vector2.zero;
		Vector3[] array = obj;
		foreach (Vector3 b in array)
		{
			Vector3 position = center + Vector3.Scale(a, b);
			Vector3 position2 = tech.trans.TransformPoint(position);
			Vector2 rhs = camera.WorldToScreenPoint(position2).ToVector2XY();
			rhs.y = vector.y - rhs.y;
			vector2 = Vector2.Min(vector2, rhs);
			vector3 = Vector2.Max(vector3, rhs);
		}
		vector2 -= Vector2.one * cropRectPad;
		vector3 += Vector2.one * cropRectPad;
		vector2 = vector2.Clamp(Vector2.zero, vector);
		vector3 = vector3.Clamp(Vector2.zero, vector);
		return new Rect(vector2, vector3 - vector2);
	}

	private static byte[] GetCompressedPreset(TechData.SerializedSnapshotData serializedSnapshotData)
	{
		string value = ManSaveGame.SaveObjectToRawJson(serializedSnapshotData);
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream stream = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			using StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.WriteLine(value);
		}
		return memoryStream.GetBuffer();
	}

	private static int CalcPixelsNeededToStorePresetData(byte[] compressedSerialisedPreset)
	{
		if (Singleton.Manager<ManSnapshots>.inst.EmbedSnapshotsInPNGs)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("TTTechData");
			return 0 + CalcPixelsNeededForByteArray(bytes.Length) + CalcPixelsNeededForByteArray(1) + CalcPixelsNeededForByteArray(4) + CalcPixelsNeededForByteArray(compressedSerialisedPreset.Length);
		}
		return 0;
	}

	private static int CalcPixelsNeededForByteArray(int numBytes)
	{
		return (numBytes * 8 + 2) / 3;
	}

	private static bool EncodeCompressedPreset(byte[] serialisedPresetData, Texture2D texture)
	{
		Color32[] pixels = texture.GetPixels32();
		byte[] bytes = BitConverter.GetBytes(serialisedPresetData.Length);
		int offset = 0;
		byte[] bytes2 = Encoding.ASCII.GetBytes("TTTechData");
		int num;
		if (true && EncodeInPixels(pixels, ref offset, bytes2) && EncodeInPixels(pixels, ref offset, new byte[1]) && EncodeInPixels(pixels, ref offset, bytes))
		{
			num = (EncodeInPixels(pixels, ref offset, serialisedPresetData) ? 1 : 0);
			if (num != 0)
			{
				d.Assert(offset == CalcPixelsNeededToStorePresetData(serialisedPresetData), "EncodeCompressedPreset is storing a different amount of data as calculated by CalcPixelsNeededToStoreCompressedPreset");
				int num2 = pixels.Length / 2 + ((texture.height % 2 == 0) ? (texture.width / 2) : 0);
				Color32 color = pixels[num2];
				color.a = (byte)(color.a & -8);
				pixels[num2] = color;
				texture.SetPixels32(pixels);
				texture.Apply();
			}
		}
		else
		{
			num = 0;
		}
		d.Assert((byte)num != 0, "ManScreenshot.EncodeCompressedPreset - Failed to encode snapshot data in image!");
		return (byte)num != 0;
	}

	private static Color32 EncodeLSB(Color32 col, int channel, bool bit)
	{
		switch (channel)
		{
		case 0:
			col.r = (byte)(bit ? (col.r | 1) : (col.r & -2));
			break;
		case 1:
			col.g = (byte)(bit ? (col.g | 1) : (col.g & -2));
			break;
		case 2:
			col.b = (byte)(bit ? (col.b | 1) : (col.b & -2));
			break;
		case 3:
			col.a = (byte)(bit ? (col.a | 1) : (col.a & -2));
			break;
		}
		return col;
	}

	private static bool EncodeInPixels(Color32[] carrierPixels, ref int offset, byte[] messageBytes)
	{
		if ((carrierPixels.Length - offset) * 3 >= messageBytes.Length * 8)
		{
			BitArray bitArray = new BitArray(messageBytes);
			int num = offset;
			int num2 = 0;
			for (int i = 0; i < bitArray.Length; i++)
			{
				carrierPixels[num] = EncodeLSB(carrierPixels[num], num2++, bitArray[i]);
				if (num2 == 3)
				{
					num2 = 0;
					num++;
				}
			}
			offset = ((num2 == 0) ? num : (num + 1));
			return true;
		}
		d.LogError("EncodeInPixels: Insufficient capacity to Encode data");
		return false;
	}

	private static bool DecodeLSB(Color32 col, int channel)
	{
		switch (channel)
		{
		case 1:
			return (col.g & 1) == 1;
		case 2:
			return (col.b & 1) == 1;
		case 3:
			return (col.a & 1) == 1;
		case 0:
			return (col.r & 1) == 1;
		default:
			d.LogWarning("invalid channel " + channel);
			return false;
		}
	}

	private static int DecodeFromPixels(Color32[] carrierPixels, int offset, int numBytes, out byte[] messageBytes)
	{
		BitArray bitArray = new BitArray(numBytes * 8);
		int num = offset;
		int num2 = 0;
		for (int i = 0; i < bitArray.Length; i++)
		{
			bitArray[i] = DecodeLSB(carrierPixels[num], num2++);
			if (num2 == 3)
			{
				num2 = 0;
				num++;
			}
		}
		messageBytes = new byte[numBytes];
		bitArray.CopyTo(messageBytes, 0);
		return (bitArray.Length + 2) / 3;
	}

	private string TakeScreenshot(string relativePath, string screenShotFileStub, int supersize = 1)
	{
		string filePath = FileUtils.GetFilePath(relativePath, screenShotFileStub, incrementCount: true, ".png");
		ScreenCapture.CaptureScreenshot(filePath, supersize);
		return filePath;
	}

	private void Start()
	{
		TakingSnapshot = false;
		m_TechPresetCamera = UnityEngine.Object.Instantiate(m_TechPresetCameraPrefab);
		m_TechPresetCamera.gameObject.SetActive(value: false);
		m_TechRenderLayer = LayerMask.NameToLayer("RenderImageForPrefab");
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (s_LastConvertedSnapshotPath != null)
		{
			s_IsConvertingSnapshotAsync = false;
			SnapshotConversionCompletedEvent.Send(s_LastConvertedSnapshotPath, s_LastConversionSuccess);
			s_LastConvertedSnapshotPath = null;
			s_LastConversionSuccess = false;
		}
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManGameMode>.inst.LockPlayerControls)
		{
			flag = false;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(30))
		{
			StartCoroutine(DoScreenGrab());
		}
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Input.GetKeyDown(KeyCode.F12) || (flag && Input.GetKeyDown(KeyCode.Slash))))
		{
			StartCoroutine(DoScreenGrab(Singleton.Manager<DebugUtil>.inst.supersizedScreenshotSize));
		}
	}
}
