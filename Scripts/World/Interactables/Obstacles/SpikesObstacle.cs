using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SpikesObstacle : PlayerTrigger
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected override void OnPlayerTriggerEnter(PlayerBall ball)
    {
        base.OnPlayerTriggerEnter(ball);
        ball.OnKill();
    }

    #endregion

    #region Enums



    #endregion
}
