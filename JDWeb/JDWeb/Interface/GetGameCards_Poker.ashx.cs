using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    /// <summary>
    /// GetGameCards_Poker 的摘要说明
    /// </summary>
    public class GetGameCards_Poker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataBase db = DBProvide.GetLocalDBBase();

            DBRet ret = new DBRet();
            string sql = string.Format("select * from card_configs_poker");

            db.Read(sql, (DataSet ds) =>
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string cards = row["Cards"].ToString();

                        if (cards.Length > 150)
                        {
                            row["Cards"] = cards.Substring(0, 150) + "...";
                        }

                    }

                    ret.data = ds.Tables[0];
                }

            });

            db.Close();

            string rr = JsonConvert.SerializeObject(ret);

            context.Response.ContentType = "application/json";
            context.Response.Write(rr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}