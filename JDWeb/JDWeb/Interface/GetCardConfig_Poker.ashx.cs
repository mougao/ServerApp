using JDWebBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JDWeb.Interface
{
    public class RetPokers
    {
        public List<string> cards = new List<string>();

        public int HandCount = 0;
    }

    /// <summary>
    /// GetCardConfig_Poker 的摘要说明
    /// </summary>
    public class GetCardConfig_Poker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string gametype = context.Request["gametype"];

            string defaultcards = "30,31,33,34,35,36,37,38,39,40,41,42,43,44,45,49,50,51,52,53,54,55,56,57,58,59,60,61,65,66,67,68,69,70,71,72,73,74,75,76,77,81,82,83,84,85,86,87,88,89,90,91,92,93";

            DataBase db = DBProvide.GetLocalDBBase();

            string sql = string.Format("select * from default_cards_poker where GameType='{0}'", gametype);

            int count = 1;
            int handcount = 5;
            string removecards = "";

            db.Read(sql, (DataSet ds) =>
            {
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        count = (int)row["Count"];
                        handcount = (int)row["HandCardCount"];
                        removecards = row["RemoveCards"].ToString();
                    }
                }
            });

            db.Close();

            for(int i=1;i<count;i++)
            {
                defaultcards += ","+defaultcards;
            }

            List<string> listdefaults = new List<string>();
            listdefaults.AddRange(defaultcards.Split(','));

            List<string> lstremoves = new List<string>();
            lstremoves.AddRange(removecards.Split(','));
                
            for (int i=0;i< lstremoves.Count;i++)
            {
                if(listdefaults.Contains(lstremoves[i]))
                {
                    listdefaults.Remove(lstremoves[i]);
                }
            }

            List<string> dic = new List<string>();

            foreach (string scardid in listdefaults)
            {
                int cardid = -1;
                int.TryParse(scardid, out cardid);

                if (cardid != -1)
                {
                    string cardname = PokerTF.GetPokerName(cardid, gametype);
                    dic.Add(string.Format("<option value={0}>{1}</option>", scardid, cardname));
                }
            }

            RetPokers ret = new RetPokers();
            ret.cards = dic;
            ret.HandCount = handcount;

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