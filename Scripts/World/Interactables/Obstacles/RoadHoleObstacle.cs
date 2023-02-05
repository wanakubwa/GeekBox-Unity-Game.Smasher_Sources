using UnityEngine;
using System.Collections;

public class RoadHoleObstacle : MonoBehaviour, IPlayerInteractable
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void Interact(PlayerBall source)
    {
        source.OnEnterHole();
    }

    #endregion

    #region Enums



    #endregion
}
