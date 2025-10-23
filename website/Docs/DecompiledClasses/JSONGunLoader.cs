#define UNITY_EDITOR
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JSONGunLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "Gun";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			GetOrAddComponent<TargetAimer>(block);
			FireData orAddComponent = GetOrAddComponent<FireData>(block);
			ModuleWeapon orAddComponent2 = GetOrAddComponent<ModuleWeapon>(block);
			ModuleWeaponGun orAddComponent3 = GetOrAddComponent<ModuleWeaponGun>(block);
			ModuleWeaponGun component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.GSOMGunFixed_111).GetComponent<ModuleWeaponGun>();
			ApplyTemplate(blockID, orAddComponent3, component);
			orAddComponent2.m_AimType = TryParseEnum(obj, "AimType", ModuleWeapon.AimType.AutoAim);
			orAddComponent2.m_RotateSpeed = TryParse(obj, "RotateSpeed", 175f);
			orAddComponent2.m_ChangeTargetInteval = TryParse(obj, "ChangeTargetInterval", 0.5f);
			orAddComponent2.m_AutoFire = TryParse(obj, "AutoFire", defaultValue: false);
			orAddComponent2.m_PreventShootingTowardsFloor = TryParse(obj, "PreventShootingTowardsFloor", defaultValue: false);
			orAddComponent2.m_DeployOnHasTarget = TryParse(obj, "DeployWhenHasTarget", defaultValue: false);
			orAddComponent2.m_LimitedShootAngle = TryParse(obj, "LimitedShootAngle", 0f);
			orAddComponent2.m_DontFireIfNotAimingAtTarget = TryParse(obj, "DontFireIfNotAimingAtTarget", defaultValue: false);
			orAddComponent2.m_ShotCooldown = TryParse(obj, "ShotCooldown", 0.25f);
			orAddComponent2.m_FireSFXType = TryParseEnum(obj, "SoundEffectType", TechAudio.SFXType.LightMachineGun);
			orAddComponent3.m_ShotCooldown = TryParse(obj, "ShotCooldown", 0.25f);
			orAddComponent3.m_CooldownVariancePct = TryParse(obj, "CooldownVariance", 0.05f);
			orAddComponent3.m_FireControlMode = TryParseEnum(obj, "FireControlMode", ModuleWeaponGun.FireControlMode.Sequenced);
			orAddComponent3.m_BurstShotCount = TryParse(obj, "BurstShotCount", 0);
			orAddComponent3.m_BurstCooldown = TryParse(obj, "BurstCooldown", 0f);
			orAddComponent3.m_ResetBurstOnInterrupt = TryParse(obj, "ResetBurstOnInterrupt", defaultValue: true);
			orAddComponent3.m_SeekingRounds = TryParse(obj, "SeekingRounds", defaultValue: false);
			orAddComponent3.m_RegisterWarningAfter = TryParse(obj, "RegisterWarningAfter", 1f);
			orAddComponent3.m_ResetFiringTAfterNotFiredFor = TryParse(obj, "ResetFiringTimeAfterNotFiredFor", 1f);
			orAddComponent3.m_HasSpinUpDownAnim = TryParse(obj, "HasSpinUpDownAnim", defaultValue: false);
			orAddComponent3.m_HasCooldownAnim = TryParse(obj, "HasCooldownAnim", defaultValue: false);
			orAddComponent3.m_CanInterruptSpinUpAnim = TryParse(obj, "CanInterruptSpinUp", defaultValue: false);
			orAddComponent3.m_CanInterruptSpinDownAnim = TryParse(obj, "CanInterruptSpinDown", defaultValue: false);
			orAddComponent3.m_SpinUpAnimLayerIndex = TryParse(obj, "SpinUpAnimLayerIndex", 0);
			orAddComponent3.m_DeploySFXType = TryParseEnum(obj, "DeploySoundEffectType", TechAudio.SFXType.Default);
			orAddComponent3.m_OverheatTime = TryParse(obj, "OverheatTime", 0f);
			orAddComponent3.m_OverheatPauseWindow = TryParse(obj, "OverheatPauseWindow", 0f);
			orAddComponent3.m_DisableMainAudioLoop = TryParse(obj, "DisableMainAudioLoop", defaultValue: false);
			orAddComponent.m_BulletPrefab = Singleton.Manager<ManMods>.inst.m_ProjectileReferences.Find<WeaponRound>(TryParse(obj, "Bullet", "BulletMGun"));
			orAddComponent.m_BulletCasingPrefab = Singleton.Manager<ManMods>.inst.m_CasingReferences.Find<BulletCasing>(TryParse(obj, "Casing", "CasingMicro"));
			orAddComponent.m_MuzzleVelocity = TryParse(obj, "MuzzleVelocity", 25f);
			orAddComponent.m_CasingVelocity = TryParse(obj, "CasingVelocity", 5f);
			orAddComponent.m_BulletSprayVariance = TryParse(obj, "BulletSprayVariance", 0.1f);
			orAddComponent.m_BulletSpin = TryParse(obj, "BulletSpin", 0f);
			orAddComponent.m_CasingEjectVariance = TryParse(obj, "CasingEjectVariance", 0.3f);
			orAddComponent.m_CasingEjectSpin = TryParse(obj, "CasingEjectSpin", 50f);
			orAddComponent.m_KickbackStrength = TryParse(obj, "KickbackStrength", 5f);
			MuzzleFlash[] componentsInChildren = orAddComponent3.transform.GetComponentsInChildren<MuzzleFlash>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].m_SpeedFactor = TryParse(obj, "MuzzleFlashSpeedFactor", 1f);
			}
			CannonBarrel[] componentsInChildren2 = orAddComponent3.transform.GetComponentsInChildren<CannonBarrel>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].m_ShowParticlesOnAllQualitySettings = TryParse(obj, "ShowParticlesOnAllQualitySettings", defaultValue: false);
			}
			GimbalAimer[] componentsInChildren3 = orAddComponent3.transform.GetComponentsInChildren<GimbalAimer>();
			foreach (GimbalAimer gimbalAimer in componentsInChildren3)
			{
				switch (gimbalAimer.rotationAxis)
				{
				case GimbalAimer.AxisConstraint.Y:
					gimbalAimer.rotationLimits[0] = TryParse(obj, "GimbalBaseMinRotation", 0f);
					gimbalAimer.rotationLimits[1] = TryParse(obj, "GimbalBaseMaxRotation", 0f);
					gimbalAimer.aimClampMaxPercent = TryParse(obj, "GimbalBaseAimClampMaxPercent", 1f);
					gimbalAimer.m_XAngleAimOffset = TryParse(obj, "GimbalBaseXAngleAimOffset", 0f);
					break;
				case GimbalAimer.AxisConstraint.X:
					gimbalAimer.rotationLimits[0] = TryParse(obj, "GimbalElevMinRotation", 0f);
					gimbalAimer.rotationLimits[1] = TryParse(obj, "GimbalElevMaxRotation", 0f);
					gimbalAimer.aimClampMaxPercent = TryParse(obj, "GimbalElevAimClampMaxPercent", 1f);
					gimbalAimer.m_XAngleAimOffset = TryParse(obj, "GimbalElevXAngleAimOffset", 0f);
					break;
				}
			}
			return true;
		}
		return false;
	}

	public void ApplyTemplate(int blockID, ModuleWeaponGun target, ModuleWeaponGun template)
	{
		AnimationClip clip = JSONModuleLoader.ChildMatching(template.transform, "_muzzleFlashAnim").GetComponent<Animation>().clip;
		d.Log("Muzzle Flash Clip " + ((clip == null) ? "null" : clip.name) + " found in template " + template.name);
		AnimationClip clip2 = JSONModuleLoader.ChildMatching(template.transform, "_recoiler").GetComponent<Animation>().clip;
		d.Log("Barrel Recoil Clip " + ((clip2 == null) ? "null" : clip2.name) + " found in template " + template.name);
		foreach (Transform item in JSONModuleLoader.ChildrenMatching(target.transform, "_gimbalBase"))
		{
			GetOrAddComponent<GimbalAimer>(item).rotationAxis = GimbalAimer.AxisConstraint.Y;
		}
		foreach (Transform item2 in JSONModuleLoader.ChildrenMatching(target.transform, "_gimbalElev"))
		{
			GetOrAddComponent<GimbalAimer>(item2).rotationAxis = GimbalAimer.AxisConstraint.X;
		}
		foreach (Transform item3 in JSONModuleLoader.ChildrenMatching(target.transform, "_barrel"))
		{
			CannonBarrel orAddComponent = GetOrAddComponent<CannonBarrel>(item3);
			foreach (Transform item4 in JSONModuleLoader.ChildrenMatching(item3, "_muzzleFlash"))
			{
				orAddComponent.muzzleFlash = GetOrAddComponent<MuzzleFlash>(item4);
				foreach (Transform item5 in JSONModuleLoader.ChildrenMatching(item4, "_muzzleFlashAnim"))
				{
					GetOrAddComponent<AnimEvent>(item5);
					Animation orAddComponent2 = GetOrAddComponent<Animation>(item5);
					orAddComponent2.clip = clip;
					orAddComponent2.AddClip(clip, clip.name);
					orAddComponent2.playAutomatically = false;
					orAddComponent2.clip = orAddComponent2.GetClip(clip.name);
				}
			}
			foreach (Transform item6 in JSONModuleLoader.ChildrenMatching(item3, "_recoiler"))
			{
				GetOrAddComponent<AnimEvent>(item6);
				Animation orAddComponent3 = GetOrAddComponent<Animation>(item6);
				orAddComponent3.AddClip(clip2, clip2.name);
				orAddComponent3.playAutomatically = false;
				orAddComponent3.clip = orAddComponent3.GetClip(clip2.name);
			}
			foreach (Transform item7 in JSONModuleLoader.ChildrenMatching(item3, "_spinner"))
			{
				orAddComponent.m_FireSpinner = GetOrAddComponent<Spinner>(item7);
			}
			orAddComponent.casingEjectTransform = JSONModuleLoader.ChildMatching(item3, "_casingSpawn");
			orAddComponent.projectileSpawnPoint = JSONModuleLoader.ChildMatching(item3, "_bulletSpawn");
			orAddComponent.recoiler = JSONModuleLoader.ChildMatching(item3, "_recoiler");
		}
	}
}
