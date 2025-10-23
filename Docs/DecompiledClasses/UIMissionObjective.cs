using UnityEngine;
using UnityEngine.UI;

public class UIMissionObjective : MonoBehaviour
{
	[SerializeField]
	private Text m_ObjectiveLabel;

	[SerializeField]
	private Text m_CountLabel;

	[SerializeField]
	private RectTransform m_CompletedObject;

	[SerializeField]
	private Color m_CompletedTextColour;

	private QuestLogData.EncounterObjective m_EncounterObjective;

	private Color m_DefaultTextColour;

	private const string kCountString = " ({0}/{1})";

	public void Init(QuestLogData.EncounterObjective objective)
	{
		m_EncounterObjective = objective;
		UpdateObjective();
	}

	private void UpdateObjective()
	{
		string text = m_EncounterObjective.Description;
		bool isCompleted = m_EncounterObjective.IsCompleted;
		bool flag = m_EncounterObjective.ShowCount && !isCompleted;
		int currentCount = m_EncounterObjective.CurrentCount;
		int targetCount = m_EncounterObjective.TargetCount;
		string text2 = (flag ? $" ({currentCount.ToString()}/{targetCount.ToString()})" : string.Empty);
		if (m_CountLabel != null)
		{
			m_CountLabel.text = text2;
			m_CountLabel.gameObject.SetActive(flag);
		}
		else if (flag)
		{
			text += text2;
		}
		m_ObjectiveLabel.text = text;
		m_ObjectiveLabel.color = (isCompleted ? m_CompletedTextColour : m_DefaultTextColour);
		if (m_CompletedObject != null)
		{
			m_CompletedObject.gameObject.SetActive(isCompleted);
		}
	}

	private void OnPool()
	{
		m_DefaultTextColour = m_ObjectiveLabel.color;
	}
}
