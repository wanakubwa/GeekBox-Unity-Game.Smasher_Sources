using UnityEngine;

public class BoltPickup : PickupBase
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
        ball.AddSpeed(Ammount);
        HapticsManager.Instance.TryVibrate(HapticsManager.Instance.BoltAmplitude);
        AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.COLLECT);
    }

    #endregion

    #region Enums



    #endregion
}
