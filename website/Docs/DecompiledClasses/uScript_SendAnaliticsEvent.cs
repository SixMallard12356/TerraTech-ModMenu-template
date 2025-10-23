#define UNITY_EDITOR
using System.Collections.Generic;

[FriendlyName("Send Analytics Event")]
public class uScript_SendAnaliticsEvent : uScriptLogic
{
	public bool Out => true;

	public void In(string analiticsEvent, string parameterName, object parameter)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		bool flag = parameter == null;
		bool flag2 = parameterName.NullOrEmpty();
		if (!flag && !flag2)
		{
			dictionary.Add(parameterName, parameter.ToString());
		}
		else if (flag != flag2)
		{
			d.LogWarning("Parameter or parameter name null in analytics event");
		}
	}
}
