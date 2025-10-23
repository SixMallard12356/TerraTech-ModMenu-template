#define UNITY_EDITOR
public static class VirtualKeyboard
{
	public delegate void EntryCompleteDelegate(bool inputAccepted, string inputResult);

	public struct PlatformParams
	{
		public ManXboxOne.VirtualKeyboardInputMode xb_vkInputMode;

		public bool ps_multiLine;

		public int ps_maxInputLength;

		public static PlatformParams Default => m_DefaultPlatformParams;
	}

	private static readonly PlatformParams m_DefaultPlatformParams = new PlatformParams
	{
		xb_vkInputMode = ManXboxOne.VirtualKeyboardInputMode.AlphaNumeric,
		ps_multiLine = false,
		ps_maxInputLength = 22
	};

	public static void PromptInput(string keyboardTitle, string keyboardDesc, string defaultText, EntryCompleteDelegate onCompleteHandler)
	{
		PromptInput(keyboardTitle, keyboardDesc, defaultText, onCompleteHandler, PlatformParams.Default);
	}

	public static void PromptInput(string keyboardTitle, string keyboardDesc, string defaultText, EntryCompleteDelegate onCompleteHandler, PlatformParams platformParams)
	{
		d.Assert(onCompleteHandler != null, "VirtualKeyboard.PromptInput - Virtual Keyboard input requires completion callback handler!");
		d.LogError("VirtualKeyboard.PromptInput - Virtual keyboard not implementated for this platform!");
	}

	public static bool IsRequired()
	{
		return false;
	}
}
