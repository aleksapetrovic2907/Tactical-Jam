using UnityEngine;

namespace Aezakmi.GridSystem
{
    public class GridNode : MonoBehaviour
    {
        public int i, j;
        public bool Occupied => gridNodeElement != null;
        public GridNodeElement gridNodeElement;

        public const float SIZE = 1f;

        private void Awake()
        {
            if (gridNodeElement == null) return;
            gridNodeElement.GridNode = this;
        }

        public void SetNewElement(GridNodeElement element)
        {
            gridNodeElement = element;
            gridNodeElement.GridNode = this;
        }

        public void RemoveElement()
        {
            gridNodeElement = null;
        }
    }
}