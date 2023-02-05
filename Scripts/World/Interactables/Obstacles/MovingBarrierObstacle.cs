using UnityEngine;

public class MovingBarrierObstacle : BarrierObstacle
{
    #region Fields

    [SerializeField]
    private Transform leftWaypoint;
    [SerializeField]
    private Transform rightWaypoint;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool randomFirstPosition = true;
    [SerializeField]
    private bool randomDirection = true;

    #endregion

    #region Propeties

    public Transform LeftWaypoint { get => leftWaypoint; }
    public Transform RightWaypoint { get => rightWaypoint; }
    public float Speed { get => speed; }
    public bool RandomFirstPosition { get => randomFirstPosition; }
    public bool RandomDirection { get => randomDirection; }

    private Vector3 LeftPositon { get; set; }
    private Vector3 RightPosition { get; set; }
    private int Direction { get; set; } = 1;

    #endregion

    #region Methods

    private void Start()
    {
        if(RandomDirection == true)
        {
            Direction = (UnityEngine.Random.Range(0, 10) / 2) == 0 ? -1 : 1;
        }

        LeftPositon = LeftWaypoint.position;
        RightPosition = RightWaypoint.position;

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, UnityEngine.Random.Range(LeftPositon.z, RightPosition.z));
        transform.position = newPosition;
    }

    private void Update()
    {
        float step = Direction * Speed * Time.deltaTime;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + step);

        if(newPosition.z >= RightPosition.z)
        {
            newPosition.z = RightPosition.z;
            Direction = -1;
        }
        else if(newPosition.z <= LeftPositon.z)
        {
            newPosition.z = LeftPositon.z;
            Direction = 1;
        }

        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        // Dla celow debugowych.
        BoxCollider colider = GetComponent<BoxCollider>();

        if(colider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(LeftWaypoint.position + colider.center, colider.size);
            Gizmos.DrawWireCube(RightWaypoint.position + colider.center, colider.size);
        }
    }

    #endregion

    #region Enums


    #endregion
}
