using UnityEngine;

namespace Aezakmi
{
    public class GameManagerUI : GloballyAccessibleBase<GameManagerUI>
    {
        [SerializeField] private GameObject levelPassedCanvas;
        [SerializeField] private GameObject levelFailedCanvas;

        public void EnableWinScreen() => levelPassedCanvas.SetActive(true);
        public void EnableLoseScreen() => levelFailedCanvas.SetActive(true);
    }
}