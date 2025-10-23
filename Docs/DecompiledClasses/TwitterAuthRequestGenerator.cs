#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

internal class TwitterAuthRequestGenerator
{
	public static UnityWebRequest GenerateRequest(string accessTokenUrl, string requestToken, string enteredPinValue, string consumerKey, string consumerSecret)
	{
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(accessTokenUrl);
		foreach (KeyValuePair<string, string> item in GenerateAccessRequestHeaders(accessTokenUrl, requestToken, enteredPinValue, consumerKey, consumerSecret))
		{
			unityWebRequest.SetRequestHeader(item.Key, item.Value);
		}
		return unityWebRequest;
	}

	private static Dictionary<string, string> GenerateAccessRequestHeaders(string accessTokenUrl, string requestToken, string pinCode, string consumerKey, string consumerSecret)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		string[] array = new string[7] { "oauth_version", "oauth_nonce", "oauth_timestamp", "oauth_signature_method", "oauth_consumer_key", "oauth_token", "oauth_verifier" };
		Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
		dictionary2.Add("oauth_version", "1.0");
		dictionary2.Add("oauth_nonce", GenerateNonce());
		dictionary2.Add("oauth_timestamp", GenerateTimeStamp());
		dictionary2.Add("oauth_signature_method", "HMAC-SHA1");
		dictionary2.Add("oauth_consumer_key", consumerKey);
		dictionary2.Add("oauth_consumer_secret", consumerSecret);
		dictionary2.Add("oauth_token", requestToken);
		dictionary2.Add("oauth_verifier", pinCode);
		string value = GenerateSignature("POST", accessTokenUrl, dictionary2);
		dictionary2.Add("oauth_signature", value);
		SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
		foreach (KeyValuePair<string, string> item in dictionary2)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].Contains(item.Key))
				{
					sortedDictionary.Add(item.Key, item.Value);
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		foreach (KeyValuePair<string, string> item2 in sortedDictionary)
		{
			if (flag)
			{
				flag = false;
				stringBuilder.AppendFormat("{0}=\"{1}\"", UrlEncode(item2.Key), UrlEncode(item2.Value));
			}
			else
			{
				stringBuilder.AppendFormat(",{0}=\"{1}\"", UrlEncode(item2.Key), UrlEncode(item2.Value));
			}
		}
		dictionary["Authorization"] = $"OAuth {stringBuilder.ToString()}";
		return dictionary;
	}

	private static string GenerateSignature(string reqType, string url, Dictionary<string, string> parameters)
	{
		string[] array = new string[3] { "oauth_consumer_secret", "oauth_token_secret", "oauth_signature" };
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (KeyValuePair<string, string> parameter in parameters)
		{
			bool flag = false;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] == parameter.Key)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				dictionary.Add(parameter.Key, parameter.Value);
			}
		}
		string s = $"{reqType}&{UrlEncode(NormalizeUrl(url))}&{MakeStringForSignature(dictionary)}";
		string text = string.Format("{0}&{1}", UrlEncode(parameters["oauth_consumer_secret"]), parameters.ContainsKey("oauth_token_secret") ? UrlEncode(parameters["oauth_token_secret"]) : string.Empty);
		d.Log("sig key : " + text);
		return Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(text)).ComputeHash(Encoding.UTF8.GetBytes(s)));
	}

	private static string GenerateNonce()
	{
		return new Random().Next(123400, int.MaxValue).ToString("X", CultureInfo.InvariantCulture);
	}

	private static string GenerateTimeStamp()
	{
		return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
	}

	private static string NormalizeUrl(string url)
	{
		Uri uri = new Uri(url);
		string text = $"{uri.Scheme}://{uri.Host}";
		if ((!(uri.Scheme == "http") || uri.Port != 80) && (!(uri.Scheme == "https") || uri.Port != 443))
		{
			text = text + ":" + uri.Port;
		}
		return text + uri.AbsolutePath;
	}

	private static string UrlEncode(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return string.Empty;
		}
		value = Uri.EscapeDataString(value);
		value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", (Match c) => c.Value.ToUpper());
		value = value.Replace("(", "%28").Replace(")", "%29").Replace("$", "%24")
			.Replace("!", "%21")
			.Replace("*", "%2A")
			.Replace("'", "%27");
		value = value.Replace("%7E", "~");
		return value;
	}

	private static string MakeStringForSignature(IEnumerable<KeyValuePair<string, string>> parameters)
	{
		StringBuilder stringBuilder = new StringBuilder();
		SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
		foreach (KeyValuePair<string, string> parameter in parameters)
		{
			sortedDictionary.Add(parameter.Key, parameter.Value);
		}
		foreach (KeyValuePair<string, string> item in sortedDictionary)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append("&");
			}
			string value = $"{UrlEncode(item.Key)}={UrlEncode(item.Value)}";
			stringBuilder.Append(value);
		}
		return UrlEncode(stringBuilder.ToString());
	}
}
