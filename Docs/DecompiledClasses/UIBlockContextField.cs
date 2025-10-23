using UnityEngine;
using UnityEngine.UI;

public abstract class UIBlockContextField : MonoBehaviour
{
	public abstract void ConfigureNavigation(Selectable elementAbove, Selectable elementBelow);

	public abstract Selectable GetDefaultHighlightElement();

	public abstract Selectable GetFirstElement();

	public abstract Selectable GetLastElement();

	public abstract void Set(IHUDContextControlFieldModel targetConsumer);

	public abstract void SetSelectionAsResult();

	public abstract void ApplyResult();

	public abstract void Reset();
}
