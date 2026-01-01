using UnityEngine;
using MountAndBlade2D.Core;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Grants a quest when the player interacts via trigger.
    /// </summary>
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private QuestData quest;
        [SerializeField] private bool destroyOnGive = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (GameState.Instance?.QuestLog != null && quest != null)
            {
                GameState.Instance.QuestLog.AddQuest(quest);
                if (destroyOnGive)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
