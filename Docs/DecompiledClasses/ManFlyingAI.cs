using UnityEngine;

public class ManFlyingAI : Singleton.Manager<ManFlyingAI>
{
	[SerializeField]
	public float PIDKp = 1f;

	[SerializeField]
	public float PIDKi = 1f;

	[SerializeField]
	public float PIDKd = 1f;

	[SerializeField]
	public float MaxPitch = 60f;

	[SerializeField]
	public float MaxRoll = 90f;

	[SerializeField]
	public float PitchChange = 10f;

	[SerializeField]
	public float RollChange = 10f;

	[SerializeField]
	public float MaxPitchError = 45f;

	[SerializeField]
	public float MaxRollError = 90f;

	public void Save(ManSaveGame.State saveState)
	{
	}

	private void Start()
	{
		Debug.LogError("Flying AI started");
	}

	private void Update()
	{
	}
}
