using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Party;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.UI
{
    public class VendorPanel : MonoBehaviour
    {
        [SerializeField] private PartyInventory inventory;
        [SerializeField] private CurrencyWallet wallet;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button itemButtonPrefab;
        [SerializeField] private List<VendorItem> vendorItems = new();

        private void OnEnable()
        {
            RenderItems();
        }

        private void RenderItems()
        {
            if (contentRoot == null || itemButtonPrefab == null)
            {
                return;
            }

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (var entry in vendorItems)
            {
                var button = Instantiate(itemButtonPrefab, contentRoot);
                var label = button.GetComponentInChildren<TMPro.TMP_Text>();
                if (label != null && entry.item != null)
                {
                    label.text = $"{entry.item.displayName} — {entry.price}g ({entry.stock})";
                }

                button.interactable = entry.item != null && entry.stock > 0;
                button.onClick.AddListener(() => Buy(entry));
            }
        }

        private void Buy(VendorItem entry)
        {
            if (entry.item == null || inventory == null || wallet == null)
            {
                return;
            }

            if (!wallet.TrySpend(entry.price))
            {
                Debug.Log("Not enough gold to buy item.");
                return;
            }

            inventory.Add(entry.item, 1);
        }

        public void Sell(ItemData item)
        {
            if (item == null || inventory == null || wallet == null)
            {
                return;
            }

            if (!inventory.TryRemove(item, 1))
            {
                return;
            }

            wallet.Add(item.value);
        }

        [System.Serializable]
        public struct VendorItem
        {
            public ItemData item;
            public int price;
            public int stock;
        }
    }
}
