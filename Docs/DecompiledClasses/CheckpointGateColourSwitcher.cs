using UnityEngine;

public class CheckpointGateColourSwitcher : MonoBehaviour, ICheckpointVisualer
{
	private Material m_LocalMat;

	private int m_PrevMaterialParamValue;

	public void Initialise(Checkpoint checkpoint, int relativeCheckpointIndex, float time, int numFutureGatesToShow)
	{
		m_PrevMaterialParamValue = int.MinValue;
		UpdateColourFromRelativeIndex(relativeCheckpointIndex);
	}

	public void RelativeIndexUpdated(int relativeCheckpointIndex, int numFutureGatesToShow)
	{
		UpdateColourFromRelativeIndex(relativeCheckpointIndex);
	}

	public void StartCleanup()
	{
	}

	public bool IsReadyWithCleanup()
	{
		return true;
	}

	private int GetMaterialParamFromRelativeCheckpointIndex(int relativeCheckpointIndex)
	{
		if (relativeCheckpointIndex == 0)
		{
			return 1;
		}
		if (relativeCheckpointIndex > 0)
		{
			return 0;
		}
		return 2;
	}

	private void UpdateColourFromRelativeIndex(int relativeCheckpointIndex)
	{
		int materialParamFromRelativeCheckpointIndex = GetMaterialParamFromRelativeCheckpointIndex(relativeCheckpointIndex);
		if (materialParamFromRelativeCheckpointIndex != m_PrevMaterialParamValue)
		{
			if ((bool)m_LocalMat)
			{
				m_LocalMat.SetFloat("_NumLit", materialParamFromRelativeCheckpointIndex);
			}
			m_PrevMaterialParamValue = materialParamFromRelativeCheckpointIndex;
		}
	}

	private void OnPool()
	{
		Renderer component = GetComponent<Renderer>();
		if ((bool)component)
		{
			m_LocalMat = new Material(component.sharedMaterial);
			component.material = m_LocalMat;
		}
	}
}
