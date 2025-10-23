using System;
using System.Collections.Generic;
using UnityEngine;

internal class ModuleItemConsumer
{
	private enum State
	{

	}

	[Serializable]
	private struct ConsumeState
	{
		[SerializeField]
		private float duration;

		[SerializeField]
		private int moneyOutput;

		[SerializeField]
		private float energyOutputPerFrame;

		[SerializeField]
		private TechEnergy.EnergyType energyOutputType;

		[SerializeField]
		private List<ItemTypeInfo> itemOutput;
	}

	[Serializable]
	private class SerialData : Module.SerialData<SerialData>
	{
		[SerializeField]
		private State state;

		[SerializeField]
		private ConsumeState consumeState;

		[SerializeField]
		private float consumeProgress;

		[SerializeField]
		private bool[] lockedStacks;
	}
}
