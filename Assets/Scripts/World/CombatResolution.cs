using UnityEngine;
using UnityEngine.SceneManagement;
using MountAndBlade2D.Core;
using MountAndBlade2D.Character;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Tracks combatants and returns to overworld when enemies are defeated.
    /// </summary>
    public class CombatResolution : MonoBehaviour
    {
        [SerializeField] private string overworldSceneName = "Scenes/Overworld";
        [SerializeField] private PartyTroopProgression troopProgression;
        [SerializeField] private int xpPerBattle = 25;

        private int _enemyCount;

        public void RegisterEnemy(HealthComponent health)
        {
            if (health == null)
            {
                return;
            }

            _enemyCount++;
            health.Died += HandleEnemyDeath;
        }

        private void HandleEnemyDeath(HealthComponent health)
        {
            _enemyCount = Mathf.Max(0, _enemyCount - 1);
            if (_enemyCount <= 0)
            {
                ResolveVictory();
            }
        }

        private void ResolveVictory()
        {
            if (troopProgression != null)
            {
                troopProgression.AddXpToRoster(xpPerBattle);
            }

            SceneManager.LoadScene(overworldSceneName);
        }
    }
}
