#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ManUndo : Singleton.Manager<ManUndo>
{
	public interface IUndoCommand
	{
		UndoTypes UndoType { get; }

		UIUndoButton.Context UIContext { get; }

		void ArmedStart();

		void ArmedRefresh();

		bool ArmedUpdateValid();

		void ExecuteStart();

		bool ExecuteUpdateValid();

		void Reset();

		Tank GetBufferTech();
	}

	[SerializeField]
	private UndoBlockDetach.Config m_UndoBlockDetachConfig;

	[SerializeField]
	private UndoSendTechToInventory.Config m_UndoSendTechToInventoryConfig;

	private List<IUndoCommand> m_History = new List<IUndoCommand>();

	private List<IUndoCommand> m_ExecutingCommands = new List<IUndoCommand>();

	private UndoBlockDetach m_BlockDetachCommand;

	private Queue<UndoBlockDetach> m_UndoBlockDetachPool = new Queue<UndoBlockDetach>(2);

	private bool m_Dirty;

	private TankBlock m_LastDetachedBlock;

	public bool UndoAvailable => m_History.Count > 0;

	public bool UndoInProgress => m_ExecutingCommands.Count > 0;

	public void AddCommand(IUndoCommand command)
	{
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer());
		m_History.Clear();
		m_History.Add(command);
		command.ArmedStart();
		m_Dirty = true;
	}

	private void AddOrRefreshCommand(IUndoCommand command)
	{
		if (m_History.Count == 0 || command != m_History[m_History.Count - 1])
		{
			AddCommand(command);
		}
		else
		{
			command.ArmedRefresh();
		}
	}

	public void OnBeforeDetachBlock(TankBlock block)
	{
		if (m_BlockDetachCommand == null)
		{
			if (m_History.Count != 0 && m_History[0].GetBufferTech() == block.tank)
			{
				m_History.Clear();
				m_Dirty = true;
			}
			m_BlockDetachCommand = GetUndoBlockDetachCommand();
		}
		if (block.tank == null)
		{
			d.LogError("ManUndo.OnBeforeDetachBlock - Block " + block.name + " is not attached to tech");
			return;
		}
		bool flag = true;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			flag = !Singleton.Manager<ManNetwork>.inst.NetController.AllowCollaboration;
		}
		if (flag)
		{
			d.Assert(block.tank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam);
		}
		m_LastDetachedBlock = block;
		m_BlockDetachCommand.OnBeforeDetachBlock(block);
	}

	public void BufferDetachedBlock(TankBlock block, Tank fromTech)
	{
		if (m_BlockDetachCommand != null)
		{
			m_BlockDetachCommand.OnDetachBlock(block, fromTech);
		}
	}

	public UndoBlockDetach GetUndoBlockDetachCommand()
	{
		if (m_UndoBlockDetachPool.Count > 0)
		{
			return m_UndoBlockDetachPool.Dequeue();
		}
		return new UndoBlockDetach(m_UndoBlockDetachConfig);
	}

	public UndoSendTechToInventory GetUndoSendTechToInventory()
	{
		return new UndoSendTechToInventory(m_UndoSendTechToInventoryConfig);
	}

	public void OnButtonPressed()
	{
		if (m_History.Count > 0)
		{
			IUndoCommand undoCommand = m_History[m_History.Count - 1];
			m_History.Remove(undoCommand);
			Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem();
			m_ExecutingCommands.Add(undoCommand);
			undoCommand.ExecuteStart();
			m_Dirty = true;
		}
	}

	private void OnGamepadInputMapChanged(UIInputMode enabledInputMap)
	{
		if (!InputMapSupportsUndo(enabledInputMap))
		{
			m_Dirty = true;
		}
	}

	private bool InputMapSupportsUndo(UIInputMode inputMode)
	{
		if (inputMode != UIInputMode.BlockBuilding && inputMode != UIInputMode.ItemDragging)
		{
			return inputMode == UIInputMode.Interaction;
		}
		return true;
	}

	private void Update()
	{
		if (m_BlockDetachCommand != null)
		{
			if (Singleton.Manager<ManPointer>.inst.DraggingItem == null || Singleton.Manager<ManPointer>.inst.DraggingItem.block != m_LastDetachedBlock)
			{
				if (m_BlockDetachCommand.OnAfterDetachBlock())
				{
					if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
					{
						AddOrRefreshCommand(m_BlockDetachCommand);
					}
					m_BlockDetachCommand = null;
				}
				else
				{
					m_BlockDetachCommand.Reset();
				}
				m_LastDetachedBlock = null;
			}
			else if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && m_BlockDetachCommand.OnAfterDetachBlock() && !Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				AddOrRefreshCommand(m_BlockDetachCommand);
			}
		}
		for (int num = m_History.Count - 1; num >= 0; num--)
		{
			IUndoCommand undoCommand = m_History[num];
			if (!undoCommand.ArmedUpdateValid())
			{
				m_History.Remove(undoCommand);
				undoCommand.Reset();
				m_Dirty = true;
			}
		}
		for (int num2 = m_ExecutingCommands.Count - 1; num2 >= 0; num2--)
		{
			IUndoCommand undoCommand2 = m_ExecutingCommands[num2];
			if (!undoCommand2.ExecuteUpdateValid())
			{
				m_ExecutingCommands.Remove(undoCommand2);
				undoCommand2.Reset();
				m_Dirty = true;
			}
		}
		if (!m_Dirty)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad() && !InputMapSupportsUndo(Singleton.Manager<ManInput>.inst.GetCurrentUIInputMode()))
		{
			m_History.Clear();
		}
		if (UndoAvailable)
		{
			IUndoCommand undoCommand3 = m_History[m_History.Count - 1];
			if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.UndoButton))
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.UndoButton, undoCommand3.UIContext);
				Singleton.Manager<ManInput>.inst.GamepadMapsChangedEvent.Subscribe(OnGamepadInputMapChanged);
			}
			else
			{
				(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.UndoButton) as UIUndoButton).UpdateContext(undoCommand3.UIContext);
			}
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.UndoButton);
			Singleton.Manager<ManInput>.inst.GamepadMapsChangedEvent.Unsubscribe(OnGamepadInputMapChanged);
		}
		m_Dirty = false;
	}
}
