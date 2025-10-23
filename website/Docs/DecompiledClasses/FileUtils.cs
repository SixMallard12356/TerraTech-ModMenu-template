#define UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;

public class FileUtils
{
	public const string k_SuffixPNG = ".png";

	public static bool SaveTexture(Texture2D texture, string path)
	{
		bool result = true;
		try
		{
			byte[] buffer = texture.EncodeToPNG();
			FileStream fileStream = File.Open(path, FileMode.Create);
			new BinaryWriter(fileStream).Write(buffer);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			d.LogError("Failed to write Texture to file: " + ex);
			result = false;
		}
		return result;
	}

	public static void LoadSnapshotTextureAsync(string path, Action<Texture2D> callback)
	{
		d.Log("[FileUtils.LoadTextureAsync]: " + path);
		SaveDataConsoles.LoadData(path, "Cache", delegate(bool success, byte[] bytes)
		{
			Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, mipChain: false);
			if (success)
			{
				texture2D.LoadImage(bytes);
			}
			callback(texture2D);
		});
	}

	public static Texture2D LoadTexture(string path)
	{
		Texture2D texture2D;
		try
		{
			byte[] data = File.ReadAllBytes(path);
			texture2D = new Texture2D(4, 4);
			texture2D.LoadImage(data);
		}
		catch (Exception ex)
		{
			d.LogError("FileUtils.LoadTexture failed: " + ex);
			texture2D = null;
		}
		return texture2D;
	}

	public static void DeleteFile(string path)
	{
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	public static void Rename(string oldPath, string newPath)
	{
		try
		{
			File.Move(oldPath, newPath);
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("FileUtils.Rename from {0} to {1} failed: {2}", oldPath, newPath, ex);
		}
	}

	public static string GetFilePath(string relativePath, string fileName, bool incrementCount, string suffix)
	{
		string text = ManSaveGame.GetSaveDataFolder() + "/" + relativePath;
		Directory.CreateDirectory(text);
		string path = Path.Combine(text, fileName + suffix);
		path = Path.GetFullPath(path);
		if (incrementCount)
		{
			int num = 0;
			do
			{
				string text2 = fileName + ((num < 1) ? "" : num.ToString());
				path = Path.Combine(text, text2 + suffix);
				num++;
			}
			while (File.Exists(path));
		}
		return path;
	}
}
