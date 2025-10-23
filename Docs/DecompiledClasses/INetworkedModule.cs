using UnityEngine.Networking;

public interface INetworkedModule
{
	TankBlock GetBlock();

	NetworkedModuleID GetModuleID();

	void OnSerialize(NetworkWriter writer);

	void OnDeserialize(NetworkReader reader);
}
