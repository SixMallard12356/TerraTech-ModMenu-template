using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WobblyLaser : MonoBehaviour
{
	public struct Beam
	{
		public LineRenderer line;

		public Material material;
	}

	public float segmentLength = 2f;

	public int fixedSegments = 8;

	public float wobbleCycleSeconds = 1f;

	public float wobbleCycleSecondsScaler = 1f;

	public float materialCycleSeconds = 1f;

	public float materialCycleSecondsScaler = 0.5f;

	public Vector2 beamWidth = Vector2.one;

	public float wobbleExtent = 1f;

	public Vector2 wobbleExtentTaper = Vector2.one;

	public float wobbleWaveFrequency = 2f;

	public Transform debugEndPointTarget;

	public bool debugUpdateBeam;

	private List<Beam> allBeams = new List<Beam>();

	public Vector3 endPoint { get; set; }

	private void Start()
	{
		foreach (Transform item in base.transform)
		{
			LineRenderer component = item.GetComponent<LineRenderer>();
			if ((bool)component)
			{
				component.GetComponent<Renderer>().material = new Material(component.GetComponent<Renderer>().sharedMaterial);
				allBeams.Add(new Beam
				{
					line = component,
					material = component.GetComponent<Renderer>().material
				});
			}
		}
		EditorHooks.Update += DebugUpdateActiveObject;
	}

	private IEnumerable<Beam> DebugEnumerateChildBeams()
	{
		foreach (Transform item in base.transform)
		{
			LineRenderer component = item.GetComponent<LineRenderer>();
			if ((bool)component)
			{
				yield return new Beam
				{
					line = component,
					material = null
				};
			}
		}
	}

	private static void DebugUpdateActiveObject()
	{
		GameObject selectedObject = EditorHooks.SelectedObject;
		WobblyLaser wobblyLaser = (selectedObject ? selectedObject.GetComponent<WobblyLaser>() : null);
		if ((bool)wobblyLaser && wobblyLaser.debugUpdateBeam)
		{
			wobblyLaser.endPoint = wobblyLaser.transform.position + 10f * wobblyLaser.transform.forward;
			wobblyLaser.UpdateBeams(wobblyLaser.DebugEnumerateChildBeams().ToList());
			wobblyLaser.debugUpdateBeam = false;
		}
	}

	public void Update()
	{
		UpdateBeams(allBeams);
	}

	public void SetBeamColour(Color colour)
	{
		foreach (Beam allBeam in allBeams)
		{
			allBeam.line.startColor = colour;
			allBeam.line.endColor = colour;
		}
	}

	public void UpdateBeams(List<Beam> beams)
	{
		float magnitude = (endPoint - base.transform.position).magnitude;
		int num = Mathf.RoundToInt(magnitude / segmentLength + 0.5f);
		if (fixedSegments != 0)
		{
			num = fixedSegments;
			segmentLength = magnitude / (float)num;
		}
		Vector3 vector = (endPoint - base.transform.position).normalized * segmentLength;
		float num2 = 0f;
		float num3 = 1f;
		float num4 = 1f;
		foreach (Beam beam in beams)
		{
			beam.line.positionCount = num + 1;
			beam.line.startWidth = beamWidth[0];
			beam.line.endWidth = beamWidth[1];
			beam.line.useWorldSpace = true;
			float num5 = Time.time * num3 / wobbleCycleSeconds + num2;
			float x = Time.time * num4 / materialCycleSeconds + num2;
			num2 += 1f / (float)beams.Count;
			num3 *= wobbleCycleSecondsScaler;
			num4 *= materialCycleSecondsScaler;
			Vector3 position = base.transform.position;
			for (int i = 0; i < num + 1; i++)
			{
				float t = (position - base.transform.position).magnitude / magnitude;
				float num6 = wobbleExtent * Mathf.Lerp(wobbleExtentTaper[0], wobbleExtentTaper[1], t);
				float y = Mathf.Lerp(beamWidth[0], beamWidth[1], t) * num6 * Mathf.Cos((float)Math.PI * 2f * Mathf.Lerp(num5, num5 + wobbleWaveFrequency, t));
				if (i == num)
				{
					position = endPoint;
				}
				beam.line.SetPosition(i, position + new Vector3(0f, y, 0f));
				position += vector;
			}
			if ((bool)beam.material)
			{
				beam.material.mainTextureOffset = new Vector2(x, beam.material.mainTextureOffset.y);
			}
		}
	}
}
