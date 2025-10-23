#define UNITY_EDITOR
using System;
using UnityEngine;

public static class PhysicsUtils
{
	public class RaycastAllResult : IDisposable
	{
		private RaycastHit[] hits = new RaycastHit[64];

		private int index;

		private int numResults;

		private int resultsBufferSize = 64;

		private const int k_InitialResultsBufferSize = 64;

		public RaycastHit Current => hits[index];

		public bool Finished { get; private set; }

		public RaycastAllResult DoRaycast(Ray ray, float distance, int layerMask, QueryTriggerInteraction queryTriggers)
		{
			while (true)
			{
				numResults = Physics.RaycastNonAlloc(ray, hits, distance, layerMask, queryTriggers);
				if (numResults != resultsBufferSize)
				{
					break;
				}
				resultsBufferSize *= 2;
				Array.Resize(ref hits, resultsBufferSize);
			}
			index = -1;
			Finished = false;
			return this;
		}

		public bool MoveNext()
		{
			index++;
			if (index == numResults)
			{
				return false;
			}
			return true;
		}

		public RaycastAllResult GetEnumerator()
		{
			return this;
		}

		public void Dispose()
		{
			Finished = true;
		}

		public void Rewind(bool assertFinished = true)
		{
			d.Assert(!assertFinished || Finished, "rewinding iterator while in progress");
			index = -1;
			Finished = false;
		}
	}

	public class OverlapSphereAllResults : IDisposable
	{
		private const int k_InitialResultsBufferSize = 32;

		private int m_ResultsBufferSize = 32;

		private Collider[] m_Results = new Collider[32];

		private int index;

		private int numResults;

		public Collider Current => m_Results[index];

		public bool Finished { get; private set; }

		public OverlapSphereAllResults DoOverlapSphere(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggers = QueryTriggerInteraction.UseGlobal)
		{
			while (true)
			{
				numResults = Physics.OverlapSphereNonAlloc(position, radius, m_Results, layerMask, queryTriggers);
				if (numResults != m_ResultsBufferSize)
				{
					break;
				}
				m_ResultsBufferSize *= 2;
				Array.Resize(ref m_Results, m_ResultsBufferSize);
			}
			index = -1;
			Finished = false;
			return this;
		}

		public bool MoveNext()
		{
			index++;
			if (index == numResults)
			{
				return false;
			}
			return true;
		}

		public OverlapSphereAllResults GetEnumerator()
		{
			return this;
		}

		public void Dispose()
		{
			Finished = true;
		}
	}

	private static int[] s_LayerCollisionMatrixMasks = null;

	private static RaycastAllResult s_raycastAllResult = new RaycastAllResult();

	private static OverlapSphereAllResults s_OverlapSphereAllResults = new OverlapSphereAllResults();

	public static RaycastAllResult RaycastAllNonAlloc(Ray ray, float distance, int layerMask, QueryTriggerInteraction queryTriggers)
	{
		s_raycastAllResult.DoRaycast(ray, distance, layerMask, queryTriggers);
		return s_raycastAllResult;
	}

	public static OverlapSphereAllResults OverlapSphereAllNonAlloc(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggers = QueryTriggerInteraction.UseGlobal)
	{
		s_OverlapSphereAllResults.DoOverlapSphere(position, radius, layerMask, queryTriggers);
		return s_OverlapSphereAllResults;
	}

	public static int GetLayerCollisionMask(int layerIndex)
	{
		if (s_LayerCollisionMatrixMasks == null)
		{
			BuildLayerCollisionMask();
		}
		return s_LayerCollisionMatrixMasks[layerIndex];
	}

	private static void BuildLayerCollisionMask()
	{
		s_LayerCollisionMatrixMasks = new int[32];
		for (int i = 0; i < s_LayerCollisionMatrixMasks.Length; i++)
		{
			int num = 0;
			for (int j = 0; j < s_LayerCollisionMatrixMasks.Length; j++)
			{
				if (!Physics.GetIgnoreLayerCollision(i, j))
				{
					num |= 1 << j;
				}
			}
			s_LayerCollisionMatrixMasks[i] = num;
		}
	}

	public static float GetSpeedInDirection(this Rigidbody rb, Vector3 direction)
	{
		return (Quaternion.FromToRotation(direction, Vector3.forward) * rb.velocity).z;
	}

	public static Vector3 GetDampingForce(this Rigidbody rb, Vector3 directionToDampAround, Vector3 velocity = default(Vector3), float dampingT = 1f, float deltaTime = -1f)
	{
		if (velocity == default(Vector3))
		{
			velocity = rb.velocity;
		}
		directionToDampAround = directionToDampAround.normalized;
		Vector3 vector = velocity - Vector3.Dot(velocity, directionToDampAround) * directionToDampAround;
		if (deltaTime == -1f)
		{
			deltaTime = Time.fixedDeltaTime;
		}
		return dampingT * (rb.mass * ((vector - velocity) / deltaTime));
	}

	public static Vector3 GetAntiForce(this Rigidbody rb, float t)
	{
		return t * (rb.mass * (-rb.velocity / Time.fixedDeltaTime));
	}
}
