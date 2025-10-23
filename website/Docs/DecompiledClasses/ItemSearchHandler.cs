public interface ItemSearchHandler
{
	void HandleExpandSearch(ItemSearcher builder, ModuleItemHolder.Stack entryStack, ModuleItemHolder.Stack prevStack, out ItemSearchAvailableItems availItems);

	void HandleSearchRequest();

	bool WantsToKnowAboutSearchRequest();

	void HandleCollectItems(ItemSearchCollector collector, bool processed);
}
