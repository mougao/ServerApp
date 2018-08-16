using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{

    public enum MapEventType
    {
        None,
        Fight,
    }

    /// <summary>
    /// 地图位子
    /// </summary>
    public class MapPosition
    {
        /// <summary>
        /// 地图位置索引
        /// </summary>
        public int MapIndex;
        /// <summary>
        /// 地图列索引
        /// </summary>
        public int MapXIndex;
        /// <summary>
        /// 地图行索引
        /// </summary>
        public int MapYIndex;
    }

    /// <summary>
    /// 地图管理
    /// </summary>
    public class WorldMapManager
    {
        Dictionary<int, WorldBaseMap> _Maps = new Dictionary<int, WorldBaseMap>();






    }
}
