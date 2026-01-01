using UnityEngine;
using MountAndBlade2D.Core;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Grants gold, renown, and morale when this unit dies.
    /// </summary>
    [RequireComponent(typeof(Character.HealthComponent))]
    public class CombatRewardOnDeath : MonoBehaviour
    {
        [SerializeField] private int goldReward = 5;
        [SerializeField] private int renownReward = 1;
        [SerializeField] private float moraleReward = 2f;

        private Character.HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<Character.HealthComponent>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.Died += HandleDeath;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.Died -= HandleDeath;
            }
        }

        private void HandleDeath(Character.HealthComponent health)
        {
            if (GameState.Instance == null)
            {
                return;
            }

            GameState.Instance.Wallet?.Add(goldReward);
            GameState.Instance.RenownSystem?.AddRenown(renownReward);
            if (GameState.Instance.MoraleSystem != null)
            {
                GameState.Instance.MoraleSystem.SetMorale(GameState.Instance.MoraleSystem.Morale + moraleReward);
            }
        }
    }
}
