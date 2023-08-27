using UnityEngine;

namespace Aezakmi
{
    public class RotationResetter : MonoBehaviour
    {
        [SerializeField] private Vector3 eulerAngles;
        private void LateUpdate() => transform.eulerAngles = eulerAngles;
    }
}