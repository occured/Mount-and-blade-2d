using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Spawns roaming parties based on an encounter table at timed intervals.
    /// </summary>
    public class EncounterSpawner : MonoBehaviour
    {
        [SerializeField] private EncounterTable encounterTable;
        [SerializeField] private PartyAIController partyPrefab;
        [SerializeField] private float spawnIntervalSeconds = 12f;
        [SerializeField] private int maxActiveParties = 6;
        [SerializeField] private Vector2 spawnRadius = new(12f, 8f);

        private float _timer;
        private int _activeCount;

        private void Update()
        {
            if (encounterTable == null || partyPrefab == null)
            {
                return;
            }

            if (_activeCount >= maxActiveParties)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= spawnIntervalSeconds)
            {
                _timer = 0f;
                SpawnWave();
            }
        }

        private void SpawnWave()
        {
            if (encounterTable.entries.Count == 0)
            {
                return;
            }

            var spawnCount = Random.Range(encounterTable.minSpawnCount, encounterTable.maxSpawnCount + 1);
            spawnCount = Mathf.Min(spawnCount, maxActiveParties - _activeCount);
            for (var i = 0; i < spawnCount; i++)
            {
                SpawnParty();
            }
        }

        private void SpawnParty()
        {
            var entry = SelectEntry();
            var spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnRadius.x, spawnRadius.x),
                Random.Range(-spawnRadius.y, spawnRadius.y),
                0f);

            var instance = Instantiate(partyPrefab, spawnPosition, Quaternion.identity, transform);
            if (entry.faction != null)
            {
                instance.name = $"{entry.faction.factionName} Party";
            }

            var encounterParty = instance.GetComponent<EncounterParty>();
            if (encounterParty != null)
            {
                encounterParty.Configure(entry);
            }
            _activeCount++;
            instance.gameObject.AddComponent<PartyLifetime>().Init(() => _activeCount--);
        }

        private EncounterTable.Entry SelectEntry()
        {
            var totalWeight = 0f;
            foreach (var entry in encounterTable.entries)
            {
                totalWeight += Mathf.Max(0f, entry.weight);
            }

            if (totalWeight <= 0f)
            {
                return encounterTable.entries[Random.Range(0, encounterTable.entries.Count)];
            }

            var roll = Random.Range(0f, totalWeight);
            var cumulative = 0f;
            foreach (var entry in encounterTable.entries)
            {
                cumulative += Mathf.Max(0f, entry.weight);
                if (roll <= cumulative)
                {
                    return entry;
                }
            }

            return encounterTable.entries[0];
        }
    }
}
