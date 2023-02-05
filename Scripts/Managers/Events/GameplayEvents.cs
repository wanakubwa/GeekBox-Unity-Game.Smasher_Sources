using System;

public class GameplayEvents : ManagerSingletonBase<GameplayEvents>
{
    #region Fields

    #endregion

    #region Propeties

    public event Action<int> OnBallStopOnWall = delegate { };
    public event Action OnBallBreakWall = delegate { };

    #endregion

    #region Methods

    public void NotifyBallStopOnWall(int breakedWalls)
    {
        OnBallStopOnWall(breakedWalls);
    }

    public void NotifyBallBreakWall()
    {
        OnBallBreakWall();
    }

    #endregion

    #region Enums



    #endregion
}
