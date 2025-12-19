using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Attach to weapon hitboxes; forwards trigger hits to a HitResolver.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private HitResolver resolver;

        private void Reset()
        {
            var collider2d = GetComponent<Collider2D>();
            collider2d.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (resolver == null)
            {
                return;
            }

            resolver.Resolve(other);
        }
    }
}
