using System;

[NodeDescription("Convert multiple inputs into an array")]
[NodePath("TerraTech/Variables/Lists")]
[FriendlyName("TankBlock Array")]
public class uScript_TankBlockArray : uScriptLogic
{
	protected TankBlock[] unshortenedTankBlockArray = new TankBlock[8];

	protected TankBlock[] culledTankBlockArray = new TankBlock[8];

	protected uint[] blockIDArray = new uint[8];

	public bool Out => true;

	public TankBlock[] In([DefaultValue(null)] TankBlock input1, [DefaultValue(null)] TankBlock input2, [DefaultValue(null)] TankBlock input3, [DefaultValue(null)] TankBlock input4, [DefaultValue(null)] TankBlock input5, [DefaultValue(null)] TankBlock input6, [DefaultValue(null)] TankBlock input7, [DefaultValue(null)] TankBlock input8)
	{
		unshortenedTankBlockArray[0] = input1;
		unshortenedTankBlockArray[1] = input2;
		unshortenedTankBlockArray[2] = input3;
		unshortenedTankBlockArray[3] = input4;
		unshortenedTankBlockArray[4] = input5;
		unshortenedTankBlockArray[5] = input6;
		unshortenedTankBlockArray[6] = input7;
		unshortenedTankBlockArray[7] = input8;
		bool flag = false;
		int num = 0;
		for (int i = 0; i < unshortenedTankBlockArray.Length; i++)
		{
			if (unshortenedTankBlockArray[i] != null)
			{
				num++;
			}
			uint num2 = ((!(unshortenedTankBlockArray[i] == null)) ? unshortenedTankBlockArray[i].blockPoolID : 0u);
			if (blockIDArray[i] != num2)
			{
				flag = true;
			}
			blockIDArray[i] = num2;
		}
		if (flag)
		{
			Array.Resize(ref culledTankBlockArray, num);
			int num3 = 0;
			for (int j = 0; j < unshortenedTankBlockArray.Length; j++)
			{
				if (!(unshortenedTankBlockArray[j] == null))
				{
					culledTankBlockArray[num3] = unshortenedTankBlockArray[j];
					num3++;
				}
			}
		}
		return culledTankBlockArray;
	}

	public void OnDisable()
	{
		for (int i = 0; i < blockIDArray.Length; i++)
		{
			blockIDArray[i] = 0u;
		}
		Array.Resize(ref culledTankBlockArray, 0);
	}
}
