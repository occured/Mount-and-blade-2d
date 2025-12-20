using UnityEngine;

namespace MountAndBlade2D.Character
{
    /// <summary>
    /// Applies a movement speed multiplier while sprinting and spending stamina.
    /// </summary>
    public class SprintController : MonoBehaviour
    {
        [SerializeField] private StaminaComponent stamina;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private float staminaCostPerSecond = 12f;

        public bool IsSprinting { get; private set; }
        public float SprintMultiplier => sprintMultiplier;

        public void SetSprinting(bool sprinting)
        {
            IsSprinting = sprinting;
        }

        private void Update()
        {
            if (!IsSprinting || stamina == null)
            {
                return;
            }

            stamina.TrySpend(staminaCostPerSecond * Time.deltaTime);
        }
    }
}
