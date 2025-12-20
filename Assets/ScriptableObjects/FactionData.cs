using UnityEngine;

namespace MountAndBlade2D.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FactionData", menuName = "MountAndBlade2D/FactionData")]
    public class FactionData : ScriptableObject
    {
        public string factionName = "Neutral";
        public Color primaryColor = Color.gray;
        public Color secondaryColor = Color.white;
        public Sprite banner;
        [Range(-100, 100)] public int startingRelations = 0;
    }
}
