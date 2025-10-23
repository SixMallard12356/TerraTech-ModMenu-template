using UnityEngine;
using UnityEngine.UI;

public class UITechDisplay : MonoBehaviour
{
	[SerializeField]
	private RawImage m_Image;

	[SerializeField]
	private IntVector2 m_TechImageSize = new IntVector2(54, 54);

	[SerializeField]
	private TooltipComponent m_TooltipComponent;

	public void Setup(Tank liveTech)
	{
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage(liveTech, m_TechImageSize, encodeTechData: false, SetupWithRenderedTech);
	}

	public void Setup(TechData techData)
	{
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage(techData, m_TechImageSize, encodeTechData: false, SetupWithRenderedTech);
	}

	public void Clear()
	{
		m_Image.texture = null;
	}

	private void SetupWithRenderedTech(TechData techData, Texture2D techImage)
	{
		Setup(techImage, techData.Name);
	}

	private void Setup(Texture2D techImage, string techName)
	{
		m_Image.texture = techImage;
		if (techImage != null)
		{
			Vector2 sizeDelta = m_Image.rectTransform.sizeDelta;
			float num = (float)techImage.width / (float)techImage.height;
			sizeDelta.x = sizeDelta.y * num;
			m_Image.rectTransform.sizeDelta = sizeDelta;
		}
		m_TooltipComponent.SetText(techName);
	}

	private void OnRecycle()
	{
		Clear();
	}
}
