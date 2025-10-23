public interface OfferedItem
{
	ItemTypeInfo GetTypeInfo();

	void SetRequested();

	bool IsSameObjectAs(OfferedItem other);
}
