using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;
using MountAndBlade2D.Party;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Tracks kill objectives for active quests and completes them when targets are met.
    /// </summary>
    public class KillQuestTracker : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;
        [SerializeField] private QuestRewardSystem rewardSystem;

        private readonly Dictionary<QuestData, int> _progress = new();

        private void OnEnable()
        {
            HealthComponent.OnAnyDeath += HandleDeath;
        }

        private void OnDisable()
        {
            HealthComponent.OnAnyDeath -= HandleDeath;
        }

        private void HandleDeath(HealthComponent health)
        {
            if (health == null || questLog == null)
            {
                return;
            }

            foreach (var quest in questLog.ActiveQuests)
            {
                if (quest == null)
                {
                    continue;
                }

                if (quest.killsRequired <= 0 || quest.targetFaction == null)
                {
                    continue;
                }

                if (health.OwnerFaction != quest.targetFaction)
                {
                    continue;
                }

                var current = 0;
                _progress.TryGetValue(quest, out current);
                current++;
                _progress[quest] = current;

                if (current >= quest.killsRequired)
                {
                    if (rewardSystem != null)
                    {
                        rewardSystem.CompleteQuest(quest);
                    }
                    else
                    {
                        questLog.CompleteQuest(quest);
                    }
                }
            }
        }
    }
}
