using UnityEngine;
using UnityEngine.UI;
using MountAndBlade2D.Character;

namespace MountAndBlade2D.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private HealthComponent playerHealth;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider staminaSlider;

        private void Start()
        {
            if (playerHealth != null)
            {
                healthSlider.maxValue = playerHealth.CurrentHealth;
                healthSlider.value = playerHealth.CurrentHealth;
            }
        }

        private void Update()
        {
            if (playerHealth != null)
            {
                healthSlider.value = playerHealth.CurrentHealth;
            }

            // Placeholder stamina binding; hook up to a stamina component once available.
            if (staminaSlider != null && staminaSlider.maxValue <= 0f)
            {
                staminaSlider.maxValue = 100f;
                staminaSlider.value = 100f;
            }
        }
    }
}
