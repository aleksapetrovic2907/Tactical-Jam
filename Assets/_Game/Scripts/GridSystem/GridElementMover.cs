using System;
using UnityEngine;

namespace Aezakmi.GridSystem
{
    [RequireComponent(typeof(GridNodeElement))]
    public abstract class GridElementMover : MonoBehaviour
    {
        public delegate void OnMoveStatusChange();
        public OnMoveStatusChange onReachedEnd;
        public OnMoveStatusChange onStartedMoving;

        public abstract void StartMoving(GridNode gridNode);
    }
}