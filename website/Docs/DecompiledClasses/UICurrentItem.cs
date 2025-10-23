using UnityEngine;
using UnityEngine.UI;

public class UICurrentItem : MonoBehaviour
{
	public Image m_Image;

	public Sprite m_EmptySprite;

	private RecipeTable.Recipe mRecipe;

	public void Show(RecipeTable.Recipe recipe)
	{
		mRecipe = recipe;
		m_Image.sprite = Singleton.Manager<ManUI>.inst.GetSprite(mRecipe.Output_Deprecated);
	}

	public void Hide()
	{
		m_Image.sprite = m_EmptySprite;
	}
}
