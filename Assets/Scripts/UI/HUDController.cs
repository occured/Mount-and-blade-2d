using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HealthComponent playerHealth;
        [SerializeField] private StaminaComponent playerStamina;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private Party.CurrencyWallet wallet;
        [SerializeField] private Party.MoraleSystem moraleSystem;
        [SerializeField] private TMPro.TMP_Text goldLabel;
        [SerializeField] private TMPro.TMP_Text moraleLabel;

        private void Start()
        {
            if (playerHealth != null)
            {
                healthSlider.maxValue = playerHealth.CurrentHealth;
                healthSlider.value = playerHealth.CurrentHealth;
            }

            if (playerStamina != null && staminaSlider != null)
            {
                staminaSlider.maxValue = playerStamina.Max;
                staminaSlider.value = playerStamina.Current;
            }

            UpdateGold();
            UpdateMorale();
        }

        private void Update()
        {
            if (playerHealth != null)
            {
                healthSlider.value = playerHealth.CurrentHealth;
            }

            if (playerStamina != null && staminaSlider != null)
            {
                staminaSlider.value = playerStamina.Current;
            }

            UpdateGold();
            UpdateMorale();
        }

        private void UpdateGold()
        {
            if (wallet == null || goldLabel == null)
            {
                return;
            }

            goldLabel.text = $"Gold: {wallet.Gold}";
        }

        private void UpdateMorale()
        {
            if (moraleSystem == null || moraleLabel == null)
            {
                return;
            }

            moraleLabel.text = $"Morale: {moraleSystem.Morale:0}";
        }
    }
}
