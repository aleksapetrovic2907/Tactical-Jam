using UnityEngine;

namespace Aezakmi.SelectionSystem
{
    public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color highlightedColor;

        private Color m_originalColor;

        public void OnSelect()
        {
            renderer.material.color = highlightedColor;
        }

        public void OnDeselect()
        {
            renderer.material.color = m_originalColor;
        }

        private void Awake() => m_originalColor = renderer.material.color;
    }
}