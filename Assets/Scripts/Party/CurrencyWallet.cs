using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Stores party currency and handles spend/add operations.
    /// </summary>
    public class CurrencyWallet : MonoBehaviour
    {
        [SerializeField] private int startingGold = 500;

        public int Gold { get; private set; }

        private void Awake()
        {
            Gold = startingGold;
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0)
            {
                return true;
            }

            if (Gold < amount)
            {
                return false;
            }

            Gold -= amount;
            return true;
        }

        public void Add(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Gold += amount;
        }
    }
}
