using UnityEngine;

public class CheckpointChallengeSplinePoint : MonoBehaviour
{
	[SerializeField]
	public bool IsCheckpointGate;

	[SerializeField]
	[Tooltip("Sets the width of the track at this point")]
	public float TrackWidth;

	[SerializeField]
	[Tooltip("Modifies the illegal bounds line by given amount")]
	public float BoundsModifier;
}
