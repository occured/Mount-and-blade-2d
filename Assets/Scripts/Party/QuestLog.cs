using System.Collections.Generic;
using UnityEngine;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks active and completed quests for the party.
    /// </summary>
    public class QuestLog : MonoBehaviour
    {
        [SerializeField] private List<QuestData> activeQuests = new();
        [SerializeField] private List<QuestData> completedQuests = new();

        public IReadOnlyList<QuestData> ActiveQuests => activeQuests;
        public IReadOnlyList<QuestData> CompletedQuests => completedQuests;

        public void AddQuest(QuestData quest)
        {
            if (quest == null || activeQuests.Contains(quest) || completedQuests.Contains(quest))
            {
                return;
            }

            activeQuests.Add(quest);
        }

        public void CompleteQuest(QuestData quest)
        {
            if (quest == null || !activeQuests.Remove(quest))
            {
                return;
            }

            completedQuests.Add(quest);
        }

        public void Clear()
        {
            activeQuests.Clear();
            completedQuests.Clear();
        }

        public bool IsCompleted(QuestData quest)
        {
            return quest != null && completedQuests.Contains(quest);
        }
    }
}
