using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class SmokeTrail : MonoBehaviour, IWorldTreadmill
{
	public int numberOfPoints = 10;

	public float updateSpeed = 0.25f;

	public float riseSpeed = 0.25f;

	public float spread = 0.2f;

	private LineRenderer line;

	private Transform trans;

	private Vector3[] positions;

	private Vector3[] directions;

	private float[] alphaCache;

	private float timeSinceUpdate;

	private Material lineMaterial;

	private float lineSegment;

	private int currentNumberOfPoints = 2;

	private bool allPointsAdded;

	private Color baseColour;

	private bool lineHidden;

	public Func<float, float> UpdateAlphaFn { get; set; }

	private void UpdateAlpha()
	{
		if (UpdateAlphaFn == null)
		{
			return;
		}
		float num = UpdateAlphaFn(alphaCache[0]);
		if (num < 0.001f)
		{
			if (!lineHidden)
			{
				lineHidden = true;
				allPointsAdded = false;
				currentNumberOfPoints = 0;
				line.positionCount = 0;
				alphaCache[0] = 0f;
			}
		}
		else
		{
			alphaCache[0] = num;
			Color color = baseColour;
			Color endColor = color;
			color.a = num;
			endColor.a = alphaCache[alphaCache.Length - 1];
			lineHidden = false;
			line.startColor = color;
			line.endColor = endColor;
		}
	}

	private Vector3 getSmokeVec()
	{
		Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
		if (onUnitSphere.y < 0f)
		{
			onUnitSphere.y = 0f - onUnitSphere.y;
		}
		onUnitSphere *= spread;
		onUnitSphere.y += riseSpeed;
		return onUnitSphere;
	}

	public void Reset()
	{
		for (int i = 0; i < numberOfPoints; i++)
		{
			alphaCache[i] = 0f;
		}
		UpdateAlpha();
		allPointsAdded = false;
		currentNumberOfPoints = 0;
		line.positionCount = currentNumberOfPoints;
		lineHidden = false;
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		if (!lineHidden)
		{
			for (int i = 0; i < numberOfPoints; i++)
			{
				positions[i] += amountToMove;
			}
			line.SetPositions(positions);
		}
	}

	private void OnPool()
	{
		trans = base.transform;
		line = GetComponent<LineRenderer>();
		lineMaterial = line.material;
		baseColour = lineMaterial.GetColor("_TintColor");
		lineSegment = 1f / (float)numberOfPoints;
		alphaCache = new float[numberOfPoints];
		for (int i = 0; i < numberOfPoints; i++)
		{
			alphaCache[i] = 0f;
		}
		positions = new Vector3[numberOfPoints];
		directions = new Vector3[numberOfPoints];
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
		Reset();
		line.enabled = true;
	}

	private void OnRecycle()
	{
		line.enabled = false;
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	private void Update()
	{
		Vector3 position = trans.position;
		bool flag = lineHidden;
		int num = currentNumberOfPoints;
		UpdateAlpha();
		if (lineHidden)
		{
			return;
		}
		if (currentNumberOfPoints < 2)
		{
			currentNumberOfPoints = 2;
			for (int i = num; i < currentNumberOfPoints; i++)
			{
				Vector3 smokeVec = getSmokeVec();
				directions[i] = smokeVec;
				positions[i] = position;
			}
		}
		float deltaTime = Time.deltaTime;
		timeSinceUpdate += deltaTime;
		if (timeSinceUpdate > updateSpeed)
		{
			timeSinceUpdate -= updateSpeed;
			Vector3 smokeVec;
			if (!allPointsAdded)
			{
				currentNumberOfPoints++;
				smokeVec = getSmokeVec();
				directions[0] = smokeVec;
				positions[0] = position;
				if (currentNumberOfPoints == numberOfPoints)
				{
					allPointsAdded = true;
				}
			}
			for (int num2 = currentNumberOfPoints - 1; num2 > 0; num2--)
			{
				float num3 = alphaCache[num2 - 1];
				alphaCache[num2] = num3;
				smokeVec = positions[num2 - 1];
				positions[num2] = smokeVec;
				smokeVec = directions[num2 - 1];
				directions[num2] = smokeVec;
			}
			smokeVec = getSmokeVec();
			directions[0] = smokeVec;
		}
		for (int j = 1; j < currentNumberOfPoints; j++)
		{
			Vector3 smokeVec = positions[j];
			smokeVec += directions[j] * deltaTime;
			positions[j] = smokeVec;
		}
		positions[0] = position;
		if (num != currentNumberOfPoints || flag)
		{
			line.positionCount = currentNumberOfPoints;
		}
		line.SetPositions(positions);
		if (allPointsAdded)
		{
			lineMaterial.mainTextureOffset = new Vector2(lineSegment * (timeSinceUpdate / updateSpeed), lineMaterial.mainTextureOffset.y);
		}
	}
}
