using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Displays a defeat panel before returning to the overworld.
    /// </summary>
    public class CombatDefeatUI : MonoBehaviour
    {
        [SerializeField] private CombatResolution combatResolution;
        [SerializeField] private GameObject defeatPanel;
        [SerializeField] private UI.SceneTransitionFader fader;
        [SerializeField] private float returnDelaySeconds = 2f;

        private void OnEnable()
        {
            if (combatResolution != null)
            {
                combatResolution.Defeat += HandleDefeat;
            }
        }

        private void OnDisable()
        {
            if (combatResolution != null)
            {
                combatResolution.Defeat -= HandleDefeat;
            }
        }

        private void HandleDefeat(string overworldSceneName)
        {
            if (defeatPanel != null)
            {
                defeatPanel.SetActive(true);
            }

            if (fader != null)
            {
                fader.FadeOut(this);
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
