using UnityEngine;

public static class MapFuncFastNoise
{
	private const int k_LowerBound = 4500000;

	private const int k_UpperBound = 28500000;

	private const float k_InvRange = 4.1666667E-08f;

	private const int k_Quantise8 = 3000000;

	private const int k_Quantise16 = 1500000;

	public static int noise(double x, double y, int nbOctave)
	{
		int num = 0;
		int num2 = 256;
		int num3 = (int)(x * (double)num2);
		int num4 = (int)(y * (double)num2);
		int num5 = nbOctave;
		while (num5 != 0)
		{
			int num6 = num3 & 0xFF;
			int num7 = num4 & 0xFF;
			int num8 = num3 >> 8;
			int num9 = (num4 >> 8) * 1376312589;
			int num10 = num9 + 1376312589;
			int num11 = num8 + num9;
			int num12 = num11 + 1;
			int num13 = num8 + num10;
			int num14 = num13 + 1;
			int num15 = (num11 << 13) ^ num11;
			int num16 = (num12 << 13) ^ num12;
			int num17 = (num13 << 13) ^ num13;
			int num18 = (num14 << 13) ^ num14;
			int num19 = num15 * (num15 * num15 * 15731 + 789221) + 1376312589;
			int num20 = num16 * (num16 * num16 * 15731 + 789221) + 1376312589;
			int num21 = num17 * (num17 * num17 * 15731 + 789221) + 1376312589;
			int num22 = num18 * (num18 * num18 * 15731 + 789221) + 1376312589;
			int num23 = (num19 & 0xFF) - 128;
			int num24 = ((num19 >> 8) & 0xFF) - 128;
			int num25 = (num20 & 0xFF) - 128;
			int num26 = ((num20 >> 8) & 0xFF) - 128;
			int num27 = (num21 & 0xFF) - 128;
			int num28 = ((num21 >> 8) & 0xFF) - 128;
			int num29 = (num22 & 0xFF) - 128;
			int num30 = ((num22 >> 8) & 0xFF) - 128;
			int num31 = num6 >> 1;
			int num32 = num7 >> 1;
			int num33 = 128 - num31;
			int num34 = num32;
			int num35 = num31;
			int num36 = 128 - num32;
			int num37 = 128 - num31;
			int num38 = 128 - num32;
			num19 = num23 * num31 + num24 * num32 + 16384 + ((num19 & 0xFF0000) >> 9);
			num20 = num25 * num33 + num26 * num34 + 16384 + ((num20 & 0xFF0000) >> 9);
			num21 = num27 * num35 + num28 * num36 + 16384 + ((num21 & 0xFF0000) >> 9);
			num22 = num29 * num37 + num30 * num38 + 16384 + ((num22 & 0xFF0000) >> 9);
			int num39 = num6 * num6 >> 8;
			int num40 = num39 * num6 >> 8;
			int num41 = 3 * num39;
			int num42 = 2 * num40;
			int num43 = num19 - ((num41 - num42) * (num19 - num20) >> 8);
			int num44 = num21 - ((num41 - num42) * (num21 - num22) >> 8);
			int num45 = num7 * num7 >> 8;
			int num46 = num45 * num7 >> 8;
			int num47 = 3 * num45;
			int num48 = 2 * num46;
			int num49 = num43 - ((num47 - num48) * (num43 - num44) >> 8);
			num49 *= 256;
			num += num49 << num5;
			num5--;
			num3 <<= 1;
			num4 <<= 1;
		}
		return num;
	}

	public static float value(float x, float y)
	{
		return (float)(noise(x, y, 1) - 4500000 << 1) * 4.1666667E-08f - 1f;
	}

	public static float valueAbs(float x, float y)
	{
		return Mathf.Abs(value(x, y));
	}

	public static float valueQuantised8(float x, float y)
	{
		return 0.25f * (float)((noise(x, y, 1) - 4500000) / 3000000) - 1f;
	}

	public static float valueQuantised8Smooth1(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 0.1875f * (float)((num - 4500000) / 3000000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f * 0.5f;
		return num2 + num3 - 1f;
	}

	public static float valueQuantised8Smooth2(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 0.125f * (float)((num - 4500000) / 3000000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f;
		return num2 + num3 - 1f;
	}

	public static float valueQuantised8Smooth3(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 0.0625f * (float)((num - 4500000) / 3000000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f * 1.5f;
		return num2 + num3 - 1f;
	}

	public static float valueQuantised16(float x, float y)
	{
		return 0.125f * (float)((noise(x, y, 1) - 4500000) / 1500000) - 1f;
	}

	public static float valueQuantised16Smooth1(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 3f / 32f * (float)((num - 4500000) / 1500000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f * 0.5f;
		return num2 + num3 - 1f;
	}

	public static float valueQuantised16Smooth2(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 0.0625f * (float)((num - 4500000) / 1500000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f;
		return num2 + num3 - 1f;
	}

	public static float valueQuantised16Smooth3(float x, float y)
	{
		int num = noise(x, y, 1);
		float num2 = 1f / 32f * (float)((num - 4500000) / 1500000);
		float num3 = (float)(num - 4500000) * 4.1666667E-08f * 1.5f;
		return num2 + num3 - 1f;
	}
}
