using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public abstract class TriggerEventListener : MonoBehaviour
{
	public Event<TriggerCatcher.Interaction, Collider> Event;
}
