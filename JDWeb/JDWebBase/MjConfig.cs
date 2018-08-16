using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDWebBase
{
    //"0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41";
    
    public enum MahjongType 
    {
        一万 = 0,        // 万子 1
        二万 = 1,        // 万子 2
        三万 = 2,        // 万子 3
        四万 = 3,        // 万子 4
        五万 = 4,        // 万子 5
        六万 = 5,        // 万子 6
        七万 = 6,        // 万子 7
        八万 = 7,        // 万子 8
        九万 = 8,        // 万子 9

        一条 = 9,         // 索子 1
        二条 = 10,        // 索子 2
        三条 = 11,        // 索子 3
        四条 = 12,        // 索子 4
        五条 = 13,        // 索子 5
        六条 = 14,        // 索子 6
        七条 = 15,        // 索子 7
        八条 = 16,        // 索子 8
        九条 = 17,        // 索子 9

        一筒 = 18,        // 同子 1
        二筒 = 19,        // 同子 2
        三筒 = 20,        // 同子 3
        四筒 = 21,        // 同子 4
        五筒 = 22,        // 同子 5
        六筒 = 23,        // 同子 6
        七筒 = 24,        // 同子 7
        八筒 = 25,        // 同子 8
        九筒 = 26,        // 同子 9

        东风 = 27,        // 东风
        南风 = 28,        // 南风
        西风 = 29,        // 西风
        北风 = 30,        // 北风
        红中 = 31,        // 红中
        发财 = 32,        // 发财
        白板 = 33,        // 白板

        春 = 34,          // 春
        夏 = 35,          // 夏
        秋 = 36,          // 秋
        东 = 37,          // 东

        梅 = 38,          // 梅
        兰 = 39,          // 兰
        菊 = 40,          // 菊
        竹 = 41,          // 竹

    }

    /// <summary>
    /// 扑克牌花色
    /// </summary>
    public enum PokerSuit : byte
    {
        /// <summary>
        /// 大小王
        /// </summary>
        Joker = 1,
        /// <summary>
        /// 方块
        /// </summary>
        Diamond,
        /// <summary>
        /// 梅花
        /// </summary>
        Club,
        /// <summary>
        /// 红桃
        /// </summary>
        Heart,
        /// <summary>
        /// 黑桃
        /// </summary>
        Spade,
    }

    public class PokerTF
    {

        public static List<Tuple<string,int>> TestPoker()
        {
            var tempCards = new List<int>();
            for (int i = 1; i < 14; i++)
            {
                tempCards.Add(CreatePokerInt(PokerSuit.Club, i));
                tempCards.Add(CreatePokerInt(PokerSuit.Diamond, i));
                tempCards.Add(CreatePokerInt(PokerSuit.Heart, i));
                tempCards.Add(CreatePokerInt(PokerSuit.Spade, i));
            }

            tempCards.Add(CreatePokerInt(PokerSuit.Joker, 14));
            tempCards.Add(CreatePokerInt(PokerSuit.Joker, 15));

            //var randArray = Enumerable.Range(0, 52).OrderBy(c => Guid.NewGuid()).ToArray();

            List<Tuple<string, int>> ret = new List<Tuple<string, int>>();

            foreach(int id in tempCards)
            {
                ret.Add(new Tuple<string, int>(GetPokerName(id),id));
            }

            return ret;
        }


        public static int CreatePokerInt(PokerSuit color,int num)
        {
            int ret = (byte)((int)((int)color << 4) | num);

            return ret;
        }

        private static PokerSuit GetSuit(int poker)
        {
            return (PokerSuit)(poker >> 4);
        }

        private static int GetNumber(int poker)
        {
            return (int)(poker & 15);
        }

        public static string GetPokerName(int pokervalue)
        {
            string ret = "";

            PokerSuit suit = GetSuit(pokervalue);

            int num = GetNumber(pokervalue);

            switch(suit)
            {
                case PokerSuit.Club:
                    {
                        ret = string.Format("梅花{0}", num);
                        break;
                    }
                case PokerSuit.Diamond:
                    {
                        ret = string.Format("方块{0}", num);
                        break;
                    }
                case PokerSuit.Heart:
                    {
                        ret = string.Format("红桃{0}", num);
                        break;
                    }
                case PokerSuit.Spade:
                    {
                        ret = string.Format("黑桃{0}", num);
                        break;
                    }
                case PokerSuit.Joker:
                    {
                        ret = string.Format("王{0}", num);
                        break;
                    }
            }

            return ret;
        }

    }
}
