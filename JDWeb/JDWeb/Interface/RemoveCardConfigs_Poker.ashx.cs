using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    /// <summary>
    /// RemoveCardConfigs_Poker 的摘要说明
    /// </summary>
    public class RemoveCardConfigs_Poker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string index = context.Request["index"];

            DataBase db = DBProvide.GetLocalDBBase();

            string sql = string.Format("DELETE FROM card_configs_poker WHERE `index`={0}", index);

            db.Write(sql);

            db.Close();

            string rr = JsonConvert.SerializeObject("移除成功");
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