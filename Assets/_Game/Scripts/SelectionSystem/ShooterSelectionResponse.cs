using UnityEngine;

namespace Aezakmi.SelectionSystem
{
    public class ShooterSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private GameObject circleIndicator;
        [SerializeField] private GameObject arrowIndicator;

        public void OnSelect()
        {
            circleIndicator.SetActive(true);
            arrowIndicator.SetActive(true);
            renderer.material.SetInt("_Active_Fresnel", 1);
        }

        public void OnDeselect()
        {
            circleIndicator.SetActive(false);
            arrowIndicator.SetActive(false);
            renderer.material.SetInt("_Active_Fresnel", 0);
        }
    }
}