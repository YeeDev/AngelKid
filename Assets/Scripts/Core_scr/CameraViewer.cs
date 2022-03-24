using UnityEngine;

namespace AK.Core
{
    public class CameraViewer : MonoBehaviour
    {
        [Range(0, 10)] [SerializeField] float smoothYSpeed = 1f;
        [Range(0, 10)] [SerializeField] float yOffset = 1f;
        [Header("Camera Locks")]
        [SerializeField] Transform leftLock = null;
        [SerializeField] Transform rightLock = null;

        float refVelocity = 0f;
        Transform player;

        public bool IsPlayerGrounded { get; set; }

        private void Awake() { player = GameObject.FindGameObjectWithTag("Player").transform; }

        private void LateUpdate() { transform.position = CalculateFollowPosition(); }

        private Vector3 CalculateFollowPosition()
        {
            Vector3 followPosition = transform.position;
            followPosition.x = player.position.x;

            float smoothY = Mathf.SmoothDamp(transform.position.y, player.position.y + yOffset, ref refVelocity, smoothYSpeed);
            followPosition.y = IsPlayerGrounded ? smoothY : followPosition.y;
            followPosition.y = Mathf.Clamp(followPosition.y, leftLock.position.y, rightLock.position.y);

            return followPosition;
        }
    }
}