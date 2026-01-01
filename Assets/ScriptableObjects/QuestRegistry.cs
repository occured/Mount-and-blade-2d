using System.Collections.Generic;
using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "QuestRegistry", menuName = "MountAndBlade2D/QuestRegistry")]
    public class QuestRegistry : ScriptableObject
    {
        public List<QuestData> quests = new();

        public QuestData FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            foreach (var quest in quests)
            {
                if (quest != null && quest.questId == id)
                {
                    return quest;
                }
            }

            return null;
        }
    }
}
