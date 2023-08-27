using UnityEngine;

namespace Aezakmi.ColorSystem
{
    public class ShootersColorController : ColorControllerBase
    {
        [SerializeField] private new Renderer renderer;

        private void Start() => Colorize();

        public override void Colorize()
        {
            renderer.material.color = ColorsManager.Instance.GetColor(index);
            renderer.material.SetColor("_Fresnel_Color", ColorsManager.Instance.GetFresnelColor(index));
        }
    }
}