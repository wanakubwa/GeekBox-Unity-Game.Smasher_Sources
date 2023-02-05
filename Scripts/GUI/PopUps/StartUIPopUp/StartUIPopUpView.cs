using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUIPopUpView : PopUpView
{
    #region Fields

    private const string BOOST_FORMAT = "+{0}";
    private const string PRICE_FORMAT = "PRICE: {0}";

    [SerializeField]
    private TextMeshProUGUI playerCoinsText;

    [SerializeField]
    private GameObject speedLimitNormal;
    [SerializeField]
    private GameObject speedLimitAD;
    [SerializeField]
    private TextMeshProUGUI speedLimitPrice;
    [SerializeField]
    private TextMeshProUGUI nextSpeedLimitValue;

    [SerializeField]
    private GameObject startSpeedNormal;
    [SerializeField]
    private GameObject startSpeedAD;
    [SerializeField]
    private TextMeshProUGUI startSpeedPrice;
    [SerializeField]
    private TextMeshProUGUI nextStartSpeedValue;

    [Header("Vibration Button")]
    [SerializeField]
    private Button vibrationButton;
    [SerializeField]
    private Sprite vibrationOnTex;
    [SerializeField]
    private Sprite vibrationOffTex;

    [Header("Audio Button")]
    [SerializeField]
    private Button audioButton;
    [SerializeField]
    private Sprite audioOnTex;
    [SerializeField]
    private Sprite audioOffTex;

    #endregion

    #region Propeties

    private StartUIPopUpModel CurrentModel { get; set; }

    public GameObject SpeedLimitNormal { get => speedLimitNormal; }
    public GameObject SpeedLimitAD { get => speedLimitAD; }
    public GameObject StartSpeedNormal { get => startSpeedNormal; }
    public GameObject StartSpeedAD { get => startSpeedAD; }
    public TextMeshProUGUI SpeedLimitPrice { get => speedLimitPrice; }
    public TextMeshProUGUI StartSpeedPrice { get => startSpeedPrice; }
    public TextMeshProUGUI PlayerCoinsText { get => playerCoinsText; }
    public TextMeshProUGUI NextSpeedLimitValue { get => nextSpeedLimitValue; }
    public TextMeshProUGUI NextStartSpeedValue { get => nextStartSpeedValue; }
    public Button VibrationButton { get => vibrationButton; }
    public Sprite VibrationOnTex { get => vibrationOnTex; }
    public Sprite VibrationOffTex { get => vibrationOffTex; }
    public Button AudioButton { get => audioButton; }
    public Sprite AudioOnTex { get => audioOnTex; }
    public Sprite AudioOffTex { get => audioOffTex; }

    #endregion

    #region Methods

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<StartUIPopUpModel>();

        RefreshView();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        PlayerManager.Instance.Wallet.OnCoinsChange += OnCoinsChanedHandler;
        HapticsManager.Instance.OnVibrationStateChange += RefreshVibrationButton;
        AudioManager.Instance.OnVolumeChanged += OnVolumeChangedHandler;
        SaveLoadManager.Instance.OnResetCompleted += RefreshView;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Wallet.OnCoinsChange -= OnCoinsChanedHandler;
        }

        if(HapticsManager.Instance != null)
        {
            HapticsManager.Instance.OnVibrationStateChange -= RefreshVibrationButton;
        }

        if(SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.OnResetCompleted -= RefreshView;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.OnVolumeChanged -= OnVolumeChangedHandler;
        }
    }

    private void RefreshView()
    {
        RefreshSpeedLimitButtons();
        RefreshStartSpeedButtons();
        RefreshCoins(CurrentModel.CachedWallet.Coins);
        RefreshVibrationButton(CurrentModel.IsVibrationsEnabled());
        RefreshAudioButton(CurrentModel.IsAudioEnabled());
    }

    private void RefreshVibrationButton(bool isOn)
    {
        VibrationButton.image.sprite = isOn == true ? VibrationOnTex : VibrationOffTex;
    }

    private void RefreshAudioButton(bool isOn)
    {
        AudioButton.image.sprite = isOn == true ? AudioOnTex : AudioOffTex;
    }

    private void RefreshCoins(int ammount)
    {
        PlayerCoinsText.SetText(ammount.ToString());
    }

    private void RefreshSpeedLimitButtons()
    {
        bool canPlayerBuy = CurrentModel.CanPlayerBuySpeedLimitBoost();
        SpeedLimitNormal.SetActive(canPlayerBuy);
        SpeedLimitAD.SetActive(!canPlayerBuy);

        SpeedLimitPrice.SetText(string.Format(PRICE_FORMAT, CurrentModel.GetNextSpeedLimitPrice()));
        NextSpeedLimitValue.SetText(string.Format(BOOST_FORMAT, CurrentModel.GetNextSpeedLimitBoost()));
    }

    private void RefreshStartSpeedButtons()
    {
        bool canPlayerBuy = CurrentModel.CanPlayerBuyStartSpeedBoost();
        StartSpeedNormal.SetActive(canPlayerBuy);
        StartSpeedAD.SetActive(!canPlayerBuy);

        StartSpeedPrice.SetText(string.Format(PRICE_FORMAT, CurrentModel.GetNextStartSpeedPrice()));
        NextStartSpeedValue.SetText(string.Format(BOOST_FORMAT, CurrentModel.GetNextStartSpeedBoost()));
    }

    private void OnCoinsChanedHandler(int ammount)
    {
        RefreshCoins(ammount);
        RefreshSpeedLimitButtons();
        RefreshStartSpeedButtons();
    }

    private void OnVolumeChangedHandler(float obj)
    {
        RefreshAudioButton(CurrentModel.IsAudioEnabled());
    }

    #endregion

    #region Enums



    #endregion
}