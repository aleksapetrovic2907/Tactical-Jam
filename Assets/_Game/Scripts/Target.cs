using UnityEngine;
using Aezakmi.ColorSystem;
using DG.Tweening;

namespace Aezakmi.ShootersSystem
{
    [RequireComponent(typeof(ColorControllerBase))]
    public class Target : MonoBehaviour, IShootable
    {
        [SerializeField] private Transform targetPad;
        [SerializeField] private Vector3 padUpEuler;
        [SerializeField] private float getUpDuration;

        [Space(5)]
        [SerializeField] private Vector3 destroyedEuler;
        [SerializeField] private float destroyDuration;

        [Space(5)]
        [SerializeField] private Vector3 wronglyShotEuler;
        [SerializeField] private float wronglyShotDuration;

        [SerializeField] private GameObject destroyedExplosionPrefab;
        [SerializeField] private GameObject wronglyExplosionPrefab;

        private ColorControllerBase m_colorControllerBase;

        private Tween m_destroyedTween;
        private Sequence m_wronglyShotSequence; // When shooter and target are NOT the same color (index).

        private void Start()
        {
            m_colorControllerBase = GetComponent<ColorControllerBase>();
            m_destroyedTween = targetPad.DOLocalRotate(destroyedEuler, destroyDuration).SetEase(Ease.OutSine);

            m_wronglyShotSequence = DOTween.Sequence();

            m_wronglyShotSequence
                .Append(targetPad.DOLocalRotate(wronglyShotEuler, wronglyShotDuration).SetEase(Ease.InOutSine))
                .Append(targetPad.DOLocalRotate(padUpEuler, getUpDuration).SetEase(Ease.OutSine));
        }

        public void GetShot(Bullet bullet)
        {
            var bulletCCB = bullet.GetComponent<ColorControllerBase>();

            if (bulletCCB.index == m_colorControllerBase.index)
            {
                Destroy(GetComponent<Collider>());
                m_destroyedTween.Play();
                Instantiate(destroyedExplosionPrefab, bullet.transform.position, destroyedExplosionPrefab.transform.rotation);
                GameManager.Instance.TargetDestroyed();
            }
            else
            {
                m_wronglyShotSequence.Rewind();
                m_wronglyShotSequence.Play();
                Instantiate(wronglyExplosionPrefab, bullet.transform.position, wronglyExplosionPrefab.transform.rotation);
            }

        }
    }
}