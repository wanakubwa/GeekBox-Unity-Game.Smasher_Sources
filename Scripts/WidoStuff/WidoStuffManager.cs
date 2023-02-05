//using com.adjust.sdk;
using System;
using System.Threading.Tasks;
using UnityEngine;
//using Firebase.Extensions;
using Sirenix.OdinInspector;
//using Facebook.Unity;

public class WidoStuffManager : ManagerSingletonBase<WidoStuffManager>
{

    //private Firebase.FirebaseApp FirebaseApp { get; set; }
    //System.Collections.Generic.Dictionary<string, object> defaults = new System.Collections.Generic.Dictionary<string, object>();
    //private bool IsFirebaseInit { get; set; } = false;

    //// IronSource
    //private Action OnSuccessADRewarded { get; set; } = delegate{};
    //protected override void Start()
    //{
    //    base.Start();

    //    // Firebase init.
    //    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    //    {
    //        var dependencyStatus = task.Result;
    //        if (dependencyStatus == Firebase.DependencyStatus.Available)
    //        {
    //            // Create and hold a reference to your FirebaseApp,
    //            // where app is a Firebase.FirebaseApp property of your application class.
    //            FirebaseApp = Firebase.FirebaseApp.DefaultInstance;
    //            IsFirebaseInit = true;

    //            // Messaging.
    //            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
    //            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

    //            // Zdalna konfiguracja.
    //            defaults.Add("show_interstitial_ads_interval", 2);
    //            FetchDataAsync();
    //        }
    //        else
    //        {
    //            UnityEngine.Debug.LogError(System.String.Format(
    //              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    //            // Firebase Unity SDK is not safe to use here.
    //        }
    //    });

      

    //    // Ironsource.
    //    IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    //    IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
    //    IronSource.Agent.init("13de04489");

    //    //test.
    //    //SdkInitializationCompletedEvent();

    //    //Facebook
    //    if (!FB.IsInitialized)
    //    {
    //        // Initialize the Facebook SDK
    //        FB.Init(InitCallback, OnHideUnity);
    //    }
    //    else
    //    {
    //        // Already initialized, signal an app activation App Event
    //        FB.ActivateApp();
    //    }
    //}

    //private void InitCallback()
    //{
    //    if (FB.IsInitialized)
    //    {
    //        // Signal an app activation App Event
    //        FB.ActivateApp();
    //        // Continue with Facebook SDK
    //        // ...
    //    }
    //    else
    //    {
    //        Debug.Log("Failed to Initialize the Facebook SDK");
    //    }
    //}

    //private void OnHideUnity(bool isGameShown)
    //{
    //    if (!isGameShown)
    //    {
    //        // Pause the game - we will need to hide
    //        Time.timeScale = 0;
    //    }
    //    else
    //    {
    //        // Resume the game - we're getting focus again
    //        Time.timeScale = 1;
    //    }
    //}

    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    //}

    //// Start a fetch request.
    //// FetchAsync only fetches new data if the current data is older than the provided
    //// timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    //// By default the timespan is 12 hours, and for production apps, this is a good
    //// number. For this example though, it's set to a timespan of zero, so that
    //// changes in the console will always show up immediately.
    //public void FetchDataAsync()
    //{
    //    var remoteConfig = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance;
    //    remoteConfig.SetDefaultsAsync(defaults)
    //      .ContinueWith(_ => {
    //          remoteConfig.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread((task) => {

    //              if (task.IsCompleted)
    //                  Debug.Log("Fetch firebase success");
    //              else
    //                  Debug.Log("Fetch firebase failed");
    //          });
    //      });
    //}

    //public void ShowInterstitialAd()
    //{
    //    //todo;
    //    OnShowInterstitialAD();

    //    if(IronSource.Agent.isInterstitialReady() == true)
    //    {
    //        IronSource.Agent.showInterstitial();
    //    }
    //}

    //public void ShowRewardedAd(Action onSuccess, Action onFailed = null)
    //{
    //    OnShowRewardedAD();

    //    OnSuccessADRewarded = onSuccess;
    //    if(onFailed == null)
    //    {
    //        onFailed = delegate { };
    //    }

    //    if (IronSource.Agent.isRewardedVideoAvailable() == true)
    //    {
    //        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
    //        IronSource.Agent.showRewardedVideo();
    //    }
    //    else
    //    {
    //        onFailed();
    //    }
    //}

    //void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    //{
    //    IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
    //    OnSuccessADRewarded();
    //}

    //public override void AttachEvents()
    //{
    //    base.AttachEvents();

    //    GamePlayManager.Instance.OnGameStart += OnLvlStart;
    //    GamePlayManager.Instance.OnLvlFailed += OnLvlFailed;
    //    GamePlayManager.Instance.OnLvlSuccess += OnLvlPassed;
    //}

    //protected override void DetachEvents()
    //{
    //    base.DetachEvents();

    //    GamePlayManager.Instance.OnGameStart -= OnLvlStart;
    //    GamePlayManager.Instance.OnLvlFailed -= OnLvlFailed;
    //    GamePlayManager.Instance.OnLvlSuccess -= OnLvlPassed;
    //}

    //public void OnLvlStart()
    //{
    //    // Log an event with a string parameter.
    //    Firebase.Analytics.FirebaseAnalytics
    //      .LogEvent(
    //        Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
    //        Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
    //        "level_start"
    //      );

    //    AdjustEvent adjustEvent = new AdjustEvent("level_start");
    //    Adjust.trackEvent(adjustEvent);
    //}

    //public void OnLvlPassed(int a)
    //{
    //    // Log an event with a string parameter.
    //    Firebase.Analytics.FirebaseAnalytics
    //      .LogEvent(
    //        Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
    //        Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
    //        "level_passed"
    //      );

    //    AdjustEvent adjustEvent = new AdjustEvent("level_passed");
    //    Adjust.trackEvent(adjustEvent);
    //}

    //public void OnLvlFailed()
    //{
    //    // Log an event with a string parameter.
    //    Firebase.Analytics.FirebaseAnalytics
    //      .LogEvent(
    //        Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
    //        Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
    //        "level_failed"
    //      );

    //    AdjustEvent adjustEvent = new AdjustEvent("level_failed");
    //    Adjust.trackEvent(adjustEvent);
    //}

    //public void OnShowInterstitialAD()
    //{
    //    // Log an event with a string parameter.
    //    Firebase.Analytics.FirebaseAnalytics
    //      .LogEvent(
    //        Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
    //        Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
    //        "show_interstitial_ads"
    //      );
    //}

    //public void OnShowRewardedAD()
    //{
    //    // Log an event with a string parameter.
    //    Firebase.Analytics.FirebaseAnalytics
    //      .LogEvent(
    //        Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
    //        Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
    //        "show_rewarded_ads"
    //      );

    //    AdjustEvent adjustEvent = new AdjustEvent("abc123");
    //    Adjust.trackEvent(adjustEvent);
    //}

    //public int GetInterstitialADPeriod()
    //{
    //    return (int)(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("show_interstitial_ads_interval").DoubleValue);
    //}

    //void OnApplicationPause(bool isPaused)
    //{
    //    IronSource.Agent.onApplicationPause(isPaused);
    //}

    //private void SdkInitializationCompletedEvent()
    //{
    //    IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
    //    IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
    //    IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
    //    IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
    //    IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
    //    IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
    //    IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

    //    //IronSource.Agent.validateIntegration();
    //    IronSource.Agent.loadInterstitial();
    //}

    //private void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    //{
    //    double rev = impressionData.revenue == null ? 0 : Convert.ToDouble(impressionData.revenue);

    //    AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceIronSource);
    //    adjustAdRevenue.setRevenue(rev, "USD");
    //    // optional fields
    //    adjustAdRevenue.setAdRevenueNetwork(impressionData.adNetwork);
    //    adjustAdRevenue.setAdRevenueUnit(impressionData.adUnit);
    //    adjustAdRevenue.setAdRevenuePlacement(impressionData.placement);
    //    // track Adjust ad revenue
    //    Adjust.trackAdRevenue(adjustAdRevenue);
    //}

    //// Invoked when the interstitial ad closed and the user goes back to the application screen.
    //void InterstitialAdClosedEvent()
    //{
    //    IronSource.Agent.loadInterstitial();
    //}

    ///************* Interstitial Delegates *************/
    //void InterstitialAdReadyEvent()
    //{
    //    Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    //}

    //void InterstitialAdLoadFailedEvent(IronSourceError error)
    //{
    //    Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    //}

    //void InterstitialAdShowSucceededEvent()
    //{
    //    Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
    //}

    //void InterstitialAdShowFailedEvent(IronSourceError error)
    //{
    //    Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    //}

    //void InterstitialAdClickedEvent()
    //{
    //    Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    //}

    //void InterstitialAdOpenedEvent()
    //{
    //    Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    //}
}