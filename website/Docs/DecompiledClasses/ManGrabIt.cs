using System;
using UnityEngine;

public class ManGrabIt : Singleton.Manager<ManGrabIt>
{
	[SerializeField]
	private bool m_Active;

	[Tooltip("This takes a GrabItController object!")]
	[SerializeField]
	private GameObject m_GrabItControllerPrefab;

	private Texture2D m_Snapshot;

	private string m_Url;

	private Event<bool> GrabItInitialisedEvent;

	public void PrepareTechGrab(Tank tech)
	{
	}

	public void SetInitCallback(Action<bool> callback)
	{
		GrabItInitialisedEvent.Clear();
		GrabItInitialisedEvent.Subscribe(callback);
	}

	public void DoGrab(Texture2D thumbnail)
	{
	}

	private void OnInitialise(bool success)
	{
		bool paramA = true;
		if (!success)
		{
			paramA = false;
		}
		GrabItInitialisedEvent.Send(paramA);
	}

	private void Init()
	{
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockChange);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockChange);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnTankChange);
	}

	private void PrintUpload()
	{
	}

	private void PrintUploadStart(string _s)
	{
	}

	private void OnTankChange(Tank tank, bool entered)
	{
	}

	private void OnBlockChange(Tank tank, TankBlock block)
	{
	}

	private void OnCacheURL(string url)
	{
		m_Url = url;
	}

	private void OnUploadedFailed(int code, string message)
	{
		UIGrabitNotification uIGrabitNotification = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.GrabItNotification) as UIGrabitNotification;
		if ((bool)uIGrabitNotification)
		{
			string errorMessage = "GrabIt Error - UploadFailed:\n" + message;
			uIGrabitNotification.ShowError(errorMessage);
		}
	}

	private void Awake()
	{
	}
}
