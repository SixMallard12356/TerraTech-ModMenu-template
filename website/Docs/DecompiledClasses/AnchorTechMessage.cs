#define UNITY_EDITOR
using UnityEngine.Networking;

public class AnchorTechMessage : MessageBase
{
	private TechAnchors techAnchors;

	public AnchorTechMessage(TechAnchors techAnchors)
	{
		this.techAnchors = techAnchors;
	}

	public override void Deserialize(NetworkReader reader)
	{
		d.LogError("AnchorTechMessage.Deserialise isn't used. Use TechAnchors.OnDeserialise directly.");
	}

	public override void Serialize(NetworkWriter writer)
	{
		techAnchors.OnSerialize(writer);
	}
}
