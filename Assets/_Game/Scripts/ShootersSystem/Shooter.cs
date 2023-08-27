using UnityEngine;
using Aezakmi.ColorSystem;

namespace Aezakmi.ShootersSystem
{
    public class Shooter : MonoBehaviour, IShootable
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletStartPoint;
        [SerializeField] private GameObject bloodPrefab;

        private ColorControllerBase m_colorControllerBase;
        private ShooterAnimationController m_shooterAnimationController;

        private void Start()
        {
            m_colorControllerBase = GetComponent<ColorControllerBase>();
            m_shooterAnimationController = GetComponent<ShooterAnimationController>();
        }

        public void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, bulletStartPoint.position, bulletPrefab.transform.rotation).GetComponent<Bullet>();
            bullet.SetData(transform.forward);
            bullet.GetComponent<BulletColorController>().index = m_colorControllerBase.index;
        }

        public void GetShot(Bullet bullet)
        {
            Instantiate(bloodPrefab, bullet.transform.position, bloodPrefab.transform.rotation);
            m_shooterAnimationController.StartDeathAnimation();
            GameManager.Instance.ShooterKilled();
        }
    }
}