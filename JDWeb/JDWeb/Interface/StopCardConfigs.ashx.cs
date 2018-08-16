using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    /// <summary>
    /// StopCardConfigs 的摘要说明
    /// </summary>
    public class StopCardConfigs : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string index = context.Request["index"];

            DataBase db = DBProvide.GetLocalDBBase();

            string sql = string.Format("UPDATE card_configs SET `Status`=0 WHERE `index`={0}", index);

            db.Write(sql);

            db.Close();

            string rr = JsonConvert.SerializeObject("停用成功");
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