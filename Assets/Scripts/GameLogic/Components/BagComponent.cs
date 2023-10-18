using System.Collections.Generic;
using Leopotam.EcsLite;

namespace GameLogic.Components
{
    public struct BagComponent
    {
        public Stack<EcsPackedEntity> items;
        public int maxCount;
    }
}