using UnityEngine;
using System.Collections;

public class EndRoadTrigger : PlayerTrigger
{
    [SerializeField]
    private Transform destinationPoint;

    [SerializeField]
    private Transform confettiSpawnPosition;

    protected override void OnPlayerTriggerEnter(PlayerBall ball)
    {
        base.OnPlayerTriggerEnter(ball);

        ball.SetInputEnabled(false);
        ball.MoveToDestinationZ(destinationPoint.position);

        VFXManager.Instance.DoEndCameraVFX(destinationPoint.position);
    }
}
