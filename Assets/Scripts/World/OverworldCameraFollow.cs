using UnityEngine;

namespace MountAndBlade2D.World
{
    /// <summary>
    /// Smoothly follows a target with optional damping.
    /// </summary>
    public class OverworldCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothTime = 0.15f;
        [SerializeField] private Vector3 offset = new(0f, 0f, -10f);

        private Vector3 _velocity;

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothTime);
        }
    }
}
