using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class RoadManager : ManagerSingletonBase<RoadManager>, IGameEvents
{
    #region Fields

    private const string CONTAINER_TAG = "World_Container";

    [SerializeField]
    private Road roadprefab;

    #endregion

    #region Propeties

    public Road RoadPrefab {
        get => roadprefab;
    }

    //Variables.
    public Road CurrentRoad { get; private set; }
    private Transform SceneWorldContainer { get; set; }
    private List<RoadSegment> LastSegments { get; set; } = new List<RoadSegment>();

    private RoadGeneratorSettings GeneratorSettings { get; set; }

    #endregion

    #region Methods

    public void LoadNextLvl()
    {
        LoadNextRoad();
    }

    public void RestartLvl()
    {
        ResetCurrentRoad();
    }

    public override void Initialize()
    {
        base.Initialize();

        GeneratorSettings = RoadGeneratorSettings.Instance;
    }

    public override void LoadContent()
    {
        base.LoadContent();

        SceneWorldContainer = GameObject.FindGameObjectWithTag(CONTAINER_TAG).transform;
        SpawnRoad(RoadPrefab);
    }

    public void StopLvlGame()
    {
        
    }

    public void StartLvlGame()
    {
        
    }

    private void ResetCurrentRoad()
    {
        SpawnRoad(RoadPrefab, false);
    }

    private void LoadNextRoad()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        SpawnRoad(RoadPrefab);

        UnityEngine.Debug.LogFormat("Trasa wygenerowana: {0}ms", watch.ElapsedMilliseconds);
    }

    private void SpawnRoad(Road prefab, bool rollSegments = true)
    {
        if(CurrentRoad != null)
        {
            Destroy(CurrentRoad.gameObject);
        }

        CurrentRoad = Instantiate(prefab);
        CurrentRoad.transform.ResetParent(SceneWorldContainer);

        if(rollSegments == true || LastSegments.Count < 1)
        {
            int currentLvlIndex = PlayerManager.Instance.Wallet.CurrentLvlNo;
            GeneratorSettings.GetDataForLvl(currentLvlIndex, out List<RoadSegment> segments, out int length);
            List<RoadSegment> segmentsToSpawn = RollSegments(length, segments);
            LastSegments = segmentsToSpawn;
        }

        CurrentRoad.SpawnSegments(LastSegments);
        CurrentRoad.Init();
    }

    private List<RoadSegment> RollSegments(int capacity, List<RoadSegment> segements)
    {
        List<RoadSegment> output = new List<RoadSegment>();
        List<RoadSegment> notUsedSegemtns = new List<RoadSegment>(segements);
        notUsedSegemtns = notUsedSegemtns.Shuffle();

        int currentCapacity = Constants.DEFAULT_VALUE;

        for (int i =0; i < notUsedSegemtns.Count; i++)
        {
            if(notUsedSegemtns[i].RoadUnits + currentCapacity <= capacity)
            {
                // Dodaj ten element.
                output.Add(notUsedSegemtns[i]);
                currentCapacity += notUsedSegemtns[i].RoadUnits;
            }

            if(currentCapacity > capacity)
            {
                break;
            }
        }

        return output;
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        SaveLoadManager.Instance.OnResetCompleted += ResetGame;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= ResetGame;
        }
    }

    private void ResetGame()
    {
        SpawnRoad(RoadPrefab, true);
    }

    #endregion

    #region Enums



    #endregion
}
