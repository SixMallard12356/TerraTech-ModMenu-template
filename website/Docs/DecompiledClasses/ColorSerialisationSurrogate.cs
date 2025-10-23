using System.Runtime.Serialization;
using UnityEngine;

public sealed class ColorSerialisationSurrogate : ISerializationSurrogate
{
	public static ColorSerialisationSurrogate inst = new ColorSerialisationSurrogate();

	public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
	{
		Color32 color = (Color)obj;
		uint value = (uint)(color.r | (color.g << 8) | (color.b << 16) | (color.a << 24));
		info.AddValue("rgba", value);
	}

	public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
	{
		uint num = (uint)info.GetValue("rgba", typeof(uint));
		return (Color)new Color32((byte)num, (byte)(num >> 8), (byte)(num >> 16), (byte)(num >> 24));
	}
}
