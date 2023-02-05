using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : ManagerSingletonBase<GamePlayManager>
{
    #region Fields



    #endregion

    #region Propeties

    public event Action OnLvlUpdate = delegate { };
    public event Action OnGameStart = delegate { };
    public event Action OnGameStop = delegate { };

    public event Action<int> OnLvlSuccess = delegate { };
    public event Action OnLvlFailed = delegate { };

    #endregion

    #region Methods

    public void ResetLvl()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if(manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.RestartLvl();
            }
        }

        OnLvlUpdate();
    }

    public void LoadNextLvl()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.LoadNextLvl();
            }
        }

        OnLvlUpdate();
    }

    public void LvlSuccess(int wallsCounter)
    {
        StopLvlGame();

        OnLvlSuccess(wallsCounter);
        VFXManager.Instance.DoSuccessCameraVFX();
        PopUpManager.Instance.ShowWinUIPopUp(wallsCounter);
    }

    public void LvlFailed()
    {
        StopLvlGame();
        
        PopUpManager.Instance.ShowStartUIPopUp();
        OnLvlFailed();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        StopLvlGame();
        PopUpManager.Instance.ShowStartUIPopUp();
    }

    public void StopLvlGame()
    {
        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.StopLvlGame();
            }
        }

        OnGameStop();
    }

    public void StartLvlGame()
    {
        ResetLvl();

        ICollection<IManager> managers = GameManager.Instance.Managers;
        foreach (IManager manager in managers)
        {
            if (manager is IGameEvents gameEventsManager)
            {
                gameEventsManager.StartLvlGame();
            }
        }

        OnGameStart();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GameplayEvents.Instance.OnBallStopOnWall += OnBallStopOnWallHandler;
        SaveLoadManager.Instance.OnResetCompleted += ResetLvl;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if(GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnBallStopOnWall -= OnBallStopOnWallHandler;
        }

        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= ResetLvl;
        }
    }

    private void OnBallStopOnWallHandler(int wallsCounter)
    {
        if(wallsCounter > 0)
        {
            LvlSuccess(wallsCounter);
        }
        else
        {
            LvlFailed();
        }
    }

    #endregion

    #region Enums



    #endregion
}
