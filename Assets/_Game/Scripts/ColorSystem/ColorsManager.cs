using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.ColorSystem
{
    public class ColorsManager : GloballyAccessibleBase<ColorsManager>
    {
        [SerializeField] private List<Color> colors;

        [ColorUsage(true, true)]
        [SerializeField] private List<Color> fresnelColors;

        public Color GetColor(int index) => colors[index];
        public Color GetFresnelColor(int index) => fresnelColors[index];
    }
}