namespace ShoYoo.Engine.Script
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public static class Script
	{
		public static List<ScriptAction> Parse(string script)
		{
			char[] chars = script.ToCharArray();
			int p = 0;
			return Parse(chars, ref p);
		}

		private static List<ScriptAction> Parse(char[] script, ref int index)
		{
			bool InString = false;
			StringBuilder sb = new StringBuilder();
			List<ScriptAction> currentset = new List<ScriptAction>();
			ScriptAction currentnode = new ScriptAction();

			for (; index < script.Length; index++)
			{
				char c = script[index];
				if (InString)
					if (c == '\'' || c == '"' || c == '“' || c == '”')
						InString = false;
					else
						sb.Append(c);
				else
					switch (c)
					{
						case '"':
						case '“':
						case '”':
							InString = true;
							break;
						case ',':
						case '，':
						case ';':
						case '；':
							currentnode.Action = sb.ToString();
							sb.Clear();

							currentset.Add(currentnode);
							currentnode = new ScriptAction();
							break;
						case '(':
						case '[':
						case '{':
						case '（':
						case '［':
						case '｛':
						case '〖':
						case '【':
							index++;
							currentnode.Params = Parse(script, ref index);
							break;
						case ')':
						case ']':
						case '}':
						case '）':
						case '］':
						case '｝':
						case '〗':
						case '】':
							currentnode.Action = sb.ToString();
							if (!String.IsNullOrWhiteSpace(currentnode.Action) || currentnode.Params != null)
								currentset.Add(currentnode);
							return currentset;
						case ' ':
						case '　':
						case '\r':
						case '\n':
						case '\t':
							break;
						default:
							sb.Append(c);
							break;
					}
			}

			currentnode.Action = sb.ToString();
			if ((!String.IsNullOrWhiteSpace(currentnode.Action) || currentnode.Params != null || currentset.Count > 0))
				currentset.Add(currentnode);
			return currentset;
		}

		public static string ToString(IEnumerable<ScriptAction> scripts)
		{
			return string.Join(",", scripts.Select(script => script.ToString()));
		}
	}
}
