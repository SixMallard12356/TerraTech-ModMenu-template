#define UNITY_EDITOR
using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WibbleBeam : MonoBehaviour
{
	[Serializable]
	public class Collection
	{
		public class Iterator
		{
			private Collection collection;

			private int nextIndex;

			public Iterator(Collection c)
			{
				collection = c;
				nextIndex = -1;
			}

			public void UpdateNext(Vector3 basePos, Vector3 idealPos, Vector3 endPos)
			{
				nextIndex++;
				collection.GetOrCreate(nextIndex).UpdatePath(basePos, idealPos, endPos);
			}

			public void Finalise()
			{
				collection.DisableFromIndex(nextIndex + 1);
			}
		}

		[SerializeField]
		private WibbleBeam[] beams;

		[SerializeField]
		private Transform parent;

		public Collection(WibbleBeam basePrefab, int size, Transform parentObject)
		{
			d.Assert(size > 0);
			parent = parentObject;
			beams = new WibbleBeam[size];
			for (int i = 0; i < size; i++)
			{
				beams[i] = InitBeam(basePrefab);
			}
		}

		private WibbleBeam InitBeam(WibbleBeam prefab)
		{
			WibbleBeam wibbleBeam = UnityEngine.Object.Instantiate(prefab);
			wibbleBeam.transform.parent = parent;
			wibbleBeam.lineRenderer = wibbleBeam.GetComponent<LineRenderer>();
			wibbleBeam.lineRenderer.positionCount = wibbleBeam.numVertices;
			wibbleBeam.lineRenderer.enabled = false;
			return wibbleBeam;
		}

		public WibbleBeam GetOrCreate(int index)
		{
			while (index >= beams.Length)
			{
				int num = beams.Length;
				Array.Resize(ref beams, beams.Length * 2);
				for (int i = num; i < beams.Length; i++)
				{
					beams[i] = InitBeam(beams[0]);
				}
			}
			if (!beams[index].lineRenderer.enabled)
			{
				beams[index].lineRenderer.enabled = true;
			}
			return beams[index];
		}

		public void DisableFromIndex(int index)
		{
			while (index < beams.Length)
			{
				if (beams[index].lineRenderer.enabled)
				{
					beams[index].lineRenderer.enabled = false;
				}
				beams[index].drawnOnce = false;
				index++;
			}
		}

		public Iterator GetIterator()
		{
			return new Iterator(this);
		}
	}

	public int numVertices = 10;

	public float width = 0.15f;

	public float wibble1Speed = 40f;

	public float wibble1Frequency = 10f;

	public float wibble1Height = 0.05f;

	public float wibble2Speed = 40f;

	public float wibble2Frequency = 10f;

	public float wibble2Height = 0.05f;

	public float wibbleFallOff = 1.5f;

	public float scaleWithLength = 1f;

	public float randomTimeScalePercent = 0.3f;

	public float lowDetailCameraThreshold = 40f;

	[SerializeField]
	private LineRenderer lineRenderer;

	private float timeOffset;

	private float randomTimeScale = 1f;

	private bool drawnOnce;

	private static Vector3 s_CamPosCache;

	private static int s_CamPosCacheFrame;

	public void UpdatePath(Vector3 basePos, Vector3 idealPos, Vector3 endPos)
	{
		if (drawnOnce && !lineRenderer.isVisible)
		{
			return;
		}
		float num = 1f / (float)(numVertices - 1);
		float num2 = 0f;
		drawnOnce = true;
		if ((basePos - s_CamPosCache).sqrMagnitude < lowDetailCameraThreshold * lowDetailCameraThreshold)
		{
			float magnitude = (endPos - basePos).magnitude;
			float num3 = (Time.time - timeOffset) * randomTimeScale;
			float num4 = num3 * wibble1Speed;
			float num5 = num3 * wibble2Speed;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = scaleWithLength * magnitude * 0.5f;
			Vector3 forward = Singleton.cameraTrans.forward;
			Vector3 normalized = Vector3.Cross(idealPos - basePos, forward).normalized;
			Vector3 normalized2 = Vector3.Cross(endPos - idealPos, forward).normalized;
			for (int i = 0; i < numVertices; i++)
			{
				float t = num2 * num2;
				Vector3 b = Vector3.Lerp(idealPos, endPos, t);
				Vector3 vector = Vector3.Lerp(basePos, b, num2);
				Vector3 vector2 = Vector3.Lerp(normalized, normalized2, t);
				float num9 = Mathf.Abs(num2 + num2 - 1f);
				num9 = Mathf.Pow(1f - num9, wibbleFallOff);
				float num10 = wibble1Height * Mathf.Sin(num4 + num6);
				float num11 = wibble2Height * Mathf.Sin(num5 + num7);
				float num12 = (num10 + num11) * num9 * num8;
				lineRenderer.SetPosition(i, vector + vector2 * num12);
				num2 += num;
				num6 += wibble1Frequency;
				num7 += wibble2Frequency;
			}
		}
		else
		{
			for (int j = 0; j < numVertices; j++)
			{
				Vector3 b2 = Vector3.Lerp(idealPos, endPos, num2 * num2);
				Vector3 position = Vector3.Lerp(basePos, b2, num2);
				lineRenderer.SetPosition(j, position);
				num2 += num;
			}
		}
	}

	public Collection InitCollection(int initSize, Transform parent)
	{
		return new Collection(this, Mathf.Max(initSize, 1), parent);
	}

	private void OnSpawn()
	{
		timeOffset = UnityEngine.Random.value * 100f;
		randomTimeScale = 1f + (UnityEngine.Random.value * 2f - 1f) * randomTimeScalePercent;
		drawnOnce = false;
	}

	private void Update()
	{
		if (s_CamPosCacheFrame != Time.frameCount)
		{
			s_CamPosCacheFrame = Time.frameCount;
			s_CamPosCache = Singleton.cameraTrans.position;
		}
	}
}
