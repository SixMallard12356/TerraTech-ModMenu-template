using UnityEngine;
using UnityEngine.UI;

public class UIResourceCount : MonoBehaviour
{
	public Text m_Counter;

	public Image m_Image;

	public void SetResource(RecipeTable.Recipe.ItemSpec inputItem)
	{
		m_Counter.text = "x" + inputItem.m_Quantity;
		bool active = inputItem.m_Quantity > 1;
		m_Counter.transform.parent.gameObject.SetActive(active);
		Sprite sprite = Singleton.Manager<ManUI>.inst.GetSprite(inputItem.m_Item);
		if ((bool)sprite)
		{
			m_Image.sprite = sprite;
		}
	}
}
