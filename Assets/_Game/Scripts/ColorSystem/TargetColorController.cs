using UnityEngine;

namespace Aezakmi.ColorSystem
{
    public class TargetColorController : ColorControllerBase
    {
        [SerializeField] private Renderer padRenderer;
        [SerializeField] private Renderer baseRenderer;

        private void Start() => Colorize();

        public override void Colorize()
        {
            var color = ColorsManager.Instance.GetColor(index);

            padRenderer.material.SetColor("_Colored_Color", color);
            baseRenderer.material.color = color;
        }
    }
}