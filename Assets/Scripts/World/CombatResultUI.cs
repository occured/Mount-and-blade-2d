using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Displays a simple victory panel before returning to the overworld.
    /// </summary>
    public class CombatResultUI : MonoBehaviour
    {
        [SerializeField] private CombatResolution combatResolution;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private float returnDelaySeconds = 2f;

        private void OnEnable()
        {
            if (combatResolution != null)
            {
                combatResolution.Victory += HandleVictory;
            }
        }

        private void OnDisable()
        {
            if (combatResolution != null)
            {
                combatResolution.Victory -= HandleVictory;
            }
        }

        private void HandleVictory(string overworldSceneName)
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
            }

            StartCoroutine(ReturnAfterDelay(overworldSceneName));
        }

        private IEnumerator ReturnAfterDelay(string sceneName)
        {
            yield return new WaitForSeconds(returnDelaySeconds);
            SceneManager.LoadScene(sceneName);
        }
    }
}
