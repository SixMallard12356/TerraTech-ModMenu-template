using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TankBlockTemplate : MonoBehaviour
{
	public List<Vector3> attachPoints = new List<Vector3>();

	public List<IntVector3> filledCells = new List<IntVector3>();
}
