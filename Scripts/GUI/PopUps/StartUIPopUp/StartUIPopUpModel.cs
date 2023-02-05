using GeekBox.Utils;
using PlayerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StartUIPopUpModel : PopUpModel
{
    #region Fields

    [SerializeField]
    private int speedLimitPriceMod;
    [SerializeField]
    private int speedLimitMod;

    [Space]
    [SerializeField]
    private int startSpeedPriceMod;
    [SerializeField]
    private int startSpeedMod;

    #endregion

    #region Propeties

    public PlayerWallet CachedWallet { get => PlayerManager.Instance.Wallet; }

    private bool WasSpeedLimitAD { get; set; } = false;
    private bool WasStartSpeedAD { get; set; } = false;

    #endregion

    #region Methods

    public void StartGame()
    {
        GamePlayManager.Instance.StartLvlGame();
    }

    public int GetCurrentLvlNo()
    {
        return CachedWallet.CurrentLvlNo;
    }

    public int GetNextSpeedLimitPrice()
    {
        return (CachedWallet.SpeedLimitBoost / speedLimitMod) * speedLimitPriceMod + speedLimitPriceMod;
    }

    public int GetNextStartSpeedPrice()
    {
        return (CachedWallet.StartSpeedBoost / startSpeedMod) * startSpeedPriceMod + startSpeedPriceMod;
    }

    public int GetNextSpeedLimitBoost()
    {
        return CachedWallet.SpeedLimitBoost + speedLimitMod;
    }

    public int GetNextStartSpeedBoost()
    {
        return CachedWallet.StartSpeedBoost + startSpeedMod;
    }

    public bool CanPlayerBuySpeedLimitBoost()
    {
        return CachedWallet.CanAfford(GetNextSpeedLimitPrice());
    }

    public bool CanPlayerBuyStartSpeedBoost()
    {
        return CachedWallet.CanAfford(GetNextStartSpeedPrice());
    }

    public void TryBuySpeedLimitBoost()
    {
        if(CanPlayerBuySpeedLimitBoost() == true)
        {
            int price = GetNextSpeedLimitPrice();
            CachedWallet.AddSpeedLimitBoost(speedLimitMod);
            CachedWallet.AddCoins(-price);
        }
        else if(WasSpeedLimitAD == false)
        {
            GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() =>
            {
                WasSpeedLimitAD = true;
                CachedWallet.AddSpeedLimitBoost(speedLimitMod);
            });
        }
    }

    public void TryBuyStartSpeedBoost()
    {
        if (CanPlayerBuyStartSpeedBoost() == true)
        {
            int price = GetNextStartSpeedPrice();
            CachedWallet.AddStartSpeedBoost(startSpeedMod);
            CachedWallet.AddCoins(-price);
        }
        else if(WasStartSpeedAD == false)
        {
            GeekBox.Ads.EasyMobileManager.Instance.ShowRewardedAD(() =>
            {
                WasStartSpeedAD = true;
                CachedWallet.AddStartSpeedBoost(startSpeedMod);
            });
        }
    }

    public void ShowSkinsShop()
    {
        AndroidUtils.ShowToast("Coming soon!");
    }

    public void ShowSettingsPopUp()
    {
        PopUpManager.Instance.ShowSettingsPopUp();
    }

    public bool IsVibrationsEnabled()
    {
        return HapticsManager.Instance.IsVibrationEnabled;
    }

    public bool IsAudioEnabled()
    {
        return !AudioManager.Instance.IsAudioMute;
    }

    public void ToggleVibrationsState()
    {
        HapticsManager.Instance.ToggleVibrationState();
    }

    public void ToggleAudioState()
    {
        AudioManager.Instance.ToggleIsAudioMute();
    }

    #endregion

    #region Enums



    #endregion
}
