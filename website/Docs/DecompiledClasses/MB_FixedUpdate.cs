using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class MB_FixedUpdate : MonoBehaviourEvent
{
	private void FixedUpdate()
	{
		Event.Send();
	}
}
