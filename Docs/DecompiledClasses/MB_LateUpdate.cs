using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class MB_LateUpdate : MonoBehaviourEvent
{
	private void LateUpdate()
	{
		Event.Send();
	}
}
