using System.Collections.Generic;

public class ScreenLog : Singleton.Manager<ScreenLog>
{
	private List<string> lines = new List<string>();

	public void Add(string line)
	{
		lines.Add(line);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
