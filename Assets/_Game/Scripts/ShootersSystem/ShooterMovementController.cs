using UnityEngine;
using DG.Tweening;
using Aezakmi.GridSystem;

namespace Aezakmi.ShootersSystem
{
    public class ShooterMovementController : GridElementMover
    {
        private static float s_moveDuration = .3f;
        private static float s_rotateDuration = .1f;
        private static Ease s_ease = Ease.InOutSine;

        public override void StartMoving(GridNode gridNode)
        {
            Quaternion targetRotation = Quaternion.LookRotation(gridNode.transform.position - transform.position);
            transform
                .DORotateQuaternion(targetRotation, s_rotateDuration)
                .SetEase(s_ease)
                .Play();

            transform
                .DOMove(gridNode.transform.position, s_moveDuration)
                .SetEase(s_ease)
                .OnComplete(delegate { onReachedEnd?.Invoke(); })
                .Play();

            onStartedMoving?.Invoke();
        }
    }
}