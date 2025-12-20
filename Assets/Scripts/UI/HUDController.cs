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
        [SerializeField] private Party.FoodInventory foodInventory;
        [SerializeField] private Party.RenownSystem renownSystem;
        [SerializeField] private TMPro.TMP_Text goldLabel;
        [SerializeField] private TMPro.TMP_Text moraleLabel;
        [SerializeField] private TMPro.TMP_Text foodLabel;
        [SerializeField] private TMPro.TMP_Text renownLabel;

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
            UpdateFood();
            UpdateRenown();
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
            UpdateFood();
            UpdateRenown();
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

        private void UpdateFood()
        {
            if (foodInventory == null || foodLabel == null)
            {
                return;
            }

            foodLabel.text = $"Food: {foodInventory.Food}";
        }

        private void UpdateRenown()
        {
            if (renownSystem == null || renownLabel == null)
            {
                return;
            }

            renownLabel.text = $"Renown: {renownSystem.Renown}";
        }
    }
}
