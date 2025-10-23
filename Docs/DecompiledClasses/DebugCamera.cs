using UnityEngine;

public class DebugCamera : FirstPersonFlyCam
{
	protected override void PreApplyTransform(ref Vector3 newPos, ref Vector3 newRotation)
	{
		int num = 0;
		float axis = Singleton.Manager<ManInput>.inst.GetAxis(4);
		float axis2 = Singleton.Manager<ManInput>.inst.GetAxis(72);
		if (axis > 0f)
		{
			_ = fastFactor;
		}
		else if (axis2 > 0f)
		{
			_ = slowFactor;
		}
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
		{
			num++;
		}
		if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.Q))
		{
			num--;
		}
		Vector3 vector = new Vector3(0f, (float)num * strafeSpeed, 0f);
		newPos += vector;
	}
}
