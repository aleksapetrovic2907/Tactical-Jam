using UnityEngine;

namespace Aezakmi.ColorSystem
{
    public abstract class ColorControllerBase : MonoBehaviour
    {
        public int index = 0;
        public abstract void Colorize();
    }
}