using System;
using UnityEngine;
using Array2DEditor;
using NaughtyAttributes;

namespace Aezakmi.GridSystem
{
    public class GridCreator : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private Array2DBool gridBlueprint;
        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject gridPrefab;
        [HideInInspector][SerializeField] private GridRow[] m_gridRows;

        private void Awake() => Unwrap(m_gridRows, out gridManager.grid);

        [Button]
        private void CreateGrid()
        {
            EmptyExistingGrid();

            var x = gridBlueprint.GridSize.x;
            var y = gridBlueprint.GridSize.y;

            m_gridRows = new GridRow[x];

            for (int i = 0; i < x; i++)
            {
                m_gridRows[i] = new GridRow { gridColumns = new GridNode[y] };

                for (int j = 0; j < x; j++)
                {
                    bool containsNode = gridBlueprint.GetCell(i, j);

                    if (containsNode)
                    {
                        var node = Instantiate(gridPrefab, gridParent).GetComponent<GridNode>();
                        node.i = i;
                        node.j = j;
                        m_gridRows[i].gridColumns[j] = node;
                        node.transform.localPosition = new Vector3(i * GridNode.SIZE, 0, (y - j - 1) * GridNode.SIZE);
                        node.gameObject.name = $"GridNode({i}, {j})";
                    }
                    else
                    {
                        m_gridRows[i].gridColumns[j] = null;
                    }
                }
            }

            // var grid = new GridNode[x, y];

            // for (int i = 0; i < x; i++)
            // {
            //     for (int j = 0; j < y; j++)
            //     {
            //         bool containsNode = gridBlueprint.GetCell(i, j);

            //         if (containsNode)
            //         {
            //             var node = Instantiate(gridPrefab, gridParent).GetComponent<GridNode>();
            //             node.i = i;
            //             node.j = j;
            //             grid[i, j] = node;
            //             node.transform.localPosition = new Vector3(i * GridNode.SIZE, 0, (y - j - 1) * GridNode.SIZE);
            //             node.gameObject.name = $"Node({i}, {j})";
            //         }
            //         else
            //         {
            //             grid[i, j] = null;
            //         }
            //     }
            // }

            // m_gridDataWrapper.gridNodes = grid.Clone() as GridNode[,];
        }

        private void EmptyExistingGrid()
        {
            if (gridParent.childCount == 0) return;

            for (int i = gridParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(gridParent.GetChild(i).gameObject);
            }
        }

        [Button]
        private void FillBlueprint()
        {
            for (int i = 0; i < gridBlueprint.GridSize.x; i++)
            {
                for (int j = 0; j < gridBlueprint.GridSize.y; j++)
                {
                    gridBlueprint.SetCell(i, j, true);
                }
            }
        }

        [Button]
        private void EmptyBlueprint()
        {
            for (int i = 0; i < gridBlueprint.GridSize.x; i++)
            {
                for (int j = 0; j < gridBlueprint.GridSize.y; j++)
                {
                    gridBlueprint.SetCell(i, j, false);
                }
            }
        }

        private void Unwrap(GridRow[] rows, out GridNode[,] grid)
        {
            var x = gridBlueprint.GridSize.x;
            var y = gridBlueprint.GridSize.y;

            grid = new GridNode[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    grid[i, j] = rows[i].gridColumns[j];
                }
            }
        }
    }


    // Grid data wrapper.
    [Serializable]
    public class GridRow
    {
        public GridNode[] gridColumns;
    }
}