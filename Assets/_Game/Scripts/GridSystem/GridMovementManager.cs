using UnityEngine;
using Aezakmi.SelectionSystem;

namespace Aezakmi.GridSystem
{
    public class GridMovementManager : MonoBehaviour
    {
        public bool ElementMoving { get; private set; } = false;

        [SerializeField] private GridManager gridManager;

        private GridElementMover m_gridElementMover = null;

        private void OnEnable()
        {
            SwipesManager.Instance.OnSwiped += Move;
            SelectionsManager.Instance.OnSelection += SetMover;
        }

        private void OnDisable()
        {
            SwipesManager.Instance.OnSwiped += Move;
            SelectionsManager.Instance.OnSelection += SetMover;
        }

        private void Move(Direction direction)
        {
            if (m_gridElementMover == null || ElementMoving) return;

            var startingNode = m_gridElementMover.GetComponent<GridNodeElement>().GridNode;

            GridNode endingNode = null;
            if (direction == Direction.Up) endingNode = gridManager.GetGridNodeAbove(startingNode);
            if (direction == Direction.Down) endingNode = gridManager.GetGridNodeBelow(startingNode);
            if (direction == Direction.Left) endingNode = gridManager.GetGridNodeLeft(startingNode);
            if (direction == Direction.Right) endingNode = gridManager.GetGridNodeRight(startingNode);

            if (endingNode == null) return;
            if (endingNode.Occupied) return;

            ElementMoving = true;

            m_gridElementMover.onReachedEnd += delegate { ElementMoving = false; };
            startingNode.RemoveElement();
            endingNode.SetNewElement(m_gridElementMover.GetComponent<GridNodeElement>());
            m_gridElementMover.StartMoving(endingNode);
        }

        private void SetMover(Selection selection) => m_gridElementMover = selection.transform.GetComponent<GridElementMover>();
    }
}