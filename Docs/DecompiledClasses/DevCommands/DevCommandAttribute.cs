using System;

namespace DevCommands;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class DevCommandAttribute : Attribute
{
	public string Name { get; set; }

	public Access Access { get; set; } = Access.DevCheat;

	public User Users { get; set; } = User.Singleplayer;

	public bool RnDOnly { get; set; }

	public bool Is(User usageFlags)
	{
		return (Users & usageFlags) == usageFlags;
	}
}
