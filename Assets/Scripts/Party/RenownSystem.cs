using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks renown gained from victories and quests.
    /// </summary>
    public class RenownSystem : MonoBehaviour
    {
        [SerializeField] private int renown;

        public int Renown => renown;

        public void AddRenown(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            renown += amount;
        }

        public void SetRenown(int value)
        {
            renown = Mathf.Max(0, value);
        }
    }
}
