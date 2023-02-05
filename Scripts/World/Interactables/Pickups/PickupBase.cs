using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class PickupBase : PlayerTrigger
{

    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected abstract void DoAction(PlayerBall ball);

    protected override sealed void OnPlayerTriggerEnter(PlayerBall ball)
    {
        base.OnPlayerTriggerEnter(ball);

        DoAction(ball);

        Destroy(gameObject);
    }

    #endregion

    #region Enums



    #endregion
}
