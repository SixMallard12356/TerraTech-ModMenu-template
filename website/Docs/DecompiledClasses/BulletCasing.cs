#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public sealed class BulletCasing : MonoBehaviour
{
	[SerializeField]
	private float m_LifeTime = 1f;

	private Transform trans;

	private Vector3 m_Velocity;

	private Vector3 m_AngularVelocity;

	private Vector3 m_Acceleration;

	private float m_LifetimeRemaining;

	private static bool s_SubscribedToUpdate;

	private static List<BulletCasing> s_BulletCasings;

	public void Eject(Vector3 ejectDirection, FireData fireData, Tank shooter)
	{
		m_Velocity = ejectDirection.normalized * fireData.m_CasingVelocity;
		m_Velocity = m_Velocity.RandomVariancePerAxis(fireData.m_CasingEjectVariance);
		if (shooter != null)
		{
			m_Velocity += shooter.rbody.velocity;
		}
		m_AngularVelocity = (Vector3.one * fireData.m_CasingEjectSpin).RandomVariancePerAxis(1f) * (float)Math.PI;
		m_Acceleration = Physics.gravity;
	}

	private static void StaticUpdateCasings()
	{
		float deltaTime = Time.deltaTime;
		for (int num = s_BulletCasings.Count - 1; num >= 0; num--)
		{
			if (!s_BulletCasings[num].UpdateCasing(deltaTime))
			{
				s_BulletCasings.RemoveAt(num);
			}
		}
	}

	private void PrePool()
	{
		d.Assert(m_LifeTime > 0f, "BulletCasing, please ensure lifetime is greater than zero, otherwise this object will never clean up after itself");
		base.gameObject.AddComponent<WorldSpaceObject>();
	}

	private void OnPool()
	{
		trans = GetComponent<Transform>();
	}

	private void OnSpawn()
	{
		m_Velocity = Vector3.zero;
		m_AngularVelocity = Vector3.zero;
		m_LifetimeRemaining = m_LifeTime;
		if (!s_SubscribedToUpdate)
		{
			s_SubscribedToUpdate = true;
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.Last, StaticUpdateCasings);
			s_BulletCasings = new List<BulletCasing>(64);
		}
		s_BulletCasings.Add(this);
	}

	private bool UpdateCasing(float deltaTime)
	{
		if (m_LifeTime > 0f)
		{
			m_LifetimeRemaining -= deltaTime;
			if (m_LifetimeRemaining < 0f)
			{
				this.Recycle();
				return false;
			}
		}
		m_Velocity += m_Acceleration * deltaTime;
		trans.Translate(m_Velocity * deltaTime, Space.World);
		trans.Rotate(m_AngularVelocity * deltaTime, Space.World);
		return true;
	}
}
