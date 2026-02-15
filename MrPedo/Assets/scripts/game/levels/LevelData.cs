using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LevelData
    {
        public AreaData[] areas;
        public float width;

        public AreaData GetRandomArea()
        {
            return areas[UnityEngine.Random.Range(0, areas.Length)];
        }
    }
}