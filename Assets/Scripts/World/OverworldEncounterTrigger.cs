using UnityEngine;
using UnityEngine.SceneManagement;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Triggers a combat scene when the player collides with an encounter collider.
    /// </summary>
    public class OverworldEncounterTrigger : MonoBehaviour
    {
        [SerializeField] private string combatSceneName = "Scenes/Combat";
        [SerializeField] private bool destroyOnTrigger = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            SceneManager.LoadScene(combatSceneName);
            if (destroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
