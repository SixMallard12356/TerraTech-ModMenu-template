public static class FMODEventHelpers
{
	public static void SetParamValue(this FMODEvent.FMODParams[] paramList, int paramIdx, float paramValue)
	{
		paramList[paramIdx].m_Value = paramValue;
	}
}
