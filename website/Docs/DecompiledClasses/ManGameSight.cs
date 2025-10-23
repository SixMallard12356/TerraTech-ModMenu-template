using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

internal class ManGameSight : Singleton.Manager<ManGameSight>
{
	private struct Identifiers
	{
		public string resolution { get; set; }

		public string os { get; set; }

		public string language { get; set; }

		public string timezone { get; set; }
	}

	private struct GameLaunchBody
	{
		public string user_id;

		public string type;

		public Identifiers identifiers;
	}

	private const string c_GamesightProxyURL = "https://thjw6ce6x1.execute-api.us-west-2.amazonaws.com/game-terratech/events";

	private const string c_LaunchEventType = "game_launch";

	private const string c_GameSightApiVersion = "1.1.0";

	private const string c_GameSightAPIKey = "1d157ca247314276d1417ec1c3671cc6";

	private string m_PlayerUID;

	private Identifiers m_Identifiers;

	private HttpClient m_Client = new HttpClient();

	public void InitIdentifiers()
	{
		m_Identifiers.resolution = Screen.width + "x" + Screen.height;
		m_Identifiers.os = SystemInfo.operatingSystem;
	}

	public void PostGameLaunchEvent()
	{
		if (m_PlayerUID == null || m_PlayerUID.Length == 0)
		{
			Debug.Log("Missing player id for GameSight events");
			return;
		}
		Debug.Log("[GameSight] Making launch event request");
		StartCoroutine(POSTGameSightRequest(new GameLaunchBody
		{
			user_id = m_PlayerUID,
			type = "game_launch",
			identifiers = m_Identifiers
		}));
	}

	private void HashPlatformId(byte[] id)
	{
		using MD5 mD = MD5.Create();
		byte[] array = mD.ComputeHash(id);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("X2"));
		}
		m_PlayerUID = stringBuilder.ToString();
	}

	private void MakePlayerUIDFromSteam()
	{
		HashPlatformId(BitConverter.GetBytes(Singleton.Manager<ManSteamworks>.inst.AccountID.m_AccountID));
		Debug.Log("[GameSight] Using SteamId for UID");
	}

	private void SetRequestHeaders(ref StringContent content)
	{
		content.Headers.Add("X-Api-Version", "1.1.0");
		content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	}

	private IEnumerator<YieldInstruction> POSTGameSightRequest(object requestBody, string url = "https://thjw6ce6x1.execute-api.us-west-2.amazonaws.com/game-terratech/events")
	{
		JsonSerializerSettings settings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
		StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody, settings));
		SetRequestHeaders(ref content);
		Task<HttpResponseMessage> request = null;
		try
		{
			request = m_Client.PostAsync(url, content);
		}
		catch (Exception ex)
		{
			Debug.Log("GameSight request has encountered exception. Message:" + ex.Message);
		}
		while (!request.IsCompleted)
		{
			yield return new WaitForSeconds(0.5f);
		}
		try
		{
			if (request.IsFaulted)
			{
				Debug.Log("GameSight request has faulted. Exception:" + request.Exception.Message);
			}
			else if (request.IsCanceled)
			{
				Debug.Log("GameSight request was cancelled (?)");
			}
			else if (request.Result == null)
			{
				Debug.Log("GameSight request was completed, but result was NULL!");
			}
			else if (!request.Result.IsSuccessStatusCode)
			{
				Debug.Log(string.Concat("GameSight request has returned error. Code:", request.Result.StatusCode, ". Message:", request.Result.ReasonPhrase));
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("GameSight encountered an exception during logging of the result. Message:" + ex2.Message);
		}
	}

	private void Start()
	{
		if (Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			MakePlayerUIDFromSteam();
		}
		else
		{
			Singleton.Manager<ManSteamworks>.inst.SteamWorksInitialisedEvent.Subscribe(MakePlayerUIDFromSteam);
		}
		InitIdentifiers();
		m_Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("1d157ca247314276d1417ec1c3671cc6");
	}
}
