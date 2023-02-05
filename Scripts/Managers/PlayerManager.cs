using PlayerData;
using System;
using UnityEngine;

public class PlayerManager : SingletonSaveableManager<PlayerManager, PlayerManagerMemento>, IGameEvents
{
    #region Fields

    [SerializeField]
    private PlayerBall playerPrefab;
    [SerializeField]
    private int rewardForWallBreak = 50;
    [SerializeField]
    private int scoreForWallBreak = 100;

    private PlayerWallet wallet = new PlayerWallet();

    #endregion

    #region Propeties

    public event Action<PlayerBall> OnPlayerModelChanged = delegate { };

    public PlayerBall PlayerPrefab { 
        get => playerPrefab;
    }

    public PlayerWallet Wallet { 
        get => wallet; 
        private set => wallet = value;
    }

    private int RewardForWallBreak { get => rewardForWallBreak; }
    public int ScoreForWallBreak { get => scoreForWallBreak; }

    //Variables.
    public PlayerBall CurrentPlayer { get; private set; }
    private RoadManager CachedRoadManager { get; set; }

    #endregion

    #region Methods

    public void LoadNextLvl()
    {
        Wallet.SetCurrentLvlNo(Wallet.CurrentLvlNo + 1);
        SpawnPlayer();
    }

    public void RestartLvl()
    {
        SpawnPlayer();
    }

    public void StartLvlGame()
    {
        CurrentPlayer.SetCanMove(true);
    }

    public void StopLvlGame()
    {
        CurrentPlayer.SetCanMove(false);
    }

    public override void Initialize()
    {
        base.Initialize();
        CachedRoadManager = RoadManager.Instance;   
    }

    public override void LoadContent()
    {
        base.LoadContent();
        SpawnPlayer();
    }

    public override void LoadManager(PlayerManagerMemento memento)
    {
        Wallet = new PlayerWallet(memento.SavedWallet);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        GamePlayManager.Instance.OnLvlSuccess += OnLvlSuccessHandler;
    }

    public override void ResetGameData()
    {
        base.ResetGameData();

        Wallet = new PlayerWallet();
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GamePlayManager.Instance.OnLvlSuccess -= OnLvlSuccessHandler;
    }

    private void SpawnPlayer()
    {
        if(CurrentPlayer != null)
        {
            Destroy(CurrentPlayer.gameObject);
        }

        CurrentPlayer = Instantiate(PlayerPrefab);
        CurrentPlayer.Init(CachedRoadManager.CurrentRoad.SpawnPosition.position, Wallet.GetBallParameters());
        SubscribeBallEvents();

        OnPlayerModelChanged(CurrentPlayer);
    }

    // HANDLERS.
    private void OnLvlSuccessHandler(int wallsBreaked)
    {
        int coinsRaw = wallsBreaked * RewardForWallBreak;
        int score = wallsBreaked * ScoreForWallBreak;
        LvlData data = new LvlData(Constants.DEFAULT_ID, wallsBreaked, coinsRaw, score);

        Wallet.AddLvlData(data);
    }

    private void SubscribeBallEvents()
    {
        CurrentPlayer.OnSpeedChanged += OnPlayerSpeedChanged;
    }

    private void OnPlayerSpeedChanged(float speed)
    {
        if (speed <= Constants.DEFAULT_VALUE)
        {
            GamePlayManager.Instance.LvlFailed();
        }
    }

    #endregion

    #region Enums



    #endregion
}
