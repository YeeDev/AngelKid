using UnityEngine;

namespace AK.Core
{
    public class CameraViewer : MonoBehaviour
    {
        [SerializeField] float smoothSpeed = 1f;
        [SerializeField] float yOffset = 1f;
        [SerializeField] Transform leftLock = null;
        [SerializeField] Transform rightLock = null;

        float yVelocity = 0f;
        Transform player;

        public bool IsPlayerGrounded { get; set; }

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            Vector3 followPosition = transform.position;
            followPosition.x = Mathf.Clamp(player.position.x, leftLock.position.x, rightLock.position.x);

            float smoothTransition = Mathf.SmoothDamp(transform.position.y, player.position.y + yOffset, ref yVelocity, smoothSpeed);
            followPosition.y = IsPlayerGrounded ? smoothTransition : followPosition.y;
            followPosition.y = Mathf.Clamp(followPosition.y, leftLock.position.y, rightLock.position.y);

            transform.position = followPosition;
        }
    }
}