public interface ItemSearcher
{
	void PushNode(ModuleItemHolder.Stack searchNode);

	void PushNode(ModuleItemHolder.Stack searchNode, ModuleItemHolder.Stack nextHopStack);

	void PushConverter(ItemSearchConverter conv);

	void PushFilter(ItemSearchFilter filter);
}
