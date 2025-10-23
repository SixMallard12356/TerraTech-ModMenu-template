using System.Collections.Generic;

public class ModuleControlCategoryComparer : IEqualityComparer<ModuleControlCategory>
{
	public bool Equals(ModuleControlCategory x, ModuleControlCategory y)
	{
		return x == y;
	}

	public int GetHashCode(ModuleControlCategory obj)
	{
		return (int)obj;
	}
}
