using UnityEngine;
using RoadGenerator;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoadGeneratorSettings.asset", menuName = "Settings/RoadGeneratorSettings")]
public class RoadGeneratorSettings : ScriptableObject
{
    #region Fields

    private static RoadGeneratorSettings instance;

    [SerializeField]
    private RoadLvlData[] lvlsCollection;

    #endregion

    #region Propeties

    public static RoadGeneratorSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<RoadGeneratorSettings>("Settings/RoadGeneratorSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public RoadLvlData[] LvlsCollection {
        get => lvlsCollection; 
    }

    #endregion

    #region Methods

    public void GetDataForLvl(int lvl, out List<RoadSegment> segments, out int maxLength)
    {
        maxLength = Constants.DEFAULT_VALUE;
        segments = new List<RoadSegment>();

        int lastLvlThreshold = Constants.DEFAULT_VALUE;

        for (int i = 0; i < LvlsCollection.Length; i++)
        {
            if(lastLvlThreshold <= lvl)
            {
                segments.AddRange(LvlsCollection[i].Segments);
                maxLength = LvlsCollection[i].MaxSegments;

                lastLvlThreshold = LvlsCollection[i].LvlThreshold;
            }
        }
    }

    #endregion

    #region Enums



    #endregion
}
