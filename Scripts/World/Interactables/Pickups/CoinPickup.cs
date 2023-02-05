using UnityEngine;

public class CoinPickup : PickupBase
{
    #region Fields

    [SerializeField]
    private int ammount = 1;

    #endregion

    #region Propeties

    public int Ammount { get => ammount; }

    #endregion

    #region Methods

    protected override void DoAction(PlayerBall ball)
    {
        PlayerManager.Instance.Wallet.AddCoins(Ammount);
        HapticsManager.Instance.TryVibrate(HapticsManager.Instance.CoinAmplitude);
        AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.COLLECT);
    }

    #endregion

    #region Enums



    #endregion
}
