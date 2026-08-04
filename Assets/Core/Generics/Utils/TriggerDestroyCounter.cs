using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Core.Generics.Utils
{
    public class TriggerDestroyCounter : MonoBehaviour
    {
        [SerializeField] private int m_targetCount = 4;
        [SerializeField] private int m_targetSceneIndex = 3;

        private static int s_destroyedTriggerCount;
        private static bool s_sceneChangeRequested;
        private static int s_targetSceneIndex = 3;
        private static int s_targetCount = 4;

        public static int DestroyedTriggerCount => s_destroyedTriggerCount;

        private void Awake()
        {
            s_targetCount = m_targetCount;
            s_targetSceneIndex = m_targetSceneIndex;
        }

        public static void AddDestroyedTrigger()
        {
            if (s_sceneChangeRequested)
            {
                return;
            }

            s_destroyedTriggerCount++;

            if (s_destroyedTriggerCount < s_targetCount)
            {
                return;
            }

            if (s_targetSceneIndex < 0 || s_targetSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogWarning($"TriggerDestroyCounter: target scene index {s_targetSceneIndex} is invalid.");
                return;
            }

            s_sceneChangeRequested = true;
            SceneManager.LoadScene(s_targetSceneIndex);
        }
    }
}
