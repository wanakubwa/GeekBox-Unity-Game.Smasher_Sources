using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected virtual void OnPlayerTriggerEnter(PlayerBall ball)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBall ball = other.gameObject.GetComponent<PlayerBall>();
        if (ball != null)
        {
            OnPlayerTriggerEnter(ball);
        }
    }

    #endregion

    #region Enums



    #endregion
}
