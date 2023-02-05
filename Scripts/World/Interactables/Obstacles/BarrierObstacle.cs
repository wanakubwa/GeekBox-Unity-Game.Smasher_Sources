using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BarrierObstacle : PlayerTrigger
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI valueText;

    [Header("SETTINGS")]
    [SerializeField]
    private Operation typeOfOperation = Operation.ADD;
    [SerializeField]
    private float value;

    [Header("VFX")]
    [SerializeField]
    private Transform particlesSpawnPosition;
    [SerializeField]
    private GameObject destroyParticle;

    #endregion

    #region Propeties

    public TextMeshProUGUI ValueText { get => valueText; }
    public Operation TypeOfOperation { get => typeOfOperation; }
    public float Value { get => value; }
    public GameObject DestroyParticle { get => destroyParticle; }
    public Transform ParticlesSpawnPosition { get => particlesSpawnPosition; }

    #endregion

    #region Methods

    protected override void OnPlayerTriggerEnter(PlayerBall ball)
    {
        base.OnPlayerTriggerEnter(ball);

        if(ball.CanBarrierCollide == true)
        {
            ApplyOperation(ball);
            HapticsManager.Instance.TryVibrate(HapticsManager.Instance.GateAmplitude);
            AudioManager.Instance.PlayAudioSoundByLabel(AudioContainerSettings.AudioLabel.BARRIER);
        }
    }

    private void Awake()
    {
        RefreshText();
    }

    private void ApplyOperation(PlayerBall ball)
    {
        switch (TypeOfOperation)
        {
            case Operation.ADD:
                SetSpeedToTarget(ball, ball.Speed + Value);

                break;
            case Operation.SUBSTRACT:
                SetSpeedToTarget(ball, Mathf.Clamp(ball.Speed - Value, 0, float.PositiveInfinity));

                break;
            case Operation.MULTIPLE:
                SetSpeedToTarget(ball, ball.Speed * Value);

                break;
            case Operation.DIVIDE:
                SetSpeedToTarget(ball, ball.Speed / Value);

                break;
            default:
                break;
        }

        DestroyObject();
    }

    private void SetSpeedToTarget(PlayerBall ball, float value)
    {
        ball.SetSpeed(value, fromBarrier: true);
    }

    private void RefreshText()
    {
        string operatorText = string.Empty;
        switch (TypeOfOperation)
        {
            case Operation.ADD:
                operatorText = "+";

                break;
            case Operation.SUBSTRACT:
                operatorText = "-";

                break;
            case Operation.MULTIPLE:
                operatorText = "x";

                break;
            case Operation.DIVIDE:
                operatorText = "\u00F7";

                break;
            default:
                break;
        }

        string valueText = Value.ToString().Replace(",", ".");
        ValueText.SetText(operatorText + valueText);
    }

    private void DestroyObject()
    {
        VFXManager.Instance.DoBarrierDestroyVFX(DestroyParticle, ParticlesSpawnPosition.transform.position);
        Destroy(gameObject);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        RefreshText();
    }

#endif

    #endregion

    #region Enums

    public enum Operation
    {
        ADD,
        SUBSTRACT,
        MULTIPLE,
        DIVIDE
    }

    #endregion
}
