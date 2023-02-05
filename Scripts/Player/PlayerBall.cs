using UnityEngine;
using System.Collections;
using System;
using Sirenix.OdinInspector;

public class PlayerBall : MonoBehaviour
{
    #region MEMEBRS

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float leftRightSpeed = 10f;
    [SerializeField]
    private float speedUp = 1f;
    [SerializeField]
    private Rigidbody currentRigidbody;
    [SerializeField]
    private Collider mainCollider;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private float barrierCollideDelayS = 0.1f;
    [SerializeField]
    private float speedUpFactor = 0.1f;

    [Title("Speed Settings")]
    [SerializeField]
    private float maxVisualizationSpeed;
    [SerializeField]
    private float speedLimit;

    [Title("Movement Limits")]
    [SerializeField]
    private float leftZBound;
    [SerializeField]
    private float rightZBound;

    [Title("Components")]
    [SerializeField]
    private BottomDetector holeDetector;

    #endregion

    #region PROPERTIES

    public event Action<float> OnSpeedChanged = delegate { };

    public float Speed { get => speed; private set => speed = value; }
    public float LeftRightSpeed { get => leftRightSpeed; set => leftRightSpeed = value; }
    public float SpeedUp { get => speedUp; }
    public Rigidbody CurrentRigidbody { get => currentRigidbody; }
    public TrailRenderer Trail { get => trail; }

    // Speed settings
    public float MaxVisualizationSpeed { get => maxVisualizationSpeed; }
    public float SpeedLimit { get => speedLimit; private set => speedLimit = value; }
    public float VisualizationSpeed { get => CurrentRigidbody.velocity.magnitude; }

    // Movement Limits.
    public float LeftZBound { get => leftZBound; }
    public float RightZBound { get => rightZBound; }

    // Variables.
    private UserInputManager CachedInputManager { get; set; }
    private Vector3 MoveModifierVector { get; set; }
    private Vector2 LeftRightMoveVector { get; set; } = Vector2.zero;
    private Coroutine MoveZCoroutineHandler { get; set; }
    private BallParameters Parameters { get; set; }

    private float TrailStartTime { get; set; }
    private float StartSpeed { get; set; }

    // Moving.
    private float TargetMoveFactor { get; set; }
    private float CurrentMoveFactor { get; set; }
    private bool IsGameEnabled { get; set; }
    private bool IsInputEnabled { get; set; } = true;
    private bool IsOnlyForwardMove { get; set; } = false;
    private Vector3 ZYPositionLock { get; set; } = Vector3.zero;
    public float CurrentVisualizationSpeed { get; private set; }
    public float FixedSpeedLimit { get => SpeedLimit + Parameters.SpeedLimitBoost; }

    // DEBUG.
    public bool d_IsGodMode { get; set; }

    // FLAGS.
    public bool CanBarrierCollide { get; private set; } = true;
    public float BarrierCollideDelayS { get => barrierCollideDelayS; }
    public BottomDetector HoleDetector { get => holeDetector; }
    public bool IsAlive { get; private set; } = true;
    public Collider MainCollider { get => mainCollider; }
    public float SpeedUpFactor { get => speedUpFactor; }

    #endregion

    #region FUNCTIONS

    public void Init(Vector3 worldPositon, BallParameters parameters)
    {
        transform.position = worldPositon;
        IsGameEnabled = false;
        Parameters = parameters;

        RefreshParameters();
    }

    public void OnKill()
    {
        if(d_IsGodMode == false)
        {
            IsOnlyForwardMove = false;
            GamePlayManager.Instance.LvlFailed();
            IsAlive = false;
            HapticsManager.Instance.TryVibrate(HapticsManager.Instance.DeathAmplitude);
            //AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.de);
        }
    }

    public void OnEnterHole()
    {
        OnKill();
        CurrentRigidbody.velocity = new Vector3(-2f, 0f, 0f);
        MainCollider.enabled = false;
    }

    public void AddSpeed(float value)
    {
        SetSpeed(Speed + value);
    }

    public void SetSpeed(float value, bool isSilent = false, bool fromBarrier = false)
    {
        float lastSpeed = Speed;
        Speed = Mathf.Clamp(value, Constants.DEFAULT_VALUE, SpeedLimit + Parameters.SpeedLimitBoost);

        CurrentVisualizationSpeed += (Speed - lastSpeed) * SpeedUpFactor;

        if (isSilent == false && value > lastSpeed)
        {
            float maxSpeedProportion = CurrentVisualizationSpeed / MaxVisualizationSpeed;
            VFXManager.Instance.DoSpeedUpVFX(maxSpeedProportion);
        }

        if(fromBarrier == true)
        {
            CanBarrierCollide = false;
            StartCoroutine(_WaitAndUnlockBarrierCollide());
        }

        OnSpeedChanged(Speed);
    }

    public void SetCanMove(bool canMove)
    {
        IsGameEnabled = canMove;
        if(canMove == false)
        {
            CurrentRigidbody.velocity = Vector3.zero;
        }
    }

    public void SetInputEnabled(bool isEnabled)
    {
        IsInputEnabled = isEnabled;
        if(isEnabled == false)
        {
            OnMouseReleaseHandler();
        }
    }

    public void MoveToDestinationZ(Vector3 position)
    {
        if(MoveZCoroutineHandler != null)
        {
            StopCoroutine(MoveZCoroutineHandler);
        }

        MoveZCoroutineHandler = StartCoroutine(_MoveToPositionZ(position));
    }

    private void RefreshParameters()
    {
        SetSpeed(Speed + Parameters.StartSpeedBoost, true, false);
    }

    private void Awake()
    {
        CachedInputManager = FindObjectOfType<UserInputManager>();

        MoveModifierVector = Vector3.left;
        TrailStartTime = Trail.time;
        StartSpeed = Speed;
        CurrentVisualizationSpeed = Speed;
    }

    private void OnEnable()
    {
        CachedInputManager.OnMouseHold += OnMousePressHandler;
        CachedInputManager.OnMouseRelease += OnMouseReleaseHandler;
    }

    private void OnDisable()
    {
        if(CachedInputManager != null)
        {
            CachedInputManager.OnMouseHold -= OnMousePressHandler;
            CachedInputManager.OnMouseRelease -= OnMouseReleaseHandler;
        }
    }

    private void FixedUpdate()
    {
        if(IsGameEnabled && IsAlive)
        {
            RefreshTrail();
            RefreshVelocity();
            HoleDetector.CheckHit(this);
        }
    }

    private void RefreshVelocity()
    {
        Vector3 moveVector = CurrentVisualizationSpeed * MoveModifierVector;
        CurrentRigidbody.velocity = ClampVelocityLimit(new Vector3(moveVector.x, CurrentRigidbody.velocity.y, moveVector.y));

        if (IsOnlyForwardMove)
        {
            transform.position = new Vector3(transform.position.x, ZYPositionLock.y, ZYPositionLock.z);
        }
        else
        {
            TargetMoveFactor = LeftRightMoveVector.x * LeftRightSpeed * Time.fixedDeltaTime;
            CurrentMoveFactor = TargetMoveFactor;

            float targetZPosition = transform.position.z + CurrentMoveFactor;
            targetZPosition = Mathf.Clamp(targetZPosition, LeftZBound, RightZBound);

            transform.position = new Vector3(transform.position.x, transform.position.y, targetZPosition);
        }
    }

    private void RefreshTrail()
    {
        float fixedTrailTime = TrailStartTime * (StartSpeed / CurrentVisualizationSpeed);
        Trail.time = fixedTrailTime;
    }

    private void OnMousePressHandler(Vector2 delta)
    {
        if(IsInputEnabled == true)
        {
            LeftRightMoveVector = delta;
        }
    }

    private void OnMouseReleaseHandler()
    {
        LeftRightMoveVector = Vector2.zero;
    }

    private Vector3 ClampVelocityLimit(Vector3 velocity)
    {
        if (velocity.magnitude > MaxVisualizationSpeed)
        {
            velocity = velocity.normalized * MaxVisualizationSpeed;
        }

        return velocity;
    }

    private IEnumerator _WaitAndUnlockBarrierCollide()
    {
        yield return new WaitForSeconds(BarrierCollideDelayS);
        CanBarrierCollide = true;
    }

    private IEnumerator _MoveToPositionZ(Vector3 target)
    {
        float xDelta = Mathf.Abs(transform.position.x - target.x);
        float zDelta = Mathf.Abs(target.z - transform.position.z);

        float targetTimeS = (xDelta / CurrentRigidbody.velocity.magnitude) - Mathf.Epsilon;
        int direction = (target.z - transform.position.z) < 0 ? -1 : 1;

        // O ile przesunac na S.
        float stepPerS = zDelta / targetTimeS;

        float timeCounterS = Constants.DEFAULT_VALUE;
        while (timeCounterS < targetTimeS)
        {
            float targetZPositionMove = stepPerS * direction * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + targetZPositionMove);
            timeCounterS += Time.deltaTime;

            yield return null;
        }

        // Koncowe wyrownanie.
        transform.position = new Vector3(transform.position.x, transform.position.y, target.z);
        ZYPositionLock = transform.position;
        IsOnlyForwardMove = true;
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
