using UnityEngine;
using MountAndBlade2D.Core;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Drops loot into the party inventory when this unit dies.
    /// </summary>
    [RequireComponent(typeof(Character.HealthComponent))]
    public class LootDropper : MonoBehaviour
    {
        [SerializeField] private LootTable lootTable;
        [SerializeField] private int rolls = 1;

        private Character.HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<Character.HealthComponent>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.Died += HandleDeath;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.Died -= HandleDeath;
            }
        }

        private void HandleDeath(Character.HealthComponent health)
        {
            if (lootTable == null || GameState.Instance == null || GameState.Instance.PartyInventory == null)
            {
                return;
            }

            for (var i = 0; i < rolls; i++)
            {
                var entry = SelectEntry();
                if (entry.item == null)
                {
                    continue;
                }

                var amount = Random.Range(entry.minAmount, entry.maxAmount + 1);
                if (amount > 0)
                {
                    GameState.Instance.PartyInventory.Add(entry.item, amount);
                }
            }
        }

        private LootTable.Entry SelectEntry()
        {
            var totalWeight = 0f;
            foreach (var entry in lootTable.entries)
            {
                totalWeight += Mathf.Max(0f, entry.weight);
            }

            if (totalWeight <= 0f)
            {
                return lootTable.entries.Count > 0
                    ? lootTable.entries[Random.Range(0, lootTable.entries.Count)]
                    : new LootTable.Entry();
            }

            var roll = Random.Range(0f, totalWeight);
            var cumulative = 0f;
            foreach (var entry in lootTable.entries)
            {
                cumulative += Mathf.Max(0f, entry.weight);
                if (roll <= cumulative)
                {
                    return entry;
                }
            }

            return lootTable.entries[0];
        }
    }
}
