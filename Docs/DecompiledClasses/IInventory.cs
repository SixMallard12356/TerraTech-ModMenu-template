using System;
using System.Collections.Generic;

public interface IInventory<T>
{
	IEnumerator<KeyValuePair<T, int>> GetEnumerator();

	int GetQuantity(T item);

	int GetUnreservedQuantity(T item);

	bool GetIsDeathStreakReward(T item);

	void SubscribeToInventoryChanged(Action<T, int> _delegate);

	void UnsubscribeToInventoryChanged(Action<T, int> _delegate);

	bool IsAvailableToLocalPlayer(T item);

	int GetNumReserved(T item);

	bool CanReserveItem(int netPlayerID, T item);

	bool HostReserveItem(int netPlayerID, T item);

	bool CancelReserveItem(int netPlayerID, T item);

	bool HasReservedItem(int netPlayerID, T item);

	bool CanConsumeItem(int netPlayerID, T item);

	int HostConsumeItem(int netPlayerID, T item, int quantity = 1);

	void HostAddItem(T item, int quantity = 1);

	void HostStoreTech(TechData techData);

	bool HasItemsToSpawnTech(TechData techData);

	bool HasItemsToSpawnTech(BlockCountList counts);

	void SetBlockCount(T item, int count);

	void Clear();

	void FillTo(InventoryBlockList list);
}
