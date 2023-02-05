using UnityEngine;
using System.Collections;

public class MonetizationManager : ManagerSingletonBase<MonetizationManager>
{
    #region Fields

    [SerializeField]
    private int interstitialPeriod = 2;

    #endregion

    #region Propeties

    public int InterstitialPeriod { 
        get => interstitialPeriod; 
    }

    private int LvlTryCounter { get; set; } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public override void AttachEvents()
    {
        base.AttachEvents();

        GamePlayManager.Instance.OnLvlSuccess += OnLvlSuccessHandler;
        GamePlayManager.Instance.OnLvlFailed += OnLvlFailedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.OnLvlSuccess -= OnLvlSuccessHandler;
            GamePlayManager.Instance.OnLvlFailed -= OnLvlFailedHandler;
        }
    }

    private void TryShowInterstitialAd()
    {
        LvlTryCounter++;
        if(LvlTryCounter > InterstitialPeriod)
        {
            GeekBox.Ads.EasyMobileManager.Instance.ShowInterstitialAD();
            LvlTryCounter = Constants.DEFAULT_VALUE;
        }
    }

    // Handlers.
    private void OnLvlFailedHandler()
    {
        TryShowInterstitialAd();
    }

    private void OnLvlSuccessHandler(int obj)
    {
        TryShowInterstitialAd();
    }


    #endregion

    #region Enums



    #endregion
}
