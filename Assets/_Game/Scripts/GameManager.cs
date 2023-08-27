using System.Collections.Generic;
using UnityEngine;
using Aezakmi.ShootersSystem;
using Aezakmi.SelectionSystem;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        [SerializeField] private List<Target> targets;

        private uint m_targetsDestroyed = 0;

        public void ShooterKilled()
        {
            Destroy(SwipesManager.Instance);
            Destroy(SelectionsManager.Instance);
            GameManagerUI.Instance.EnableLoseScreen();
        }

        public void TargetDestroyed()
        {
            if (++m_targetsDestroyed != targets.Count) return;
            Destroy(SwipesManager.Instance);
            Destroy(SelectionsManager.Instance);
            GameManagerUI.Instance.EnableWinScreen();
        }

        public void RestartLevel() => SceneNavigator.Instance.RestartLevel();
        public void GoToNextLevel() => SceneNavigator.Instance.LevelPassed();
    }
}