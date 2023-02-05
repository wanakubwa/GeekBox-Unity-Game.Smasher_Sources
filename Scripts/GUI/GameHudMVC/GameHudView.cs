using UnityEngine;
using TMPro;

public class GameHudView : UIView
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    private TextMeshProUGUI speedLimitText;
    [SerializeField]
    private TextMeshProUGUI coinsText;

    #endregion

    #region Propeties

    public TextMeshProUGUI SpeedText { get => speedText; }
    public TextMeshProUGUI CoinsText { get => coinsText; }
    public TextMeshProUGUI SpeedLimitText { get => speedLimitText; }

    #endregion

    #region Methods

    public void GameStart()
    {
        PlayerBall ball = PlayerManager.Instance.CurrentPlayer;
        AttachEventsToPlayer(ball);
        SpeedChangedHandler(ball.Speed);
        SpeedLimitChange(ball.FixedSpeedLimit);
        CoinsText.text = PlayerManager.Instance.Wallet.Coins.ToString();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnCoinsChange += OnCoinsChangedHandler;
        }
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnCoinsChange -= OnCoinsChangedHandler;
        }   
    }

    private void AttachEventsToPlayer(PlayerBall ball)
    {
        ball.OnSpeedChanged += SpeedChangedHandler;
    }

    private void SpeedChangedHandler(float value)
    {
        SpeedText.SetText(((int)value).ToString());
    }

    private void SpeedLimitChange(float value)
    {
        SpeedLimitText.SetText(((int)value).ToString());
    }

    private void OnCoinsChangedHandler(int obj)
    {
        CoinsText.text = obj.ToString();
    }

    #endregion

    #region Enums



    #endregion
}
