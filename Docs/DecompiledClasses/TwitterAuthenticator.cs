#define UNITY_EDITOR
using System;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using Spring.Social.OAuth1;
using Spring.Social.Twitter.Connect;
using UnityEngine;
using UnityEngine.Networking;

public class TwitterAuthenticator : MonoBehaviour
{
	public interface PinEntryForm
	{
		void BeginPinEntry(string authUrl, Action<string> onPinEntered, Action onPinEntryFailed);
	}

	[SerializeField]
	private string m_ConsumerKey;

	[SerializeField]
	private string m_ConsumerSecret;

	private const string kOAuthUrl = "https://api.twitter.com/oauth/authorize?oauth_token={0}";

	private const string kAccessTokenUrl = "https://api.twitter.com/oauth/access_token";

	private Action<string, string> m_SuccessAction;

	private Action m_FailAction;

	private PinEntryForm m_PinEntryForm;

	private IEnumerator m_AuthenticationExecutor;

	private TwitterServiceProvider m_ServiceProvider;

	private bool m_HasEnteredPin;

	private string m_EnteredPinValue;

	public TwitterServiceProvider ServiceProvider
	{
		get
		{
			if (m_ServiceProvider == null)
			{
				if (!string.IsNullOrEmpty(m_ConsumerKey) && !string.IsNullOrEmpty(m_ConsumerSecret))
				{
					m_ServiceProvider = new TwitterServiceProvider(m_ConsumerKey, m_ConsumerSecret);
				}
				else
				{
					d.LogError("please input your consumer key and consumer secret.");
				}
			}
			return m_ServiceProvider;
		}
	}

	public void Awake()
	{
	}

	public void Update()
	{
		if (m_AuthenticationExecutor != null && !m_AuthenticationExecutor.MoveNext())
		{
			m_AuthenticationExecutor = null;
		}
	}

	public void BeginAuthentication(Action<string, string> successAction, Action failAction, PinEntryForm entryForm)
	{
		Log("BeginAuthentication");
		Cancel();
		m_SuccessAction = successAction;
		m_FailAction = failAction;
		m_PinEntryForm = entryForm;
		m_HasEnteredPin = false;
		m_EnteredPinValue = "";
		if (!string.IsNullOrEmpty(m_ConsumerKey) && !string.IsNullOrEmpty(m_ConsumerSecret))
		{
			m_AuthenticationExecutor = AuthenticationProcedure();
			return;
		}
		d.LogError("please input your consumer key and consumer secret.");
		failAction?.Invoke();
	}

	public void Cancel()
	{
		if (m_AuthenticationExecutor != null)
		{
			Log("Authentication Cancelled");
			m_AuthenticationExecutor = null;
		}
	}

	private IEnumerator AuthenticationProcedure()
	{
		Log("Begin AuthenticationProcedure");
		string requestToken;
		bool success = GenerateRequestToken(out requestToken) && LaunchAuthorisationPage(requestToken);
		if (success)
		{
			Log("AuthenticationProcedure waiting on pin entry");
			while (!m_HasEnteredPin)
			{
				yield return null;
			}
			Log("AuthenticationProcedure pin entry complete");
		}
		if (success)
		{
			if (m_HasEnteredPin && !string.IsNullOrEmpty(m_EnteredPinValue))
			{
				UnityWebRequest accessKeyPageReq = TwitterAuthRequestGenerator.GenerateRequest("https://api.twitter.com/oauth/access_token", requestToken, m_EnteredPinValue, m_ConsumerKey, m_ConsumerSecret);
				Log("AuthenticationProcedure begin waiting on accessKey request");
				UnityWebRequestAsyncOperation operation = accessKeyPageReq.SendWebRequest();
				while (!operation.isDone)
				{
					yield return null;
				}
				Log("AuthenticationProcedure end waiting on accessKey request");
				if (!accessKeyPageReq.isNetworkError && !accessKeyPageReq.isHttpError && string.IsNullOrEmpty(accessKeyPageReq.error))
				{
					string value = Regex.Match(accessKeyPageReq.downloadHandler.text, "oauth_token=([^&]+)").Groups[1].Value;
					string value2 = Regex.Match(accessKeyPageReq.downloadHandler.text, "oauth_token_secret=([^&]+)").Groups[1].Value;
					if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
					{
						Log("accessKeyPageReq - succeded");
						if (m_SuccessAction != null)
						{
							m_SuccessAction(value, value2);
						}
						success = true;
					}
					else
					{
						Log("accessKeyPageReq - no token or secret. error : token or secret is empty");
						success = false;
					}
				}
				else
				{
					Log($"accessKeyPageReq - failed on error. error : {accessKeyPageReq.error}");
					success = false;
				}
			}
			else
			{
				Log("AuthenticationProcedure: No PIN given or entered PIN is empty");
				success = false;
			}
		}
		if (!success && m_FailAction != null)
		{
			m_FailAction();
		}
		Log("End AuthenticationProcedure, success=" + success);
	}

	private bool GenerateRequestToken(out string resultToken)
	{
		bool result = false;
		resultToken = "";
		Log("Begin GenerateRequestToken");
		try
		{
			OAuthToken oAuthToken = ServiceProvider.OAuthOperations.FetchRequestToken("oob", null);
			if (oAuthToken != null)
			{
				if (!string.IsNullOrEmpty(oAuthToken.Value) && !string.IsNullOrEmpty(oAuthToken.Secret))
				{
					string text = "GenerateRequestToken - succeeded";
					text = text + "\n    Token : " + oAuthToken.Value;
					text = text + "\n    TokenSecret : " + oAuthToken.Secret;
					Log(text);
					resultToken = oAuthToken.Value;
					result = true;
				}
				else
				{
					Log($"GenerateRequestToken - empty result. response : {oAuthToken}");
				}
			}
			else
			{
				Log(string.Format("GenerateRequestToken - failed. error : {0}", "something went wrong"));
			}
		}
		catch (Exception ex)
		{
			Log("GenerateRequestToken threw an exception: " + ex.ToString());
		}
		Log("End GenerateRequestToken, success=" + result);
		return result;
	}

	private bool LaunchAuthorisationPage(string reqToken)
	{
		bool flag = false;
		Log("Begin LaunchAuthorisationPage");
		string text = $"https://api.twitter.com/oauth/authorize?oauth_token={reqToken}";
		try
		{
			Application.OpenURL(text);
			if (m_PinEntryForm != null)
			{
				m_PinEntryForm.BeginPinEntry(text, OnPinEntrySucceeded, OnPinEntryFailed);
				flag = true;
			}
			else
			{
				Log("LaunchAuthorisationPage has no pin entry form");
				flag = false;
			}
		}
		catch (WebException ex)
		{
			Log("LaunchAuthorisationPage threw and exception: " + ex.ToString());
			flag = false;
		}
		Log("End LaunchAuthorisationPage, success=" + flag);
		return flag;
	}

	private void OnPinEntrySucceeded(string pinCode)
	{
		d.Assert(m_AuthenticationExecutor != null, "Got a PIN entry callback when we are not authenticating");
		m_HasEnteredPin = true;
		m_EnteredPinValue = pinCode;
	}

	private void OnPinEntryFailed()
	{
		d.Assert(m_AuthenticationExecutor != null, "Got a PIN entry callback when we are not authenticating");
		m_HasEnteredPin = true;
		m_EnteredPinValue = "";
	}

	private void Log(string message)
	{
		d.Log(Time.realtimeSinceStartup + " TwitterAuthenticator: " + message);
	}
}
