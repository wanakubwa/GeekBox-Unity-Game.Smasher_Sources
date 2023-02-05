using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCameraController : MonoBehaviour
{
    #region MEMEBRS

    [SerializeField]
    private GameSceneCameraManager.CameraType typeOfCamera;
    [SerializeField]
    private float maxDistanceX;
    [SerializeField]
    private float startDistanceX;
    [SerializeField]
    private float startDistanceY;

    [Title("Animations")]
    [SerializeField]
    private float endXDistance;
    [SerializeField]
    private float endYDistance;
    [SerializeField]
    private float endXRotationAngle;

    [SerializeField]
    private GameObject confettiVfx;
    [SerializeField]
    private GameObject successVfx;
    [SerializeField]
    private GameObject speedVfx;

    #endregion

    #region PROPERTIES

    public GameSceneCameraManager.CameraType TypeOfCamera { get => typeOfCamera; }
    public float MaxDistanceX { get => maxDistanceX;  }
    public float StartDistanceX { get => startDistanceX; private set => startDistanceX = value; }
    public float StartDistanceY { get => startDistanceY; private set => startDistanceY = value; }

    public float EndXDistance { get => endXDistance; }
    public float EndYDistance { get => endYDistance; }
    public float EndXRotationAngle { get => endXRotationAngle; }

    // Variables.
    protected PlayerBall ObjectToFollow { get; set; }
    private float StartDistanceModifier { get; set; }
    private Coroutine FadeCoroutine { get; set; }
    private Coroutine SpeedTrailsCoroutine { get; set; }

    private float CurrentXDistance { get; set; }
    private float CurrentYDistance { get; set; }
    private float DefaultXRotationAngle { get; set; }

    private bool IsFollowObject { get; set; } = true;

    // TMP!!!11
    public GameObject ConfettiVfx { get => confettiVfx; }
    public GameObject SuccessVfx { get => successVfx; }
    public GameObject SpeedVfx { get => speedVfx; }

    #endregion

    #region FUNCTIONS

    public void DoSpeedEffect(float accelerationEvaluated) 
    {
        if (FadeCoroutine != null)
        {
            StopCoroutine(FadeCoroutine);
        }

        FadeCoroutine = StartCoroutine(_FadeOutAndIn(accelerationEvaluated));
    }

    public void DoEndAnimation(float animationTimeS)
    {
        StartCoroutine(_EndAnimationCoroutine(animationTimeS));
    }

    protected virtual void Start()
    {
        DefaultXRotationAngle = transform.rotation.eulerAngles.x;

        GamePlayManager.Instance.OnLvlUpdate += RefreshData;
        GameplayEvents.Instance.OnBallStopOnWall += BallStopOnWallHandler;
        //RefreshData();

        ConfettiVfx.SetActive(false);
        SuccessVfx.SetActive(false);
    }

    private void OnDestroy()
    {
        if(GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnLvlUpdate -= RefreshData;
        }

        if (GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnBallStopOnWall -= BallStopOnWallHandler;
        }
    }

    protected virtual void FixedUpdate()
    {
        if(ObjectToFollow != null && IsFollowObject == true && ObjectToFollow.IsAlive == true)
        {
            float positionX = ObjectToFollow.transform.position.x + CurrentXDistance + StartDistanceModifier;
            float positionY = ObjectToFollow.transform.position.y + CurrentYDistance;
            transform.position = new Vector3(positionX, positionY, transform.position.z);
        }
    }

    public void RefreshData()
    {
        IsFollowObject = true;
        StartDistanceModifier = 0f;

        CurrentXDistance = StartDistanceX;
        CurrentYDistance = StartDistanceY;

        ObjectToFollow = FindObjectOfType<PlayerBall>();
        FixCameraPosition();

        GameSceneCameraManager manager = FindObjectOfType<GameSceneCameraManager>();
        manager.RegisterCamera(GetComponent<Camera>(), TypeOfCamera);
    }

    private void BallStopOnWallHandler(int breakedWalls)
    {
        IsFollowObject = false;
    }

    private void FixCameraPosition()
    {
        float positionX = ObjectToFollow.transform.position.x + CurrentXDistance;
        float positionY = ObjectToFollow.transform.position.y + CurrentYDistance;
        float positionZ = ObjectToFollow.transform.position.z;

        transform.position = new Vector3(positionX, positionY, positionZ);
        transform.rotation = Quaternion.Euler(DefaultXRotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator _FadeOutAndIn(float accelerationEvaluated)
    {

        float destinationDistanceX = MaxDistanceX * accelerationEvaluated;
        while (true)
        {
            yield return null;

            StartDistanceModifier = Mathf.Lerp(StartDistanceModifier, destinationDistanceX, Time.deltaTime * 4f);
            if(StartDistanceModifier >= destinationDistanceX - 1f)
            {
                break;
            }
        }

        while (true)
        {
            yield return null;

            StartDistanceModifier = Mathf.Lerp(StartDistanceModifier, 0f, Time.deltaTime * 2f);
            if (StartDistanceModifier <= 1f)
            {
                break;
            }
        }
    }

    private IEnumerator _SpeedTrailsCoroutine(float durationS)
    {
        SpeedVfx.SetActive(true);
        yield return new WaitForSeconds(durationS);
        SpeedVfx.SetActive(false);
    }

    private IEnumerator _EndAnimationCoroutine(float animationTimeS)
    {
        float timeCounterS = Constants.DEFAULT_VALUE;
        float targetRotateXAngle = Constants.DEFAULT_VALUE;

        float xRotationDelta = Mathf.Abs(EndXRotationAngle - transform.localRotation.eulerAngles.x);

        float xRotatePerS = xRotationDelta / animationTimeS;
        float moveXUnitPerS = (EndXDistance - CurrentXDistance) / animationTimeS;
        float moveYUnitPerS = (EndYDistance - CurrentYDistance) / animationTimeS;

        while (timeCounterS < animationTimeS)
        {
            CurrentXDistance = moveXUnitPerS * Time.deltaTime + CurrentXDistance;
            CurrentYDistance = moveYUnitPerS * Time.deltaTime + CurrentYDistance;

            targetRotateXAngle = targetRotateXAngle + (xRotatePerS * Time.deltaTime);
            if(targetRotateXAngle <= xRotationDelta)
            {
                transform.Rotate(Vector3.right, xRotatePerS * Time.deltaTime);
            }

            timeCounterS += Time.deltaTime;
            yield return null;
        }
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
