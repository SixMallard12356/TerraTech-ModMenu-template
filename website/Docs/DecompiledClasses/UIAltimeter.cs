using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAltimeter : UIHUDElement
{
	[SerializeField]
	private Text m_Label;

	private Tank m_Tank;

	private List<ModuleAltimeter> m_Altimeters = new List<ModuleAltimeter>();

	private bool m_ShowingFromUI;

	public override void Show(object moduleObject)
	{
		base.Show(moduleObject);
		ModuleAltimeter moduleAltimeter = moduleObject as ModuleAltimeter;
		if ((bool)moduleAltimeter)
		{
			m_Altimeters.Add(moduleAltimeter);
			m_Tank = moduleAltimeter.block.tank;
		}
		else
		{
			m_ShowingFromUI = true;
			m_Tank = Singleton.playerTank;
		}
	}

	public override void Hide(object moduleObject)
	{
		ModuleAltimeter moduleAltimeter = moduleObject as ModuleAltimeter;
		if ((bool)moduleAltimeter)
		{
			m_Altimeters.Remove(moduleAltimeter);
		}
		if (!m_ShowingFromUI && m_Altimeters.Count == 0)
		{
			base.Hide(moduleObject);
		}
	}

	private void Update()
	{
		if (m_ShowingFromUI)
		{
			m_Tank = Singleton.playerTank;
		}
		if ((bool)m_Tank)
		{
			m_Label.text = GameUnits.GetAltitudeText(m_Tank.WorldCenterOfMass.y);
		}
	}

	private void OnRecycle()
	{
		m_Altimeters.Clear();
		m_ShowingFromUI = false;
	}
}
