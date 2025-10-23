using System;
using UnityEngine;

[Serializable]
public class PositionWithFacing
{
	public Vector3 position = Vector3.zero;

	public Vector3 forward = Vector3.forward;

	public Quaternion orientation
	{
		get
		{
			if (!(forward == Vector3.zero))
			{
				return Quaternion.LookRotation(forward);
			}
			return Quaternion.identity;
		}
	}

	public static PositionWithFacing identity => new PositionWithFacing(Vector3.zero, Vector3.forward);

	public Matrix4x4 Matrix => Matrix4x4.TRS(position, orientation, Vector3.one);

	public PositionWithFacing(PositionWithFacing from)
	{
		position = from.position;
		forward = from.forward;
	}

	public PositionWithFacing(Vector3 pos, Vector3 forw)
	{
		position = pos;
		forward = forw;
	}
}
