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
        }
    }
}
