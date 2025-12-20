using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Party;

namespace MountAndBlade2D.UI
{
    public class PartyInventoryPanel : MonoBehaviour
    {
        [SerializeField] private PartyInventory inventory;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button itemButtonPrefab;

        private void OnEnable()
        {
            Render();
        }

        private void Render()
        {
            if (inventory == null || contentRoot == null || itemButtonPrefab == null)
            {
                return;
            }

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var stack in inventory.Items)
            {
                if (stack.item == null)
                {
                    continue;
                }

                var button = Instantiate(itemButtonPrefab, contentRoot);
                var label = button.GetComponentInChildren<TMPro.TMP_Text>();
                if (label != null)
                {
                    label.text = $"{stack.item.displayName} x{stack.count}";
                }

                button.interactable = false;
            }
        }
    }
}
