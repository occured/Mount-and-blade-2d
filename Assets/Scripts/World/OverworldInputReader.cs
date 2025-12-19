using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Bridges the Unity Input System to provide movement input for the overworld.
    /// </summary>
    public class OverworldInputReader : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }

#if ENABLE_INPUT_SYSTEM
        private void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }
#endif

        public void Clear()
        {
            MoveInput = Vector2.zero;
        }
    }
}
