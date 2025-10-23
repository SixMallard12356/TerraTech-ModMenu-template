#define UNITY_EDITOR
using System;
using UnityEngine;

namespace Payload.UI.Commands.Steam;

public class SteamCreateRenderCommand : Command<SteamUploadData>
{
	public override void Execute(SteamUploadData data)
	{
		try
		{
			if (data.m_SnapshotInput.IsNotNull() && data.m_TechDataInput != null)
			{
				data.m_TechData = data.m_TechDataInput;
				data.m_CaptureItemPreview = Singleton.Manager<ManScreenshot>.inst.RenderThumbnailSteam(data.m_SnapshotInput);
				SetComplete(data);
			}
			else if (data.m_Category == SteamItemCategory.Techs)
			{
				Singleton.Manager<ManScreenshot>.inst.RenderTechImage((TrackedVisible)null, Singleton.Manager<ManScreenshot>.inst.DefaultSnapshotSize, encodeTechData: false, (ManScreenshot.OnTechRendered)delegate(TechData techData, Texture2D techImage)
				{
					data.m_CaptureItemPreview = Singleton.Manager<ManScreenshot>.inst.RenderThumbnailSteam(techImage);
					data.m_TechData = techData;
					SetComplete(data);
				});
			}
			else
			{
				data.m_CaptureItemPreview = Singleton.Manager<ManScreenshot>.inst.RenderThumbnailSteam(data.m_SaveInfo.LastScreenshot);
				SetComplete(data);
			}
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("SteamCreateRenderCommand - could not create render for {0} Error msg: {1}", data.m_SteamItem.m_Name, ex.Message);
			SetCancelled(data);
		}
	}
}
