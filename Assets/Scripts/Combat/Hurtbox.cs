using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Marker component for hurtboxes, useful for editor setup and validation.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hurtbox : MonoBehaviour
    {
    }
}
