using GeekBox.Scripts.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameSceneCameraManager : ManagerSingletonBase<GameSceneCameraManager>
{
    #region Fields


    #endregion

    #region Propeties

    private Camera RoadCamera { get; set; }
    private Camera JumpViewCamera { get; set; }
    private Camera TerrainCamera { get; set; }

    #endregion

    #region Methods

    public override void LoadContent()
    {
        base.LoadContent();

        // To jest bardzo zle.
        GameCameraController controller = FindObjectOfType<GameCameraController>();
        controller.RefreshData();
    }

    public void RegisterCamera(Camera cam, CameraType type)
    {
        switch (type)
        {
            case CameraType.ROAD:
                RoadCamera = cam;
                break;
            case CameraType.JUMP_VIEW:
                JumpViewCamera = cam;
                break;
            case CameraType.TERRAIN:
                TerrainCamera = cam;
                break;
            default:
                break;
        }
    }

    public void SwitchCamera(CameraType type)
    {

    }

    private void Start()
    {
        SwitchCamera(CameraType.ROAD);
    }

    #endregion

    #region Enums

    public enum CameraType
    {
        ROAD,
        JUMP_VIEW,
        TERRAIN
    }

    #endregion
}
