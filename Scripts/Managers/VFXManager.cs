using UnityEngine;
using System.Collections;

public class VFXManager : ManagerSingletonBase<VFXManager>, IGameEvents
{
    #region Fields

    [SerializeField]
    private GameObject confettiPrefabVFX;

    [SerializeField]
    private AnimationCurve speedVfxDurationCurve;

    #endregion

    #region Propeties

    public GameObject ConfettiPrefabVFX { get => confettiPrefabVFX; }

    private GameCameraController GameCamera { get; set; }
    private PlayerManager CachedPlayer { get; set; }
    public AnimationCurve SpeedVfxDurationCurve { get => speedVfxDurationCurve; }

    #endregion

    #region Methods

    public void DoSpeedUpVFX(float visualizationSpeedProportion)
    {
        float fixedProportion = Mathf.Clamp01(visualizationSpeedProportion);
        GameCamera.DoSpeedEffect(SpeedVfxDurationCurve.Evaluate(fixedProportion));
    }

    public void DoEndCameraVFX(Vector3 endPosition)
    {
        float distanceXDelta = Mathf.Abs(endPosition.x - CachedPlayer.CurrentPlayer.transform.position.x);
        float reachDestinationTimeS = distanceXDelta / CachedPlayer.CurrentPlayer.VisualizationSpeed;

        GameCamera.DoEndAnimation(reachDestinationTimeS);
    }

    public void DoBarrierDestroyVFX(GameObject prefab, Vector3 position)
    {
        GameObject particles = Instantiate(prefab);
        particles.transform.ResetParent(transform);
        particles.transform.position = position;
        particles.SetActive(true);
    }

    public void DoSuccessCameraVFX()
    {
        GameCamera.SuccessVfx.SetActive(true);
    }

    public void DoConfettiVFX()
    {
        GameCamera.ConfettiVfx.SetActive(true);
    }

    public void LoadNextLvl()
    {
        GameCamera.ConfettiVfx.SetActive(false);
        GameCamera.SuccessVfx.SetActive(false);
    }

    public void RestartLvl()
    {
        GameCamera.ConfettiVfx.SetActive(false);
        GameCamera.SuccessVfx.SetActive(false);
    }

    public void StopLvlGame()
    {

    }

    public void StartLvlGame()
    {

    }

    public override void Initialize()
    {
        base.Initialize();

        CachedPlayer = PlayerManager.Instance;
    }

    public override void LoadContent()
    {
        base.LoadContent();

        GameCamera = FindObjectOfType<GameCameraController>();
    }

    #endregion

    #region Enums



    #endregion
}
