using UnityEngine;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Constrains an overworld object within a rectangular boundary.
    /// </summary>
    public class OverworldBounds : MonoBehaviour
    {
        [SerializeField] private Vector2 minBounds = new(-20f, -10f);
        [SerializeField] private Vector2 maxBounds = new(20f, 10f);

        public Vector2 Clamp(Vector2 position)
        {
            return new Vector2(
                Mathf.Clamp(position.x, minBounds.x, maxBounds.x),
                Mathf.Clamp(position.y, minBounds.y, maxBounds.y));
        }
    }
}
