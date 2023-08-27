using UnityEngine;
using DG.Tweening;
using Aezakmi.SelectionSystem;

namespace Aezakmi.InstructionsSystem
{
    public class SelectionInstruction : MonoBehaviour
    {
        [SerializeField] private GameObject tapToSelect;
        [SerializeField] private Transform selectable;
        [SerializeField] private GameObject cursorParent;
        [SerializeField] private RectTransform cursor;
        [SerializeField] private float scaleAmount;
        [SerializeField] private float duration;
        [SerializeField] private SwipeInstruction swipeInstruction;

        private Tween m_tapTween = null;

        private void OnEnable() => SelectionsManager.Instance.OnSelection += delegate { InstructionComplete(); };
        private void OnDisable() => SelectionsManager.Instance.OnSelection -= delegate { InstructionComplete(); };

        private void Start()
        {
            var positionToTweenTo = Camera.main.WorldToScreenPoint(selectable.position);
            cursor.position = positionToTweenTo;
            m_tapTween = cursor.DOScale(scaleAmount, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).Play();
        }

        public void InstructionComplete()
        {
            m_tapTween.Kill();
            Destroy(cursorParent);
            tapToSelect.SetActive(false);
            swipeInstruction.StartInstruction();
            Destroy(this);
        }
    }
}