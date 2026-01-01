using UnityEngine;

namespace MountAndBlade2D.Party
{
    /// <summary>
    /// Tracks food supplies for the party.
    /// </summary>
    public class FoodInventory : MonoBehaviour
    {
        [SerializeField] private int startingFood = 30;

        public int Food { get; private set; }

        private void Awake()
        {
            Food = startingFood;
        }

        public void Add(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            Food += amount;
        }

        public bool TryConsume(int amount)
        {
            if (amount <= 0)
            {
                return true;
            }

            if (Food < amount)
            {
                return false;
            }

            Food -= amount;
            return true;
        }

        public void SetFood(int amount)
        {
            Food = Mathf.Max(0, amount);
        }
    }
}
