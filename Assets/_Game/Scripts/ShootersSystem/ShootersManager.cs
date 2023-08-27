using UnityEngine;
using Aezakmi.SelectionSystem;

namespace Aezakmi.ShootersSystem
{
    public class ShootersManager : MonoBehaviour
    {
        public Shooter CurrentShooter = null;
        public ShooterAnimationController CurrentShooterAnimationController = null;

        private void OnEnable()
        {
            SelectionsManager.Instance.OnSelection += SetShooter;
            SelectionsManager.Instance.OnReselection += Shoot;
        }

        private void OnDisable()
        {
            SelectionsManager.Instance.OnSelection -= SetShooter;
            SelectionsManager.Instance.OnReselection -= Shoot;
        }

        private void SetShooter(Selection selection)
        {
            CurrentShooter = selection.transform.GetComponent<Shooter>();
            CurrentShooterAnimationController = selection.transform.GetComponent<ShooterAnimationController>();
        }

        private void Shoot(Selection selection)
        {
            if (CurrentShooter == null) return;
            CurrentShooterAnimationController.StartShootAnimation();
        }
    }
}