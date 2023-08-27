using UnityEngine;

namespace Aezakmi
{
    public class FPSSetter : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate;
        private void Awake() => Application.targetFrameRate = targetFrameRate;
    }
}