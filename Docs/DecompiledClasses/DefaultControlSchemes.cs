using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultControlSchemes", menuName = "Asset/Table/DefaultControlSchemes")]
public class DefaultControlSchemes : ScriptableObject
{
	public List<ControlScheme> m_Schemes;
}
