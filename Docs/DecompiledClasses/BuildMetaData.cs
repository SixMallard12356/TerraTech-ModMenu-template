using System;

[Serializable]
public class BuildMetaData
{
	public string m_Timestamp = "";

	public string m_CommitID = "?";

	public string m_CommitBranch = "?";

	public string m_BuildID = "?";

	public string m_BuildTitle = "local";
}
