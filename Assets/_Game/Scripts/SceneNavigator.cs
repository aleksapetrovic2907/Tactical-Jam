using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aezakmi
{
    public class SceneNavigator : SingletonBase<SceneNavigator>
    {
        private const int TOTAL_LEVELS = 5;
        private int m_levelReached = 1;

        private void Start()
        {
            if (PlayerPrefs.HasKey("LevelReached"))
                m_levelReached = PlayerPrefs.GetInt("LevelReached");
            else
                m_levelReached = 1;

            SceneManager.LoadScene(m_levelReached);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(m_levelReached);
        }

        public void LevelPassed()
        {
            if (++m_levelReached > TOTAL_LEVELS) m_levelReached = 1;
            PlayerPrefs.SetInt("LevelReached", m_levelReached);
            SceneManager.LoadScene(m_levelReached);
        }
    }
}