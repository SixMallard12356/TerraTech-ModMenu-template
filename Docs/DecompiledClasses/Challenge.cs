using UnityEngine;

public abstract class Challenge
{
	public class ChallengeEndData
	{
		public bool completedWithSuccess;

		public bool storeSaveData;

		public ChallengeSaveData saveData;

		public ChallengeEndData(bool success)
		{
			completedWithSuccess = success;
		}

		public void SetSaveData(ChallengeSaveData saveData)
		{
			storeSaveData = true;
			this.saveData = saveData;
		}
	}

	public abstract class ChallengeSaveData
	{
	}

	public struct PlacementInfo
	{
		public WorldPosition spawnPosition;

		public Quaternion spawnRotation;

		public bool yPositionsRelativeToGround;

		public bool smoothGateUpDir;

		private bool hideHUD;

		public Matrix4x4 SpawnTransform => Matrix4x4.TRS(spawnPosition.ScenePosition, spawnRotation, Vector3.one);

		public bool ShowHUD
		{
			get
			{
				return !hideHUD;
			}
			set
			{
				hideHUD = !value;
			}
		}

		public bool ReplayMode { get; set; }

		public void SetSpawnLocation(WorldPosition position)
		{
			SetSpawnLocation(position, Quaternion.identity);
		}

		public void SetSpawnLocation(WorldPosition position, Quaternion rotation)
		{
			spawnPosition = position;
			spawnRotation = rotation;
		}
	}

	public Event<ChallengeEndData> OnChallengeEnded;

	private Tank.WeakReference m_Protagonist = new Tank.WeakReference();

	public Tank Protagonist
	{
		get
		{
			Tank tank = m_Protagonist.Get();
			if (!tank)
			{
				return Singleton.playerTank;
			}
			return tank;
		}
		set
		{
			m_Protagonist.Set(value);
		}
	}

	public abstract void Setup(object savedContextData);

	public abstract void Begin();

	public abstract void End(ManChallenge.ChallengeEndReason endReason);

	public abstract void TearDown();

	public abstract void Update();

	public virtual void OnDrawGizmos()
	{
	}

	public virtual bool IsShowingResults()
	{
		return false;
	}
}
