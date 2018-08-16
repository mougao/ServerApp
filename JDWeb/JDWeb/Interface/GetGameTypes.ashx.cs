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
    /// GetGameTypes 的摘要说明
    /// </summary>
    public class GetGameTypes : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DataBase db = DBProvide.GetLocalDBBase();

            string sql = string.Format("select * from gametypes");
            List<string> gametypes = new List<string>();
            db.Read(sql, (DataSet ds) =>
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string gametype = row["GameType"].ToString();

                        gametypes.Add(string.Format("<option value={0}>{1}</option>", gametype, gametype));
                    }

                }

            });

            db.Close();

            string rr = JsonConvert.SerializeObject(gametypes);

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