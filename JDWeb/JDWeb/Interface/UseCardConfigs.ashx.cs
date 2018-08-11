using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    /// <summary>
    /// 使用牌型配置
    /// </summary>
    public class UseCardConfigs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string index = context.Request["index"];
            string gametype = context.Request["gametype"];

            DataBase db = DBProvide.GetLocalDBBase();

            string sqlclear = string.Format("UPDATE card_configs SET `Status`=0 WHERE GameType='{0}'", gametype);
            string sql = string.Format("UPDATE card_configs SET `Status`=1 WHERE `index`={0}", index);

            db.Write(sqlclear);
            db.Write(sql);

            string rr = JsonConvert.SerializeObject("激活成功");
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