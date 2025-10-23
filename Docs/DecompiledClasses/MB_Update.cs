using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class MB_Update : MonoBehaviourEvent
{
	private void Update()
	{
		Event.Send();
	}
}
