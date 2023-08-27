using UnityEngine;

namespace Aezakmi.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        public GridNode[,] grid;

        public GridNode GetGridNodeAbove(GridNode referenceGridNode)
        {
            if (OutOfBounds(referenceGridNode.i, referenceGridNode.j - 1)) return null;
            return grid[referenceGridNode.i, referenceGridNode.j - 1];
        }

        public GridNode GetGridNodeBelow(GridNode referenceGridNode)
        {
            if (OutOfBounds(referenceGridNode.i, referenceGridNode.j + 1)) return null;
            return grid[referenceGridNode.i, referenceGridNode.j + 1];
        }

        public GridNode GetGridNodeLeft(GridNode referenceGridNode)
        {
            if (OutOfBounds(referenceGridNode.i - 1, referenceGridNode.j)) return null;
            return grid[referenceGridNode.i - 1, referenceGridNode.j];
        }

        public GridNode GetGridNodeRight(GridNode referenceGridNode)
        {
            if (OutOfBounds(referenceGridNode.i + 1, referenceGridNode.j)) return null;
            return grid[referenceGridNode.i + 1, referenceGridNode.j];
        }

        private bool OutOfBounds(int i, int j)
        {
            var x = grid.GetLength(0);
            var y = grid.GetLength(1);

            return (i < 0 || i >= x) || (j < 0 || j >= y);
        }
    }
}