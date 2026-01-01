using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "MountAndBlade2D/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string questId = "quest_default";
        public string title = "Quest Title";
        [TextArea] public string description;
        public int goldReward = 50;
        public int moraleReward = 5;
        [Header("Objectives (optional)")]
        public FactionData targetFaction;
        public int killsRequired = 0;
    }
}
