using System;
using UnityEngine;

namespace Aezakmi.SelectionSystem
{
    public class SelectionsManager : GloballyAccessibleBase<SelectionsManager>
    {
        public Selection CurrentSelection { get; private set; } = null;

        public event Action<Selection> OnSelection;
        public event Action<Selection> OnReselection;
        public event Action<Selection> OnDeselection;

        [SerializeField] private LayerMask rayLayerMask;

        private Camera m_mainCamera = null;
        private const float MAX_RAY_DISTANCE = 255f;

        protected override void Awake()
        {
            base.Awake();
            m_mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (!Physics.Raycast(ray, out raycastHit, MAX_RAY_DISTANCE, rayLayerMask)) return;

            var newSelection = new Selection(raycastHit.transform, raycastHit.collider.GetComponent<ISelectionResponse>());

            if (CurrentSelection == null)
            {
                CurrentSelection = newSelection;
                CurrentSelection.selectionResponse.OnSelect();
                OnSelection?.Invoke(CurrentSelection);
                return;
            }
            else if (EqualSelections(CurrentSelection, newSelection))
            {
                OnReselection?.Invoke(CurrentSelection);
                return;
            }
            else
            {
                CurrentSelection.selectionResponse.OnDeselect();
                OnDeselection?.Invoke(CurrentSelection);
                CurrentSelection = newSelection;
                CurrentSelection.selectionResponse.OnSelect();
                OnSelection?.Invoke(CurrentSelection);
                return;
            }
        }

        private static bool EqualSelections(Selection s1, Selection s2)
        {
            return s1.transform == s2.transform;
        }
    }

    public class Selection
    {
        public Transform transform;
        public ISelectionResponse selectionResponse;

        public Selection(Transform t, ISelectionResponse sr)
        {
            transform = t;
            selectionResponse = sr;
        }
    }
}