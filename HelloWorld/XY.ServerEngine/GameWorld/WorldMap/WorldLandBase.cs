using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 土地（地图内的土地排布 九宫格？）
    /// </summary>
    public class WorldLandBase
    {
        public MapPosition _CurPosition = null;

        public MapEventType _Type = MapEventType.None;

        private BattleActor _Enemy = null;

        public WorldLandBase(int map_index,int x_index,int y_index)
        {
            _CurPosition = new MapPosition();
            _CurPosition.MapIndex = map_index;
            _CurPosition.MapXIndex = x_index;
            _CurPosition.MapYIndex = y_index;
        }

        public void InitLandEvent(MapEventType type)
        {
            switch(type)
            {
                case MapEventType.Fight:
                    {
                        //创建战斗对象
                        _Enemy = WorldTemplateCreater.GetBattleActor();

                        break;
                    }
            }

        }
        
        //没有事件

        //遇到NPC

        //遇到怪

        //特殊事件

    }
}
