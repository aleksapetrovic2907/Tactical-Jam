using System.Collections;
using UnityEngine;
using DG.Tweening;
using Aezakmi.GridSystem;
using Aezakmi.SelectionSystem;

namespace Aezakmi.InstructionsSystem
{
    public class SwipeInstruction : MonoBehaviour
    {
        [SerializeField] private GameObject swipeToMove;
        [SerializeField] private GameObject tapToShoot;

        [SerializeField] private GameObject handCursorUp;
        [SerializeField] private GameObject handCursorRight;
        [SerializeField] private GridNode intersectionNode;
        [SerializeField] private GridNode endNode;

        private bool m_reachedEndNode = false;

        private void OnEnable() => SelectionsManager.Instance.OnReselection += delegate { InstructionComplete(); };
        private void OnDisable() => SelectionsManager.Instance.OnReselection -= delegate { InstructionComplete(); };

        public void StartInstruction()
        {
            StartCoroutine(SwipeUpInstruction());
        }

        private IEnumerator SwipeUpInstruction()
        {
            swipeToMove.SetActive(true);
            handCursorUp.SetActive(true);
            yield return new WaitUntil(() => intersectionNode.Occupied == true);
            handCursorUp.SetActive(false);
            StartCoroutine(SwipeDownInstruction());
        }

        private IEnumerator SwipeDownInstruction()
        {
            handCursorRight.SetActive(true);
            yield return new WaitUntil(() => endNode.Occupied == true);
            handCursorRight.SetActive(false);
            m_reachedEndNode = true;
            swipeToMove.SetActive(false);
            TapToShootInstruction();
        }

        [SerializeField] private Transform selectable;
        [SerializeField] private GameObject cursorParent;
        [SerializeField] private RectTransform cursor;
        [SerializeField] private float scaleAmount;
        [SerializeField] private float duration;

        private Tween m_tapTween = null;

        private void TapToShootInstruction()
        {
            tapToShoot.SetActive(true);
            cursorParent.SetActive(true);

            var positionToTweenTo = Camera.main.WorldToScreenPoint(endNode.transform.position);
            cursor.position = positionToTweenTo;
            m_tapTween = cursor.DOScale(scaleAmount, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).Play();

            Destroy(SwipesManager.Instance);
        }

        private void InstructionComplete()
        {
            if (!m_reachedEndNode) return;
            m_tapTween.Kill();
            Destroy(cursorParent);
            tapToShoot.SetActive(false);
            Destroy(this);
        }
    }
}