using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestroyableWall : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject solidWallObject;
    [SerializeField]
    private BoxCollider collider;
    [SerializeField]
    private Transform breakWallParent;
    [SerializeField]
    private GameObject[] breakWalls;
    [SerializeField]
    private Vector3 size;

    [Space]
    [SerializeField]
    private float destroyDelayS;

    [SerializeField]
    private TextMeshPro valueText;

    #endregion

    #region Propeties

    public GameObject[] BreakWalls { 
        get => breakWalls; 
    }
    public GameObject SolidWallObject { 
        get => solidWallObject; 
    }
    public Transform BreakWallParent { 
        get => breakWallParent; 
    }
    public BoxCollider Collider {
        get => collider;
    }

    public int TargetSpeed { get; private set; }
    private WallSpawner CachedWallSpawned { get; set; }
    public float DestroyDelayS { get => destroyDelayS; }
    public Vector3 Size { get => size; private set => size = value;  }
    public TextMeshPro ValueText { get => valueText; }

    #endregion

    #region Methods

    public void Init(int targetSpeed, WallSpawner spawner)
    {
        TargetSpeed = targetSpeed;
        CachedWallSpawned = spawner;

        ValueText.SetText(targetSpeed.ToString());
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerBall player = collision.gameObject.GetComponent<PlayerBall>();
        if (player != null)
        {
            HandlePlayerHitWall(player);
        }
    }

    private void HandlePlayerHitWall(PlayerBall player)
    {
        if(player.Speed >= TargetSpeed)
        {
            BreakWall();
        }
        else
        {
            CachedWallSpawned.OnBallStop(this);
        }
    }

    private void BreakWall()
    {
        Collider.enabled = false;
        GameObject breakWariant = Instantiate(BreakWalls.GetRandomElement());
        breakWariant.transform.ResetParent(BreakWallParent);
        breakWariant.SetActive(true);
        SolidWallObject.SetActive(false);

        CachedWallSpawned.OnDestroyWall(this);

        AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.WALL_BREAK);
        StartCoroutine(_WaitAndDestroy(DestroyDelayS));
    }

    [Button]
    private void SetColliderSize(MeshRenderer targetObject)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = targetObject.bounds.size;
        collider.center = targetObject.transform.localPosition;
    }

    [Button]
    private void SetSize(MeshRenderer targetObject)
    {
        Size = targetObject.bounds.size;
    }

    private IEnumerator _WaitAndDestroy(float timeToWaitS)
    {
        yield return new WaitForSeconds(timeToWaitS);
        Destroy(gameObject);
    }

    #endregion

    #region Enums



    #endregion
}
