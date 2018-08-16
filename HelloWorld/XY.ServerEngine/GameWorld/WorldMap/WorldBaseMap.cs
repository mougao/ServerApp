using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 地图对象
    /// </summary>
    public class WorldBaseMap
    {
        public int _Index = 0;

        

        public Dictionary<int, List<WorldLandBase>> _Lands = new Dictionary<int, List<WorldLandBase>>();

        /// <summary>
        /// 初始化地图块
        /// </summary>
        /// <param name="x_count">列</param>
        /// <param name="y_count">行</param>
        public void InitMapLands(int x_count, int y_count)
        {
            for(int i=0;i< x_count; i++)
            {
                for(int j=0;j< y_count; j++)
                {
                    WorldLandBase land = new WorldLandBase(_Index,i,j);

                    if(_Lands.ContainsKey(i))
                    {
                        _Lands[i].Add(land);
                    }
                    else
                    {
                        List<WorldLandBase> ll = new List<WorldLandBase>();
                        ll.Add(land);

                        _Lands.Add(i, ll);
                    }

                }

            }


        }





    }
}
