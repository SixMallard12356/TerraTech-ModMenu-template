#define UNITY_EDITOR
using UnityEngine;

public class ChangelistProxy
{
	private static BuildMetaData m_BuildMetaData;

	public static string TIMESTAMP => "T1739217970";

	public static string COMMIT_ID => "1120346eeee960bf2562f5855ea5672258468bdf";

	public static string COMMIT_BRANCH => "";

	public static string BUILD_ID => "709";

	public static string BUILD_TITLE => "TT_Configurable_Desktop_JF";

	private static void LoadBuildMetaData()
	{
		string text = "Tables/BuildMetaData";
		BuildMetaDataObject buildMetaDataObject = Resources.Load<BuildMetaDataObject>(text);
		if (buildMetaDataObject != null)
		{
			if (buildMetaDataObject.m_BuildMetaData != null)
			{
				m_BuildMetaData = buildMetaDataObject.m_BuildMetaData;
				return;
			}
			d.LogErrorFormat("BuildMetaDataObject does not define any data. Setting nil values");
			m_BuildMetaData = new BuildMetaData();
		}
		else
		{
			d.LogErrorFormat("Could not load BuildMetaDataObject at path {0}. Is the file missing? Setting nil values", text);
			m_BuildMetaData = new BuildMetaData();
		}
	}
}
