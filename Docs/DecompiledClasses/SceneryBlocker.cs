#define UNITY_EDITOR
using System;
using UnityEngine;

public class SceneryBlocker
{
	[Serializable]
	public enum Shape
	{
		Sphere,
		RectangularPrism,
		Circle
	}

	[Serializable]
	public enum BlockMode
	{
		Spawn,
		Regrow
	}

	private WorldPosition m_Centre;

	private Tank m_SourceTank;

	[SerializeField]
	private Shape m_Shape;

	[SerializeField]
	private BlockMode m_Mode;

	private float m_Radius;

	private Vector2 m_ForwardFacing;

	private Vector2 m_RightFacing;

	private Vector2 m_Size;

	private const float k_BlockerVolumeRadiusPadding = 1f;

	public BlockMode Mode => m_Mode;

	public static SceneryBlocker CreateSphereBlocker(BlockMode mode, WorldPosition origin, float radius, Tank tank = null)
	{
		return new SceneryBlocker
		{
			m_Centre = origin,
			m_Radius = radius + 1f,
			m_Mode = mode,
			m_Shape = Shape.Sphere,
			m_SourceTank = tank
		};
	}

	public static SceneryBlocker CreateRectangularPrismBlocker(BlockMode mode, WorldPosition origin, Quaternion rot, Vector3 size)
	{
		Vector3 coord = (rot * Vector3.forward).SetY(0f);
		if (coord.sqrMagnitude > Mathf.Epsilon)
		{
			coord.Normalize();
		}
		else
		{
			coord = Vector3.forward;
		}
		Vector3 coord2 = Vector3.Cross(Vector3.up, coord);
		return new SceneryBlocker
		{
			m_Centre = origin,
			m_ForwardFacing = coord.ToVector2XZ(),
			m_RightFacing = coord2.ToVector2XZ(),
			m_Size = size.ToVector2XZ(),
			m_Mode = mode,
			m_Shape = Shape.RectangularPrism
		};
	}

	public static SceneryBlocker Create2DCircularBlocker(BlockMode mode, WorldPosition origin, float radius)
	{
		return new SceneryBlocker
		{
			m_Centre = origin,
			m_Radius = radius,
			m_Mode = mode,
			m_Shape = Shape.Circle
		};
	}

	public void UpdateBoundsSphere(float radius)
	{
		d.Assert(m_Shape == Shape.Sphere, "Trying to update sphere params on non-sphere SpawnBlocker");
		m_Radius = radius + 1f;
	}

	public bool IsBlockingPos(BlockMode mode, Vector3 scenePos, float radius)
	{
		bool result = false;
		UpdateCachedPosition();
		if (mode == m_Mode)
		{
			switch (m_Shape)
			{
			case Shape.Sphere:
				result = (scenePos - m_Centre.ScenePosition).sqrMagnitude <= (m_Radius + radius) * (m_Radius + radius);
				break;
			case Shape.RectangularPrism:
			{
				Vector2 lhs = (scenePos - m_Centre.ScenePosition).ToVector2XZ();
				float num = m_Size.x + m_Size.y + radius;
				if (num > lhs.x && num > lhs.y && num * num > lhs.sqrMagnitude)
				{
					Vector2 vector = new Vector2(Vector2.Dot(lhs, m_RightFacing), Vector2.Dot(lhs, m_ForwardFacing));
					if (radius > 0f)
					{
						vector.x = Mathf.MoveTowards(vector.x, 0f, m_Size.x / 2f);
						vector.y = Mathf.MoveTowards(vector.y, 0f, m_Size.y / 2f);
						float num2 = radius * radius;
						result = vector.sqrMagnitude <= num2;
					}
					else
					{
						result = Mathf.Abs(vector.x) < Mathf.Abs(m_Size.x / 2f) && Mathf.Abs(vector.y) < Mathf.Abs(m_Size.y / 2f);
					}
				}
				break;
			}
			case Shape.Circle:
				result = (scenePos - m_Centre.ScenePosition).SetY(0f).sqrMagnitude <= (m_Radius + radius) * (m_Radius + radius);
				break;
			}
		}
		return result;
	}

	public bool OverlapsBoundsApprox(Vector2 minWorldBounds, Vector2 maxWorldBounds)
	{
		bool result = false;
		switch (m_Shape)
		{
		case Shape.RectangularPrism:
		{
			float magnitude = m_Size.magnitude;
			Vector2 vector5 = m_Centre.GameWorldPosition.ToVector2XZ() - new Vector2(magnitude, magnitude) * 0.5f;
			Vector2 vector6 = vector5 + new Vector2(magnitude, magnitude);
			result = vector6.x > minWorldBounds.x && vector5.x < maxWorldBounds.x && vector6.y > minWorldBounds.y && vector5.y < maxWorldBounds.y;
			break;
		}
		case Shape.Sphere:
		case Shape.Circle:
		{
			Vector2 vector = m_Centre.GameWorldPosition.ToVector2XZ();
			Vector2 vector2 = new Vector2(m_Radius, m_Radius);
			Vector2 vector3 = vector - vector2;
			Vector2 vector4 = vector + vector2;
			result = vector4.x > minWorldBounds.x && vector3.x < maxWorldBounds.x && vector4.y > minWorldBounds.y && vector3.y < maxWorldBounds.y;
			break;
		}
		}
		return result;
	}

	public void DrawGizmos()
	{
		UpdateCachedPosition();
		switch (m_Shape)
		{
		case Shape.Sphere:
			Gizmos.DrawSphere(m_Centre.ScenePosition, m_Radius);
			break;
		case Shape.RectangularPrism:
		{
			Gizmos.matrix = Matrix4x4.TRS(m_Centre.ScenePosition, Quaternion.LookRotation(m_ForwardFacing.ToVector3XZ()), Vector3.one);
			Gizmos.DrawCube(Vector3.zero, new Vector3(m_Size.x, 50f, m_Size.y));
			Gizmos.matrix = Matrix4x4.identity;
			Vector3 scenePosition = m_Centre.ScenePosition;
			Vector3 vector = m_RightFacing.ToVector3XZ() * m_Size.x / 2f;
			Vector3 vector2 = m_ForwardFacing.ToVector3XZ() * m_Size.y / 2f;
			Gizmos.DrawLine(scenePosition + vector + vector2, scenePosition + vector - vector2);
			Gizmos.DrawLine(scenePosition + vector - vector2, scenePosition - vector - vector2);
			Gizmos.DrawLine(scenePosition - vector - vector2, scenePosition - vector + vector2);
			Gizmos.DrawLine(scenePosition - vector + vector2, scenePosition + vector + vector2);
			break;
		}
		case Shape.Circle:
			break;
		}
	}

	private void UpdateCachedPosition()
	{
		if (m_SourceTank != null)
		{
			m_Centre = WorldPosition.FromScenePosition(m_SourceTank.boundsCentreWorld);
		}
	}
}
