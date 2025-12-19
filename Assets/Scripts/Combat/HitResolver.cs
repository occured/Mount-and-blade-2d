using UnityEngine;
using MountAndBlade2D.Character;

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
        [SerializeField] private DamageType damageType = DamageType.Slash;
        [SerializeField] private DamageProfile damageProfile;
        [SerializeField] private bool allowBlocking = true;

        public void Resolve(Collider2D collider)
        {
            if (IsInLayerMask(collider.gameObject, hurtboxMask))
            {
                var healthComponent = collider.GetComponent<MountAndBlade2D.Character.HealthComponent>();
                var health = healthComponent as IHealth ?? collider.GetComponent<IHealth>();
                if (health != null)
                {
                    var damage = damageProfile != null
                        ? damageProfile.Evaluate(damageType, baseDamage)
                        : baseDamage;
                    var stats = healthComponent != null ? healthComponent.Stats : null;
                    if (stats != null)
                    {
                        damage = stats.GetMitigatedDamage(damage);
                    }

                    if (allowBlocking)
                    {
                        var block = collider.GetComponent<BlockController>();
                        if (block != null && block.IsBlocking)
                        {
                            damage *= Mathf.Clamp01(1f - block.BlockReduction);
                        }
                    }
                    health.TakeDamage(damage);
                }
            }
        }

        private static bool IsInLayerMask(GameObject obj, LayerMask mask)
        {
            return (mask.value & (1 << obj.layer)) != 0;
        }
    }
}
