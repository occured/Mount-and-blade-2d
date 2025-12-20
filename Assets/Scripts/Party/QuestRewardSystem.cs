using UnityEngine;
using MountAndBlade2D.Core;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Completes a quest and applies rewards.
    /// </summary>
    public class QuestRewardSystem : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private MoraleSystem moraleSystem;
        [SerializeField] private RenownSystem renownSystem;

        public void CompleteQuest(QuestData quest)
        {
            if (quest == null || questLog == null)
            {
                return;
            }

            questLog.CompleteQuest(quest);
            wallet?.Add(quest.goldReward);
            if (moraleSystem != null)
            {
                moraleSystem.SetMorale(moraleSystem.Morale + quest.moraleReward);
            }
            renownSystem?.AddRenown(quest.moraleReward);
        }
    }
}
