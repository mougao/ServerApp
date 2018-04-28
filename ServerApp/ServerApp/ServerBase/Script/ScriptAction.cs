namespace ShoYoo.Engine.Script
{
    using System.Collections.Generic;
    using System.Text;

    public class ScriptActionPrefix
    {
        private List<string> _Prefixes = new List<string>();
        public bool this[string prefix]
        {
            get
            {
                return _Prefixes.Contains(prefix);
            }
        }

        public void Add(string prefix)
        {
            if (!_Prefixes.Contains(prefix))
                _Prefixes.Add(prefix);
        }
    }

    public class ScriptAction
    {
        private static Dictionary<string, string> _ValidPrefixes = new Dictionary<string, string>();
        static ScriptAction()
        {
            _ValidPrefixes.Add("!", "!");
            _ValidPrefixes.Add("！", "!");
            _ValidPrefixes.Add("?", "?");
            _ValidPrefixes.Add("？", "?");
        }


        private ScriptActionPrefix _Prefix = null;
        public ScriptActionPrefix Prefix
        {
            get { return _Prefix; }
        }

        private string _Action;
        public string Action
        {
            get { return _Action; }
            set
            {
                string v = value;

                while (v.Length > 0)
                {
                    string s = v.Substring(0, 1);

                    if (!_ValidPrefixes.ContainsKey(s))
                        break;

                    if (_Prefix == null)
                        _Prefix = new ScriptActionPrefix();

                    _Prefix.Add(_ValidPrefixes[s]);
                    v = v.Substring(1, v.Length - 1);
                }

                _Action = v;
            }
        }

        /// <summary>
        /// 脚本参数列表，无参数时为null。
        /// </summary>
        public List<ScriptAction> Params;

        public string this[int index]
        {
            get { return Params[index].Action; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Prefix);
            sb.Append(Action);
            if (Params != null)
            {
                sb.Append("(");
                sb.Append(string.Join(",", Params));
                sb.Append(")");
            }
            return sb.ToString();
        }
    }
}
