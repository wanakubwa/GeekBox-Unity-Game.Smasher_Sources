using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Road : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private SpawnSegment spawnSegment;
    [SerializeField]
    private WallSpawner endSegment;
    [SerializeField]
    private List<RoadSegment> spawnedSegments = new List<RoadSegment>();

    #endregion

    #region Propeties

    public Transform SpawnPosition { 
        get => SpawnSegment.SpawnPosition; 
    }

    public List<RoadSegment> SpawnedSegments { get => spawnedSegments; }
    public SpawnSegment SpawnSegment { get => spawnSegment; }
    public WallSpawner EndSegment { get => endSegment; }

    #endregion

    #region Methods

    public void Init()
    {
        Bounds segmentsBound = SpawnSegment.Mesh.localBounds;
        foreach (RoadSegment segment in SpawnedSegments)
        {
            segmentsBound.Expand(new Vector3(segment.Mesh.localBounds.size.x, 0f, 0f));
            segmentsBound.center = new Vector3(segmentsBound.center.x - segment.Mesh.localBounds.size.x * 0.5f, segmentsBound.center.y, segmentsBound.center.z);
        }

        SpawnSegment.RoadCollider.size = segmentsBound.size;
        SpawnSegment.RoadCollider.center = segmentsBound.center;
    }

    public void SpawnSegments(List<RoadSegment> segmentsPrefab)
    {
        RoadSegment lastSegment = SpawnSegment;

        foreach (RoadSegment segment in segmentsPrefab)
        {
            RoadSegment newSegment = Instantiate(segment);
            newSegment.transform.ResetParent(transform);
            newSegment.transform.position = new Vector3(lastSegment.transform.position.x - lastSegment.LenghtUnits, lastSegment.transform.position.y, lastSegment.transform.position.z);
            SpawnedSegments.Add(newSegment);

            lastSegment = newSegment;
        }

        EndSegment.transform.position = new Vector3(lastSegment.transform.position.x - lastSegment.LenghtUnits, lastSegment.transform.position.y, lastSegment.transform.position.z);
    }

    #endregion

    #region Enums



    #endregion
}
