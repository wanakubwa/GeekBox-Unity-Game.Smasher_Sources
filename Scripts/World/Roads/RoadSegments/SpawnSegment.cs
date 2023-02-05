using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnSegment : RoadSegment
{
    #region Fields

    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private BoxCollider roadCollider;

    #endregion

    #region Propeties

    public Transform SpawnPosition { 
        get => spawnPosition;
    }
    public BoxCollider RoadCollider { 
        get => roadCollider;
    }

    #endregion

    #region Methods

    [Button]
    private void CenterSpawnPoint()
    {
        Vector3 centerPosition = new Vector3(SpawnPosition.position.x, SpawnPosition.position.y, Mesh.localBounds.size.z / 2f);
        SpawnPosition.position = centerPosition;
    }

    #endregion

    #region Enums



    #endregion
}
