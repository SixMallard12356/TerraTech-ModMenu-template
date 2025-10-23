using System.Collections.Generic;

public class TTMsgTypeComparer : IEqualityComparer<TTMsgType>
{
	public bool Equals(TTMsgType x, TTMsgType y)
	{
		return x == y;
	}

	public int GetHashCode(TTMsgType obj)
	{
		return (int)obj;
	}
}
