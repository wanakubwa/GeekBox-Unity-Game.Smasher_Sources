using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private int startWallsBuffor = 10;
    [SerializeField]
    private int spawnOffset;
    [SerializeField]
    private int speedIncrease = 10;
    [SerializeField]
    private int startSpeed = 50;

    [SerializeField]
    private Transform startSpawnPoint;
    [SerializeField]
    private DestroyableWall[] wallPrefabs;

    #endregion

    #region Propeties

    public int StartWallsBuffor { get => startWallsBuffor; }
    public int SpawnOffset { get => spawnOffset; }
    public int SpeedIncrease { get => speedIncrease; }
    public Transform StartSpawnPoint { get => startSpawnPoint; }
    public DestroyableWall[] WallPrefabs { get => wallPrefabs; }
    public int StartSpeed { get => startSpeed; }

    // Variables.
    private List<DestroyableWall> SpawnedWalls { get; set; } = new List<DestroyableWall>();
    private int TotalWallsCounter { get; set; }
    private int DestroyedWalls { get; set; } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public void OnDestroyWall(DestroyableWall sender)
    {
        SpawnedWalls.Remove(sender);
        SpawnWallWithIndex(TotalWallsCounter);

        GameplayEvents.Instance.NotifyBallBreakWall();
        HapticsManager.Instance.TryVibrate(HapticsManager.Instance.WallAmplitude);
        DestroyedWalls++;
    }

    public void OnBallStop(DestroyableWall sender)
    {
        GameplayEvents.Instance.NotifyBallStopOnWall(DestroyedWalls);
    }

    private void Awake()
    {
        SpawnBufforWalls();
    }

    private void SpawnBufforWalls()
    {
        for(int i =0; i < StartWallsBuffor; i++)
        {
            SpawnWallWithIndex(i);
        }
    }

    private void SpawnWallWithIndex(int index)
    {
        DestroyableWall prefab = WallPrefabs.GetRandomElement();
        DestroyableWall spawnedWall = Instantiate(prefab);
        spawnedWall.transform.ResetParent(transform);
        spawnedWall.transform.position = new Vector3(startSpawnPoint.position.x + index * (-1f)* prefab.Size.x, startSpawnPoint.position.y, startSpawnPoint.position.z);
        spawnedWall.Init(StartSpeed + index * SpeedIncrease, this);

        TotalWallsCounter++;
        SpawnedWalls.Add(spawnedWall);
    }

    #endregion

    #region Enums



    #endregion
}
