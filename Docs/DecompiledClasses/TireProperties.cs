using UnityEngine;

public class TireProperties : ScriptableObject
{
	public ManWheels.TireProperties props;

	public static TireProperties CreateInstance()
	{
		TireProperties tireProperties = ScriptableObject.CreateInstance<TireProperties>();
		tireProperties.props = new ManWheels.TireProperties();
		return tireProperties;
	}
}
