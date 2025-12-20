using UnityEngine;
using MountAndBlade2D.Party;
using MountAndBlade2D.ScriptableObjects;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Simple pickup that grants items to the party inventory.
    /// </summary>
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private ItemData item;
        [SerializeField] private int amount = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var inventory = other.GetComponent<PartyInventory>();
            if (inventory != null)
            {
                inventory.Add(item, amount);
                Destroy(gameObject);
            }
        }
    }
}
