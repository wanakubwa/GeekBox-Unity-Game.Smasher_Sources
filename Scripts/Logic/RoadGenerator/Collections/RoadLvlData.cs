using System;
using UnityEngine;

namespace RoadGenerator
{
    [Serializable]
    public class RoadLvlData
    {
        #region Fields

        [SerializeField]
        private int lvlThreshold = 10;

        [Space]
        [SerializeField]
        private int maxSegments = 5;
        [SerializeField]
        private RoadSegment[] segments;

        #endregion

        #region Propeties

        public int LvlThreshold {
            get => lvlThreshold;
        }
        public int MaxSegments {
            get => maxSegments;
        }
        public RoadSegment[] Segments { 
            get => segments;
        }

        #endregion

        #region Methods



        #endregion

        #region Enums



        #endregion
    }
}
