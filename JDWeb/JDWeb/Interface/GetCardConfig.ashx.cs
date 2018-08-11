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
    /// 获取默认牌
    /// </summary>
    public class GetCardConfig : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string gametype = context.Request["gametype"];

            string defaultcards = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41";

            DataBase db = DBProvide.GetLocalDBBase();

            DBRet ret = new DBRet();
            string sql = string.Format("select * from default_cards where GameType='{0}'", gametype);

            db.Read(sql, (DataSet ds) =>
            {
                if (ds.Tables.Count > 0)
                {
                    foreach(DataRow row in ds.Tables[0].Rows)
                    {
                        defaultcards = row["DefaultCards"].ToString();
                    }
                }
            });

            string[] cards = defaultcards.Split(',');

            List<int> ncards = new List<int>();
            List<string> dic = new List<string>();

            foreach(string scardid in cards)
            {
                int cardid = -1;
                int.TryParse(scardid, out cardid);

                if(cardid != -1)
                {
                    MahjongType flag;
                    if (Enum.TryParse<MahjongType>(scardid, out flag))
                    {
                        if(cardid >= (int)MahjongType.春)
                        {
                            dic.Add(string.Format("<option value={0}>{1}</option>", scardid, flag.ToString()));
                        }
                        else
                        {
                            dic.Add(string.Format("<option value={0}>{1}</option>", scardid, flag.ToString()));
                            dic.Add(string.Format("<option value={0}>{1}</option>", scardid, flag.ToString()));
                            dic.Add(string.Format("<option value={0}>{1}</option>", scardid, flag.ToString()));
                            dic.Add(string.Format("<option value={0}>{1}</option>", scardid, flag.ToString()));
                        }
                        
                    }
                }
            }

            string rr = JsonConvert.SerializeObject(dic);

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