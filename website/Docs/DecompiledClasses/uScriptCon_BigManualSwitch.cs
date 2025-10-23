using System;

[NodeToolTip("Manually pick an Output to fire the signal to.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Conditions/Switches")]
[FriendlyName("Big Manual Switch", "Manually pick an Output to fire the signal to.\n\nThe specified Output To Use value will be clamped within the range of 1 to 8.")]
[NodeHelp("http://docs.uscript.net/#3-Working_With_uScript/3.4-Nodes.htm")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptCon_BigManualSwitch : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private int m_CurrentOutput = 1;

	private bool m_SwitchOpen = true;

	[FriendlyName("Output 1")]
	public event uScriptEventHandler Output1;

	[FriendlyName("Output 2")]
	public event uScriptEventHandler Output2;

	[FriendlyName("Output 3")]
	public event uScriptEventHandler Output3;

	[FriendlyName("Output 4")]
	public event uScriptEventHandler Output4;

	[FriendlyName("Output 5")]
	public event uScriptEventHandler Output5;

	[FriendlyName("Output 6")]
	public event uScriptEventHandler Output6;

	[FriendlyName("Output 7")]
	public event uScriptEventHandler Output7;

	[FriendlyName("Output 8")]
	public event uScriptEventHandler Output8;

	[FriendlyName("Output 9")]
	public event uScriptEventHandler Output9;

	[FriendlyName("Output 10")]
	public event uScriptEventHandler Output10;

	[FriendlyName("Output 11")]
	public event uScriptEventHandler Output11;

	[FriendlyName("Output 12")]
	public event uScriptEventHandler Output12;

	[FriendlyName("Output 13")]
	public event uScriptEventHandler Output13;

	[FriendlyName("Output 14")]
	public event uScriptEventHandler Output14;

	[FriendlyName("Output 15")]
	public event uScriptEventHandler Output15;

	[FriendlyName("Output 16")]
	public event uScriptEventHandler Output16;

	public void In([FriendlyName("Output To Use", "The output switch to use.")] int CurrentOutput)
	{
		m_CurrentOutput = CurrentOutput;
		if (m_SwitchOpen)
		{
			switch (m_CurrentOutput)
			{
			case 1:
				this.Output1(this, new EventArgs());
				break;
			case 2:
				this.Output2(this, new EventArgs());
				break;
			case 3:
				this.Output3(this, new EventArgs());
				break;
			case 4:
				this.Output4(this, new EventArgs());
				break;
			case 5:
				this.Output5(this, new EventArgs());
				break;
			case 6:
				this.Output6(this, new EventArgs());
				break;
			case 7:
				this.Output7(this, new EventArgs());
				break;
			case 8:
				this.Output8(this, new EventArgs());
				break;
			case 9:
				this.Output9(this, new EventArgs());
				break;
			case 10:
				this.Output10(this, new EventArgs());
				break;
			case 11:
				this.Output11(this, new EventArgs());
				break;
			case 12:
				this.Output12(this, new EventArgs());
				break;
			case 13:
				this.Output13(this, new EventArgs());
				break;
			case 14:
				this.Output14(this, new EventArgs());
				break;
			case 15:
				this.Output15(this, new EventArgs());
				break;
			case 16:
				this.Output16(this, new EventArgs());
				break;
			}
		}
	}
}
