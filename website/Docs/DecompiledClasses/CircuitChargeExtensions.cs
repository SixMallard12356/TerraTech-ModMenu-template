using UnityEngine.Networking;

public static class CircuitChargeExtensions
{
	public static void Write(this NetworkWriter writer, Circuits.BlockChargeData charge)
	{
		charge.Serialize(writer);
	}

	public static void ReadCircuitCharge(this NetworkReader reader, ref Circuits.BlockChargeData charge)
	{
		charge.Deserialize(reader);
	}
}
