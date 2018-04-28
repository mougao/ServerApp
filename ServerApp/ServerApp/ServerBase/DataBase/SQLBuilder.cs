using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoYoo.Engine.DataBase
{
    public enum SQLCommandType
    {
        SELECT,
        INSERT,
        UPDATE,
    }

    public interface ISQLCommandSaver
    {
        void SaveTo(SQLCommandBase scb);
    }

    public class SQLBuilder
    {
        public static SQLInsertCommand INSERT
        {
            get { return new SQLInsertCommand(); }
        }

        public static SQLInsertOrUpdateCommand INSERTORUPDATE
        {
            get { return new SQLInsertOrUpdateCommand(); }
        }

        public static SQLUpdateCommand UPDATE
        {
            get { return new SQLUpdateCommand(); }
        }

        public static ProcedureCommand PROCEDURE
        {
            get { return new ProcedureCommand(); }
        }
    }

    public class SQLField
    {
        public SQLField(string name, string value, bool isKey)
        {
            Name = name;
            Value = value == null ? "" : FilterSqlInject(value);
            IsKey = isKey;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsKey { get; set; }

        private string FilterSqlInject(string value)
        {
            return value.Replace('\'', '\u0027').Replace('-', '\u002D');
        }
    }

    public class SQLFields : List<SQLField>
    {
        public IEnumerable<string> Names
        { 
            get { return this.Select(field => field.Name); } 
        }
        public IEnumerable<string> Values
        {
            get { return this.Select(field => field.Value); } 
        }
    }

    public abstract class SQLCommandBase
    {
        public SQLCommandType CommandType { get; protected set; }
        public string TableName { get; protected set; }
        public string WhereString { get; protected set; }

        protected readonly SQLFields m_Fields = new SQLFields();
        public SQLFields Fields
        {
            get { return m_Fields; }
        }
        protected SQLCommandBase()
        {
        }
        public virtual SQLCommandBase VALUE(string name, string value, bool isKey = false)
        {
            m_Fields.Add(new SQLField(string.Format("`{0}`", name), string.Format("'{0}'", value), isKey));
            return this;
        }
        public virtual SQLCommandBase TABLE(string table_name)
        {
            TableName = table_name;
            return this;
        }
        public virtual SQLCommandBase WHERE(string whereStr)
        {
            WhereString = whereStr;
            return this;
        }
        public virtual SQLCommandBase WHERE(string formatStr, params object[] args)
        {
            WhereString = string.Format(formatStr, args);
            return this;
        }
        public virtual SQLCommandBase Save(ISQLCommandSaver saver)
        {
            saver.SaveTo(this);
            return this;
        }
        public abstract string GetSQL();
        public abstract string GetGroup();
        public override string ToString()
        {
            return GetSQL();
        }
    }

    public class SQLInsertCommand : SQLCommandBase
    {
        public SQLInsertCommand()
        {
            CommandType = SQLCommandType.INSERT;
        }

        private bool m_Ignore = false;

        public override SQLCommandBase WHERE(string where_string)
        {
            throw new Exception("INSERT operation can't set where condition");
        }

        public SQLCommandBase IGNORE
        {
            get
            {
                m_Ignore = true;
                return this;
            }
        }

        public override string GetSQL()
        {
            return string.Format(
                "INSERT{3} INTO `{0}`({1}) VALUES({2})",
                TableName, 
                string.Join(",", m_Fields.Names), 
                string.Join(",", m_Fields.Values), 
                m_Ignore ? " IGNORE" : string.Empty);
        }

        public override string GetGroup()
        {
            return string.Format("INSERT {0}", TableName);
        }
    }

    public class SQLInsertOrUpdateCommand : SQLCommandBase
    {
        public SQLInsertOrUpdateCommand()
        {
            CommandType = SQLCommandType.INSERT;
        }

        public override SQLCommandBase WHERE(string where_string)
        {
            throw new Exception("INSERT operation can't set where condition");
        }

        public override string GetSQL()
        {
            return string.Format(
                "INSERT INTO `{0}`({1}) VALUES({2}) ON DUPLICATE KEY UPDATE {3}",
                TableName,
                string.Join(",", m_Fields.Names), 
                string.Join(",", m_Fields.Values),
                string.Join(",", m_Fields.Where(field => !field.IsKey).Select(field => field.Name + "=" + field.Value)));
        }

        public override string GetGroup()
        {
            return string.Format("INSERT {0}", TableName);
        }
    }

    public class SQLUpdateCommand : SQLCommandBase
    {
        public SQLUpdateCommand()
        {
            CommandType = SQLCommandType.UPDATE;
        }

        public override string GetSQL()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE `{0}` SET ",TableName);
            int len = m_Fields.Count;
            for (int i = 0; i < len;++i )
            {
                sb.AppendFormat("{0}={1}", m_Fields[i].Name, m_Fields[i].Value);
                if(i != len - 1)
                {
                    sb.Append(",");
                }
            }
            if (!string.IsNullOrWhiteSpace(WhereString))
            {
                sb.AppendFormat(" WHERE {0}" , WhereString);
            }
            return sb.ToString();
        }
        public override string GetGroup()
        {
            return string.Format("UPDATE {0}", TableName);
        }
    }

    public class ProcedureCommand : SQLCommandBase
    {
        public override string GetSQL()
        {
            return string.Format("CALL `{0}`({1})", TableName, string.Join(",", m_Fields.Values));
        }

        public override string GetGroup()
        {
            return string.Format("CALL {0}", TableName);
        }
    }
}
