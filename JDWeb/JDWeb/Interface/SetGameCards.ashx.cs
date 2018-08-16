using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    /// <summary>
    /// SetGameCards 的摘要说明
    /// </summary>
    public class SetGameCards : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string gametype = context.Request["gametype"];
            string cards = context.Request["cards"];
            string isuse = context.Request["isuse"];
            string remarks = context.Request["remarks"];

            DataBase db = DBProvide.GetLocalDBBase();
            int status = 0;
            if(isuse == "true")
            {
                status = 1;
            }

            if(status == 1)
            {
                string sqlclear = string.Format("UPDATE card_configs SET `Status`=0 WHERE GameType='{0}'", gametype);

                db.Write(sqlclear);
            }

            string sql = string.Format("INSERT into card_configs (GameType,Cards,`Status`,Remarks)VALUES('{0}','{1}',{2},'{3}')", gametype, cards, status, remarks);

            db.Write(sql);

            db.Close();

            context.Response.ContentType = "application/json";
            string ss = JsonConvert.SerializeObject("设置成功");
            context.Response.Write(ss);
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