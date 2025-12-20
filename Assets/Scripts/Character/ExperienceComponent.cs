using System;
using UnityEngine;

namespace MountAndBlade2D.Character
{
    /// <summary>
    /// Tracks experience and levels for a character.
    /// </summary>
    public class ExperienceComponent : MonoBehaviour
    {
        [SerializeField] private int level = 1;
        [SerializeField] private int currentXp;
        [SerializeField] private int baseXpToLevel = 100;
        [SerializeField] private float xpGrowth = 1.25f;

        public int Level => level;
        public int CurrentXp => currentXp;

        public event Action<int> LeveledUp;

        public void AddXp(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            currentXp += amount;
            while (currentXp >= GetXpToNextLevel())
            {
                currentXp -= GetXpToNextLevel();
                level++;
                LeveledUp?.Invoke(level);
            }
        }

        public int GetXpToNextLevel()
        {
            return Mathf.RoundToInt(baseXpToLevel * Mathf.Pow(xpGrowth, level - 1));
        }
    }
}
