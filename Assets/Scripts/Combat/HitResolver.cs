using UnityEngine;

namespace MountAndBlade2D.Combat
{
    /// <summary>
    /// Resolves overlap hits between hitboxes and hurtboxes. Designed for 2D physics triggers.
    /// </summary>
    public class HitResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask hitboxMask;
        [SerializeField] private LayerMask hurtboxMask;
        [SerializeField] private float baseDamage = 10f;

        public void Resolve(Collider2D collider)
        {
            if (IsInLayerMask(collider.gameObject, hurtboxMask))
            {
                var health = collider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(baseDamage);
                }
            }
        }

        private static bool IsInLayerMask(GameObject obj, LayerMask mask)
        {
            return (mask.value & (1 << obj.layer)) != 0;
        }
    }
}
