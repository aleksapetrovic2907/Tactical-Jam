using UnityEngine;

namespace Aezakmi.ShootersSystem
{
    public class ShooterAnimationController : MonoBehaviour
    {
        private Animator m_animator;
        private ShooterMovementController m_shooterMovementController;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_shooterMovementController = GetComponent<ShooterMovementController>();
            m_shooterMovementController.onStartedMoving += StartMoveAnimation;
            m_shooterMovementController.onReachedEnd += StartIdleAnimation;
        }

        private void StartIdleAnimation() => m_animator.SetBool("Moving", false);
        private void StartMoveAnimation() => m_animator.SetBool("Moving", true);
        public void StartShootAnimation() => m_animator.SetTrigger("Shoot");
        public void StartDeathAnimation() => m_animator.SetTrigger("Death");
    }
}