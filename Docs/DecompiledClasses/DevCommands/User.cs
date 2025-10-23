using System;

namespace DevCommands;

[Flags]
public enum User
{
	Singleplayer = 2,
	Multiplayer = 0xC,
	Host = 6,
	Server = 4,
	Client = 8,
	Default = 2,
	Any = 0xE
}
