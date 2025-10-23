using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using UnityEngine;

public class uScript_SaveVariable : uScriptLogic
{
	public bool Out => true;

	public void In(object variable, GameObject owner, string uniqueId)
	{
		if (!owner)
		{
			return;
		}
		Encounter component = owner.GetComponent<Encounter>();
		if ((bool)component)
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto,
				TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
				Formatting = Formatting.None
			};
			object obj = variable;
			if (variable.GetType() == typeof(Vector3))
			{
				obj = new V3Serial((Vector3)variable);
			}
			string data = JsonConvert.SerializeObject(obj, obj.GetType(), settings);
			component.AddEncounterData(uniqueId, data);
		}
	}
}
