using UnityEngine.Networking;

public interface INetworkedTechComponent
{
	NetworkedTechComponentID GetTechComponentID();

	void OnSerialize(NetworkWriter writer);

	void OnDeserialize(NetworkReader reader);
}
