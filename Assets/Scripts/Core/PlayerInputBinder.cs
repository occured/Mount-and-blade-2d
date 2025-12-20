using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MountAndBlade2D.Core
{
    /// <summary>
    /// Binds a PlayerInput component to an InputActionAsset.
    /// </summary>
    public class PlayerInputBinder : MonoBehaviour
    {
#if ENABLE_INPUT_SYSTEM
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InputActionAsset inputActions;

        private void Awake()
        {
            if (playerInput != null && inputActions != null)
            {
                playerInput.actions = inputActions;
            }
        }
#endif
    }
}
