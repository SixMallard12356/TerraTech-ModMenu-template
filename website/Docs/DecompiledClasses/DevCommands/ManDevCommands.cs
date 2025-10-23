#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace DevCommands;

public class ManDevCommands : Singleton.Manager<ManDevCommands>
{
	public struct DevCommand
	{
		public string commandName;

		public Func<object> commandContext;

		public CommandParam[] commandParams;

		public MethodInfo method;

		public override string ToString()
		{
			return commandName + " " + string.Join(" ", commandParams);
		}
	}

	public struct CommandParam
	{
		public string name;

		public Type type;

		public object defaultValue;

		public DevParamAttribute paramAttrib;

		private string TypeName => GetTypeName(type);

		public override string ToString()
		{
			if (defaultValue != null)
			{
				return $"[{name}:{TypeName}]={((defaultValue is string) ? $"\"{defaultValue}\"" : defaultValue)}";
			}
			return "<" + name + ":" + TypeName + ">";
		}
	}

	public enum ParseResult
	{
		Success,
		UnknownCommand,
		UnknownNamedParam,
		InvalidParamValue,
		MissingRequiredParam,
		OutOfOrderUnnamedParam
	}

	[SerializeField]
	private DevCommandUI m_CommandUIPrefab;

	private DevCommandUI m_CommandUI;

	private int m_SuggestionIdx;

	private Dictionary<string, DevCommand> m_SuggestableCommands = new Dictionary<string, DevCommand>();

	private Dictionary<string, DevCommand> m_AvailableCommands = new Dictionary<string, DevCommand>();

	private Access m_CommandAccessLevel;

	private List<(DevCommandAttribute attrib, DevCommand commandDef)> m_AllCommands = new List<(DevCommandAttribute, DevCommand)>();

	public const string kArgSeparator = " ";

	private static readonly char[] kArgSeparatorArr = " ".ToCharArray();

	private const char kNameValuePairSeparator = '=';

	public const string kNestedStringArgSeparator = "\"";

	private static readonly char[] kNestedStringArgSeparatorArr = "\"".ToCharArray();

	private static readonly HashSet<ManGameMode.GameType> s_GameTypesWithCheatCommandsEnabled = new HashSet<ManGameMode.GameType>
	{
		ManGameMode.GameType.RaD,
		ManGameMode.GameType.Creative,
		ManGameMode.GameType.Misc
	};

	private static readonly string[] kBoolOptions = new string[2] { "true", "false" };

	private static SortedEnum s_CurrentSortedEnum;

	private HashSet<string> s_IgnoredAssemblyNames = new HashSet<string>
	{
		"mscorlib", "Ionic.Zlib", "Newtonsoft.Json", "ConsoleUtilsImport", "DataPlatformImport", "FriendsImport", "GameDVRImport", "KinectImport", "MarketplaceImport", "MultiplayerImport",
		"SmartGlassImport", "StorageImport", "StreamingInstallImport", "TextSystemsImport", "UsersImport", "XIMImport", "Common.Logging", "LitJson", "uScriptAssembly", "SonyNP",
		"GalaxyCSharp"
	};

	private List<(string, char)> s_IgnoredAssemblyNamePatterns = new List<(string, char)>
	{
		("UnityEngine", '.'),
		("System", '.'),
		("Unity", '.'),
		("Rewired", '_'),
		("BehaviourDesigner", '\0'),
		("uScript", '\0'),
		("Mono", '.'),
		("Spring", '.')
	};

	public bool HasInputFocus => m_CommandUI.HasInputFocus;

	public Access CommandAccessLevel => m_CommandAccessLevel;

	public void ShowConsole()
	{
		m_CommandUI.SetActive(active: true);
	}

	public void HideConsole()
	{
		m_CommandUI.SetActive(active: false);
	}

	public bool TryExecuteInput(string inputStr, out string resultStr)
	{
		resultStr = null;
		if (TryParseInput(inputStr, out var commandDef, out var commandParams) == ParseResult.Success)
		{
			object obj = commandDef.commandContext?.Invoke();
			object obj2 = commandDef.method.Invoke(obj, commandParams);
			bool result = true;
			if (obj2 is CommandReturn commandReturn)
			{
				if (!commandReturn.message.NullOrEmpty())
				{
					resultStr = commandReturn.message;
				}
				if (commandReturn.success.HasValue)
				{
					result = commandReturn.success.Value;
				}
			}
			return result;
		}
		return false;
	}

	public bool TryGetSuggestionForInput(string inputStr, out string outSuggestedText, out string tabCompletionStr, out int tabCompletionIdx)
	{
		outSuggestedText = inputStr;
		tabCompletionStr = null;
		tabCompletionIdx = -1;
		string[] array = SplitIntoParts(inputStr);
		if (array.Length == 0)
		{
			m_SuggestionIdx = 0;
			return false;
		}
		DevCommand value = default(DevCommand);
		string cmdInLower = array[0].ToLowerInvariant();
		bool flag = m_SuggestableCommands.TryGetValue(cmdInLower, out value);
		if (!flag && array.Length > 1)
		{
			return false;
		}
		bool flag2 = inputStr.EndsWith(" ");
		bool flag3 = inputStr.Count((char s) => s == "\""[0]) % 2 == 1;
		bool flag4 = !flag2 || flag3;
		if (array.Length == 1)
		{
			if (!flag)
			{
				if (!flag4)
				{
					m_SuggestionIdx = 0;
					return false;
				}
				Func<string, string, bool> matchPredicate = (string partial, string val) => (partial.Length <= 1) ? val.StartsWith(partial) : val.Contains(partial);
				IOrderedEnumerable<KeyValuePair<string, DevCommand>> source = from o in m_SuggestableCommands
					where matchPredicate(cmdInLower, o.Key)
					orderby (!o.Key.ToLowerInvariant().StartsWith(cmdInLower)) ? 1 : 0, o.Key
					select o;
				m_SuggestionIdx = Mathf.Clamp(m_SuggestionIdx, 0, source.Count() - 1);
				KeyValuePair<string, DevCommand>? keyValuePair = source.Skip(m_SuggestionIdx).Cast<KeyValuePair<string, DevCommand>?>().FirstOrDefault();
				if (!keyValuePair.HasValue)
				{
					return false;
				}
				value = keyValuePair.Value.Value;
			}
			if (value.commandName.Length > inputStr.Length)
			{
				outSuggestedText += value.commandName.Substring(inputStr.Length);
				tabCompletionStr = value.commandName;
				tabCompletionIdx = 0;
			}
		}
		int flags = 0;
		int num = 1;
		int num2 = -1;
		bool flag5 = false;
		for (; num < array.Length; num++)
		{
			string text = array[num];
			num2 = num - 1;
			int num3 = text.IndexOf('=');
			if (num3 >= 0)
			{
				string paramName = text.Substring(0, num3).ToLowerInvariant();
				int num4 = Array.FindIndex(value.commandParams, (CommandParam p) => p.name.ToLowerInvariant().StartsWith(paramName));
				if (num4 > 0 && num4 != num2)
				{
					flag5 = true;
					num2 = num4;
				}
			}
			else if (flag5)
			{
				break;
			}
			flags = Bitfield.Add(flags, num2);
		}
		if (flag4 && array.Length > 1 && num2 < value.commandParams.Length)
		{
			string text2 = array[array.Length - 1];
			string text3 = string.Empty;
			string text4 = text2;
			int num5 = text2.IndexOf('=');
			if (num5 >= 0)
			{
				text3 = text2.Substring(0, num5 + 1);
				text4 = text2.Substring(num5 + 1);
			}
			if (TryGetParamCompletion(text4, value.commandParams[num2], m_SuggestionIdx, out var outPredictedParamVal))
			{
				bool flag6 = outPredictedParamVal.Contains(" ");
				if (flag3 || text4.Length != outPredictedParamVal.Length || !inputStr.EndsWith("\""))
				{
					bool flag7 = flag3 || flag6;
					string text5 = (flag7 ? ("\"" + outPredictedParamVal + "\"") : outPredictedParamVal);
					int startIndex = text4.Length + (flag7 ? 1 : 0);
					outSuggestedText += text5.Substring(startIndex);
					tabCompletionStr = text3 + (flag6 ? text5 : outPredictedParamVal);
					int num6 = (flag3 ? (text2.Length + 1) : text2.Length);
					tabCompletionIdx = inputStr.Length - num6;
				}
				flags = Bitfield.Add(flags, num2);
			}
		}
		int num7 = 0;
		for (int num8 = 0; num8 < value.commandParams.Length; num8++)
		{
			if (!Bitfield.Contains(flags, num8))
			{
				CommandParam commandParam = value.commandParams[num8];
				if (num7 > 0 || !flag2)
				{
					outSuggestedText += " ";
				}
				outSuggestedText += commandParam.ToString();
				num7++;
			}
		}
		return true;
	}

	public void ChangeSuggestion(bool forward)
	{
		m_SuggestionIdx += (forward ? 1 : (-1));
	}

	public void PrintAllAvailableCommands()
	{
		foreach (KeyValuePair<string, DevCommand> item in m_SuggestableCommands.OrderBy((KeyValuePair<string, DevCommand> p) => p.Key))
		{
			m_CommandUI.AddToLog(item.Value.ToString());
		}
	}

	public void Log(string text)
	{
		m_CommandUI.AddToLog(text);
	}

	public void SetCommandAccessLevel(Access maxAllowedLevel)
	{
		bool num = s_GameTypesWithCheatCommandsEnabled.Contains(Singleton.Manager<ManGameMode>.inst.GetCurrentGameType());
		Access access = (num ? Access.Cheat : Access.Public);
		if (num)
		{
			maxAllowedLevel = (Access)Mathf.Max((int)maxAllowedLevel, (int)access);
		}
		if (m_CommandAccessLevel != maxAllowedLevel)
		{
			if (maxAllowedLevel > access)
			{
				Singleton.Manager<ManPlayer>.inst.SetPlayerHasEnabledCheatCommands();
			}
			m_CommandAccessLevel = maxAllowedLevel;
			InitAvailableCommands();
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.SetDevCommandLevel, new DevCommandLevelMessage
				{
					m_CommandLevel = (int)m_CommandAccessLevel
				});
			}
		}
	}

	private bool HasAccessToCommand(DevCommandAttribute attribConfig)
	{
		bool num = Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development, forInput: false) || (!IsMPGame() && attribConfig.Is(User.Singleplayer)) || (IsServer() && attribConfig.Is(User.Server)) || (IsClient() && attribConfig.Is(User.Client));
		bool flag = attribConfig.Access == Access.Public || attribConfig.Access <= m_CommandAccessLevel || (attribConfig.Access == Access.Cheat && Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.PublicFacing, forInput: false)) || (attribConfig.Access == Access.DevCheat && Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development, forInput: false));
		bool flag2 = !attribConfig.RnDOnly || Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD);
		return num && flag && flag2;
		static bool IsClient()
		{
			if (!IsServer())
			{
				return Singleton.Manager<ManNetwork>.inst.IsClientWaitingToJoin();
			}
			return false;
		}
		static bool IsMPGame()
		{
			return Singleton.Manager<ManNetwork>.inst.CurState != ManNetwork.State.Inactive;
		}
		static bool IsServer()
		{
			return Singleton.Manager<ManNetwork>.inst.IsServerOrWillBe;
		}
	}

	private ParseResult TryParseInput(string input, out DevCommand commandDef, out object[] commandParams)
	{
		commandDef = default(DevCommand);
		commandParams = null;
		string[] array = SplitIntoParts(input);
		if (array.Length == 0)
		{
			return ParseResult.UnknownCommand;
		}
		string key = array[0].ToLowerInvariant();
		if (!m_AvailableCommands.TryGetValue(key, out commandDef))
		{
			return ParseResult.UnknownCommand;
		}
		commandParams = new object[commandDef.commandParams.Length];
		int num = 0;
		for (int i = 0; i < commandDef.commandParams.Length; i++)
		{
			if (commandDef.commandParams[i].defaultValue != null)
			{
				commandParams[i] = commandDef.commandParams[i].defaultValue;
			}
			else
			{
				num = Bitfield.Add(num, i);
			}
		}
		int j = 1;
		bool flag = true;
		for (; j < array.Length; j++)
		{
			string text = array[j];
			int num2 = j - 1;
			string paramStr = text;
			int num3 = text.IndexOf('=');
			if (num3 >= 0)
			{
				string paramName = text.Substring(0, num3).ToLowerInvariant();
				paramStr = text.Substring(num3 + 1);
				int num4 = Array.FindIndex(commandDef.commandParams, (CommandParam p) => p.name.ToLowerInvariant() == paramName);
				if (num4 < 0)
				{
					return ParseResult.UnknownNamedParam;
				}
				if (num4 != num2)
				{
					flag = false;
					num2 = num4;
				}
			}
			else if (!flag)
			{
				break;
			}
			if (num2 >= commandDef.commandParams.Length)
			{
				return ParseResult.InvalidParamValue;
			}
			CommandParam paramDef = commandDef.commandParams[num2];
			if (!TryParseParameter(paramStr, paramDef, out commandParams[num2]))
			{
				return ParseResult.InvalidParamValue;
			}
			num = Bitfield.Remove(num, num2);
		}
		if (num > 0)
		{
			if (!flag)
			{
				return ParseResult.MissingRequiredParam;
			}
			return ParseResult.OutOfOrderUnnamedParam;
		}
		return ParseResult.Success;
	}

	private string[] SplitIntoParts(string inputStr)
	{
		if (inputStr.Contains("\""))
		{
			string[] array = inputStr.Split(kNestedStringArgSeparatorArr, StringSplitOptions.None);
			IEnumerable<string> enumerable = Enumerable.Empty<string>();
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (!text.NullOrEmpty())
				{
					enumerable = ((i % 2 != 0) ? enumerable.Append(text) : enumerable.Concat(text.Split(kArgSeparatorArr, StringSplitOptions.RemoveEmptyEntries)));
				}
			}
			return enumerable.ToArray();
		}
		return inputStr.Split(kArgSeparatorArr, StringSplitOptions.RemoveEmptyEntries);
	}

	private bool TryParseParameter(string paramStr, CommandParam paramDef, out object wrappedParamVal)
	{
		bool result = false;
		wrappedParamVal = null;
		if (paramDef.paramAttrib != null && paramDef.paramAttrib.UseCustomParse)
		{
			result = paramDef.paramAttrib.TryParse(paramStr, out wrappedParamVal);
		}
		else if (paramDef.type.IsEnum)
		{
			try
			{
				object obj = Enum.Parse(paramDef.type, paramStr, ignoreCase: true);
				if (obj != null)
				{
					result = true;
					wrappedParamVal = obj;
				}
			}
			catch (Exception message)
			{
				d.LogError(message);
			}
		}
		else
		{
			switch (Type.GetTypeCode(paramDef.type))
			{
			case TypeCode.Boolean:
			{
				if (bool.TryParse(paramStr, out var result2))
				{
					result = true;
					wrappedParamVal = result2;
				}
				break;
			}
			case TypeCode.Int32:
			{
				if (int.TryParse(paramStr, out var result3))
				{
					result = true;
					wrappedParamVal = result3;
				}
				break;
			}
			case TypeCode.Single:
			{
				if (Util.TryParseFloatInvariant(paramStr, out var value))
				{
					result = true;
					wrappedParamVal = value;
				}
				break;
			}
			case TypeCode.String:
				result = true;
				wrappedParamVal = paramStr;
				break;
			}
		}
		return result;
	}

	private static string GetTypeName(Type type)
	{
		if (type.IsEnum)
		{
			return type.Name;
		}
		return Type.GetTypeCode(type) switch
		{
			TypeCode.Boolean => "bool", 
			TypeCode.Int32 => "int", 
			TypeCode.Single => "float", 
			TypeCode.String => "string", 
			_ => type.Name, 
		};
	}

	private bool TryGetParamCompletion(string partialParamStr, CommandParam paramDef, int index, out string outPredictedParamVal)
	{
		outPredictedParamVal = null;
		IEnumerable<string> allPossibleValues = GetAllPossibleValues(paramDef, partialParamStr);
		if (allPossibleValues != null)
		{
			partialParamStr = partialParamStr.ToLowerInvariant();
			Func<string, string, bool> matchPredicate = (string partial, string val) => (partial.Length <= 1) ? val.StartsWith(partial) : val.Contains(partial);
			IOrderedEnumerable<string> source = from o in allPossibleValues
				where matchPredicate(partialParamStr, o.ToLowerInvariant())
				orderby (!o.ToLowerInvariant().StartsWith(partialParamStr)) ? 1 : 0, o
				select o;
			if (partialParamStr.NullOrEmpty())
			{
				m_SuggestionIdx = 0;
			}
			else if (m_SuggestionIdx > 0)
			{
				m_SuggestionIdx = Mathf.Clamp(m_SuggestionIdx, 0, source.Count() - 1);
			}
			string text = source.Skip(m_SuggestionIdx).FirstOrDefault();
			if (!text.NullOrEmpty() && text.Length >= partialParamStr.Length)
			{
				outPredictedParamVal = text;
				return true;
			}
		}
		return false;
	}

	private IEnumerable<string> GetAllPossibleValues(CommandParam paramDef, string partialParamStr)
	{
		if (paramDef.paramAttrib != null)
		{
			return paramDef.paramAttrib.GetAutoCompletionValues(partialParamStr);
		}
		if (paramDef.type.IsEnum)
		{
			if (s_CurrentSortedEnum == null || s_CurrentSortedEnum.EnumType != paramDef.type)
			{
				s_CurrentSortedEnum = new SortedEnum(paramDef.type);
			}
			return s_CurrentSortedEnum.AllNames();
		}
		if (Type.GetTypeCode(paramDef.type) == TypeCode.Boolean)
		{
			return kBoolOptions;
		}
		TypeCode typeCode = Type.GetTypeCode(paramDef.type);
		if (typeCode == TypeCode.Int32 || typeCode == TypeCode.Single || typeCode == TypeCode.String)
		{
			return null;
		}
		d.LogErrorFormat("No value completion exists for parameter of type '{0}'", paramDef.type);
		return null;
	}

	private void TryGatherDevCommands()
	{
		try
		{
			GatherDevCommands();
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("Exception occurred Gathering Dev Commands. One or more Commands will not be available!\n {0}", ex);
		}
	}

	private void GatherDevCommands()
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			string asmName = assembly.GetName().Name;
			if (s_IgnoredAssemblyNames.Contains(asmName) || s_IgnoredAssemblyNamePatterns.Any(((string, char) p) => MatchesPattern(asmName, p)))
			{
				continue;
			}
			IEnumerable<(MethodInfo, DevCommandAttribute)> enumerable = null;
			try
			{
				enumerable = (from m in (from t in assembly.GetTypes()
						where t.Assembly == assembly
						select t).SelectMany((Type t) => t.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
					select (m: m, attribute: m.GetCustomAttribute<DevCommandAttribute>()) into pair
					where pair.attribute != null
					select pair).ToArray();
			}
			catch (Exception ex)
			{
				if (ex is ReflectionTypeLoadException ex2)
				{
					List<(MethodInfo, DevCommandAttribute)> list = new List<(MethodInfo, DevCommandAttribute)>(ex2.Types.Length);
					int num = 0;
					for (int num2 = 0; num2 < ex2.Types.Length; num2++)
					{
						try
						{
							Type type = ex2.Types[num2];
							if (type == null)
							{
								if (num >= ex2.LoaderExceptions.Length)
								{
									goto IL_0194;
								}
								d.LogWarningFormat("ReflectionTypeLoadException inner: Failed to load type (ignoring): {0}", ex2.LoaderExceptions[num]);
								num++;
							}
							else if (!(type.Assembly != assembly))
							{
								goto IL_0194;
							}
							goto end_IL_0133;
							IL_0194:
							IEnumerable<(MethodInfo, DevCommandAttribute)> collection = from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
								select (m: m, attribute: m.GetCustomAttribute<DevCommandAttribute>()) into pair
								where pair.attribute != null
								select pair;
							list.AddRange(collection);
							end_IL_0133:;
						}
						catch (Exception ex3)
						{
							d.LogWarningFormat("ReflectionTypeLoadException uncaught: Failed to load type (ignoring): {0}", ex3);
						}
					}
					enumerable = list;
				}
				else
				{
					d.LogWarningFormat("ReflectionTypeLoadException uncaught: Failed to load types in assembly {0} (ignoring): {1}", asmName, ex);
				}
			}
			if (enumerable == null)
			{
				continue;
			}
			foreach (var item2 in enumerable)
			{
				MethodInfo item = item2.Item1;
				bool flag = false;
				Func<object> commandContext = null;
				if (item.IsStatic)
				{
					flag = true;
				}
				else
				{
					try
					{
						FieldInfo instPropInfo = null;
						if (item.ReflectedType.IsSubclassOf(typeof(Singleton.Manager)))
						{
							FieldInfo field = item.DeclaringType.GetField("inst", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
							instPropInfo = field;
						}
						bool flag2 = true;
						if (instPropInfo == null && flag2)
						{
							FieldInfo[] fields = item.DeclaringType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
							foreach (FieldInfo fieldInfo in fields)
							{
								if (fieldInfo.FieldType == fieldInfo.ReflectedType)
								{
									instPropInfo = fieldInfo;
									break;
								}
							}
						}
						if (instPropInfo != null)
						{
							commandContext = () => instPropInfo.GetValue(null);
							flag = true;
						}
					}
					catch (Exception ex4)
					{
						d.LogWarningFormat("Error when trying to query type for public static inst member: {0}", ex4);
					}
				}
				if (flag)
				{
					string methodName = ((!item2.Item2.Name.NullOrEmpty()) ? item2.Item2.Name : item.Name);
					d.AssertFormat(!m_AllCommands.Exists(((DevCommandAttribute attrib, DevCommand commandDef) p) => p.commandDef.commandName == methodName), "Duplicate DevCommand named '{0}', in {1}!", methodName, item.DeclaringType);
					ParameterInfo[] parameters = item.GetParameters();
					CommandParam[] array = new CommandParam[parameters.Length];
					for (int num4 = 0; num4 < parameters.Length; num4++)
					{
						array[num4] = new CommandParam
						{
							name = parameters[num4].Name,
							type = parameters[num4].ParameterType,
							defaultValue = (parameters[num4].HasDefaultValue ? parameters[num4].DefaultValue : null),
							paramAttrib = parameters[num4].GetCustomAttribute<DevParamAttribute>()
						};
					}
					m_AllCommands.Add((item2.Item2, new DevCommand
					{
						commandName = methodName,
						commandContext = commandContext,
						commandParams = array,
						method = item
					}));
				}
				else
				{
					d.LogErrorFormat("Invalid definition of DevCommand '{0}'! DevCommands must be static, or contained within a Singleton.Manager.", item.Name);
				}
			}
		}
		static bool MatchesPattern(string str, (string start, char separator) pattern)
		{
			bool flag3 = str.StartsWith(pattern.start);
			if (flag3 && str.Length > pattern.start.Length && pattern.separator != 0 && str[pattern.start.Length] != pattern.separator)
			{
				return false;
			}
			return flag3;
		}
	}

	private void InitAvailableCommands()
	{
		m_AvailableCommands.Clear();
		m_SuggestableCommands.Clear();
		foreach (var allCommand in m_AllCommands)
		{
			if (HasAccessToCommand(allCommand.attrib))
			{
				string text = allCommand.commandDef.commandName.ToLowerInvariant();
				d.AssertFormat(!m_AvailableCommands.ContainsKey(text), "DevCommand with name '{0}' already exists in {1}!", text, allCommand.commandDef.method.DeclaringType);
				m_AvailableCommands.Add(text, allCommand.commandDef);
				m_SuggestableCommands.Add(text, allCommand.commandDef);
			}
		}
	}

	private void OnModeSetup(Mode _)
	{
		SetCommandAccessLevel(Access.Public);
		InitAvailableCommands();
	}

	private void OnClientDevCommandLevelSet(NetworkMessage netMsg)
	{
		DevCommandLevelMessage devCommandLevelMessage = netMsg.ReadMessage<DevCommandLevelMessage>();
		SetCommandAccessLevel((Access)devCommandLevelMessage.m_CommandLevel);
	}

	private void Start()
	{
		TryGatherDevCommands();
		InitAvailableCommands();
		Singleton.Manager<ManGameMode>.inst.ModeSetupEvent.Subscribe(OnModeSetup);
		Singleton.Manager<ManMods>.inst.ModSessionLoadCompleteEvent.Subscribe(OnModSessionStarted);
		Singleton.Manager<ManDLC>.inst.OnDLCChanged.Subscribe(OnDLCChanged);
		Singleton.Manager<DebugUtil>.inst.CheatAccessChangedEvent.Subscribe(InitAvailableCommands);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.SetDevCommandLevel, OnClientDevCommandLevelSet);
	}

	private void OnModSessionStarted()
	{
		m_AllCommands.Clear();
		m_AvailableCommands.Clear();
		m_SuggestableCommands.Clear();
		TryGatherDevCommands();
		InitAvailableCommands();
	}

	private void OnDLCChanged()
	{
		InitAvailableCommands();
	}

	private void Update()
	{
		if (m_CommandUI.IsNull())
		{
			m_CommandUI = m_CommandUIPrefab.UnpooledSpawn(Singleton.Manager<ManUI>.inst.transform, worldPosStays: false);
			m_CommandUI.SetActive(active: false);
			return;
		}
		bool activeSelf = m_CommandUI.gameObject.activeSelf;
		if ((!Singleton.Manager<DebugUtil>.inst.DisableCheatInput || m_CommandUI.HasInputFocus) && Singleton.Manager<ManInput>.inst.GetButtonDoublePressUp(122))
		{
			if (!activeSelf)
			{
				ShowConsole();
			}
			else if (!m_CommandUI.HasInputFocus)
			{
				m_CommandUI.SelectInputField();
			}
			else
			{
				HideConsole();
			}
		}
		if (activeSelf && (Singleton.Manager<ManInput>.inst.GetButtonDown(24) || Singleton.Manager<ManInput>.inst.GetButtonDown(22)) && activeSelf && !Singleton.Manager<ManGameMode>.inst.IsCancelEventHandled)
		{
			HideConsole();
			Singleton.Manager<ManGameMode>.inst.SetCancelEventWasHandled(eventWasHandled: true);
		}
	}
}
