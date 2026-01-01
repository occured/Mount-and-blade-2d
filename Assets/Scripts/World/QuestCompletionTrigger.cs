using UnityEngine;
using MountAndBlade2D.Core;
using MountAndBlade2D.ScriptableObjects;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Completes a quest when the player enters a trigger.
    /// </summary>
    public class QuestCompletionTrigger : MonoBehaviour
    {
        [SerializeField] private QuestData quest;
        [SerializeField] private QuestRewardSystem rewardSystem;
        [SerializeField] private bool destroyOnComplete = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (quest == null)
            {
                return;
            }

            if (rewardSystem != null)
            {
                rewardSystem.CompleteQuest(quest);
            }
            else if (GameState.Instance?.QuestLog != null)
            {
                GameState.Instance.QuestLog.CompleteQuest(quest);
            }

            if (destroyOnComplete)
            {
                Destroy(gameObject);
            }
        }
    }
}
