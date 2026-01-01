using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "MountAndBlade2D/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemId = "item_default";
        public string displayName = "Item";
        public int value = 10;
        public Sprite icon;
    }
}
