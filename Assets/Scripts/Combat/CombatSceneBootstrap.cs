using UnityEngine;
using MountAndBlade2D.World;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Spawns combatants based on EncounterContext data from the overworld.
    /// </summary>
    public class CombatSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform enemySpawnRoot;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform allySpawnRoot;
        [SerializeField] private GameObject allyPrefab;
        [SerializeField] private int fallbackEnemyCount = 2;

        private void Start()
        {
            if (enemyPrefab == null || enemySpawnRoot == null)
            {
                Debug.Log("CombatSceneBootstrap: missing enemy prefab or spawn root.");
                return;
            }

            SpawnAllies();
            var roster = EncounterContext.EnemyRoster;
            if (roster.Count == 0)
            {
                SpawnFallback();
            }
            else
            {
                SpawnRoster(roster);
            }

            EncounterContext.Clear();
        }

        private void SpawnAllies()
        {
            if (allyPrefab == null || allySpawnRoot == null || Core.GameState.Instance == null)
            {
                return;
            }

            var roster = Core.GameState.Instance.PartyRoster;
            if (roster == null)
            {
                return;
            }

            var index = 0;
            foreach (var member in roster.Members)
            {
                var offset = new Vector3(index * 1.5f, 0f, 0f);
                var instance = Instantiate(allyPrefab, allySpawnRoot.position + offset, Quaternion.identity, allySpawnRoot);
                var binder = instance.GetComponent<Character.CharacterStatsBinder>();
                if (binder != null)
                {
                    binder.Apply(member);
                }
                index++;
            }
        }

        private void SpawnFallback()
        {
            var count = EncounterContext.PartySize > 0 ? EncounterContext.PartySize : fallbackEnemyCount;
            for (var i = 0; i < count; i++)
            {
                var offset = new Vector3(i * 1.5f, 0f, 0f);
                Instantiate(enemyPrefab, enemySpawnRoot.position + offset, Quaternion.identity, enemySpawnRoot);
            }
        }

        private void SpawnRoster(System.Collections.Generic.List<Character.CharacterStats> roster)
        {
            for (var i = 0; i < roster.Count; i++)
            {
                var offset = new Vector3(i * 1.5f, 0f, 0f);
                var instance = Instantiate(enemyPrefab, enemySpawnRoot.position + offset, Quaternion.identity, enemySpawnRoot);
                var binder = instance.GetComponent<Character.CharacterStatsBinder>();
                if (binder != null)
                {
                    binder.Apply(roster[i]);
                }

                var resolver = FindObjectOfType<World.CombatResolution>();
                if (resolver != null)
                {
                    var health = instance.GetComponent<Character.HealthComponent>();
                    resolver.RegisterEnemy(health);
                }
            }
        }
    }
}
