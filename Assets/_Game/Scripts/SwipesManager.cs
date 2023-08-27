using System;
using UnityEngine;

namespace Aezakmi
{
    public enum Direction
    { Up, Down, Left, Right }

    public class SwipesManager : GloballyAccessibleBase<SwipesManager>
    {
        public event Action<Direction> OnSwiped;

        private static float s_minSwipeDistance = 50f;

        private Vector2 m_startPosition;
        private Vector2 m_endPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_startPosition = Input.mousePosition;
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                m_endPosition = Input.mousePosition;
                DetectSwipe();
                return;
            }
        }

        private void DetectSwipe()
        {
            float swipeDistance = Vector2.Distance(m_startPosition, m_endPosition);
            if (swipeDistance < s_minSwipeDistance) return;

            Direction swipeDirection;
            Vector2 swipeDirectionNormalized = (m_endPosition - m_startPosition).normalized;

            float positiveX = Mathf.Abs(swipeDirectionNormalized.x);
            float positiveY = Mathf.Abs(swipeDirectionNormalized.y);

            if (positiveX > positiveY)
            {
                swipeDirection = (swipeDirectionNormalized.x > 0) ? Direction.Right : Direction.Left;
            }
            else
            {
                swipeDirection = (swipeDirectionNormalized.y > 0) ? Direction.Up : Direction.Down;
            }

            OnSwiped?.Invoke(swipeDirection);
        }
    }
}