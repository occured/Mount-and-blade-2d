using UnityEngine;
using UnityEngine.SceneManagement;

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Autosaves when a scene change occurs.
    /// </summary>
    public class AutosaveOnSceneChange : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            saveSystem?.Save();
        }
    }
}
