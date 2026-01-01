using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.UI
{
    public class QuestLogPanel : MonoBehaviour
    {
        [SerializeField] private QuestLog questLog;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button entryPrefab;

        private void OnEnable()
        {
            Render();
        }

        private void Render()
        {
            if (questLog == null || contentRoot == null || entryPrefab == null)
            {
                return;
            }

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var quest in questLog.ActiveQuests)
            {
                if (quest == null)
                {
                    continue;
                }

                var button = Instantiate(entryPrefab, contentRoot);
                var label = button.GetComponentInChildren<TMPro.TMP_Text>();
                if (label != null)
                {
                    label.text = quest.title;
                }

                button.interactable = false;
            }
        }
    }
}
