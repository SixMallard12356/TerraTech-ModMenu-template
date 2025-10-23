using System.Linq;
using UnityEngine;

public class DirectionMarker : MonoBehaviour
{
	private MeshRenderer markerRenderer;

	private Vector4 currentColour;

	private void Start()
	{
		markerRenderer = GetComponentsInChildren<MeshRenderer>(includeInactive: true).FirstOrDefault();
		if (markerRenderer != null)
		{
			currentColour = markerRenderer.material.GetVector("_TintColor");
		}
	}

	public void SetColour(Color newCol)
	{
		if (markerRenderer != null)
		{
			currentColour = new Vector4(newCol.r, newCol.g, newCol.b, newCol.a);
			markerRenderer.material.SetVector("_TintColor", currentColour);
		}
	}

	public void SetAlpha(float alpha)
	{
		if (markerRenderer != null)
		{
			currentColour.w = alpha;
			markerRenderer.material.SetVector("_TintColor", currentColour);
		}
	}
}
