using System;

[Flags]
public enum ChunkCategory
{
	Null = 0,
	Raw = 1,
	Refined = 2,
	Component = 4,
	Fuel = 8
}
