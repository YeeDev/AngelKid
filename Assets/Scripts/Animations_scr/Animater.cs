using UnityEngine;

namespace AK.Animations
{
    [RequireComponent(typeof(Animator))]
    public class Animater : MonoBehaviour
    {
        [SerializeField] GameObject dustJumpEffect = null;
        [SerializeField] Transform componentsToFlip = null;
        [SerializeField] SpriteRenderer spriteRenderer = null;

        #region "Animation Parameters"
        [Header("Animation Parameters")]
        [SerializeField] string walking = "Walking";
        [SerializeField] string grounded = "Grounded";
        [SerializeField] string verticalSpeed = "VerticalSpeed";
        [SerializeField] string climbing = "Climbing";
        [SerializeField] string climbSpeed = "ClimbSpeed";
        [SerializeField] string enterDoor = "EnterDoor";
        [SerializeField] string shoot = "Shoot";
        [SerializeField] string takeDamage = "TakeDamage";
        [SerializeField] string health = "Health";
        #endregion

        Animator anm;

        public float GetLookingDirection { get => spriteRenderer.flipX ? -1 : 1; }

        private void Awake() { anm = GetComponent<Animator>(); }

        public void SetWalkBool(bool isWalking) { anm.SetBool(walking, isWalking); }
        public void SetGrounded(bool isGrounded) { anm.SetBool(grounded, isGrounded); }
        public void SetFallSpeed(float fallSpeed) { anm.SetFloat(verticalSpeed, fallSpeed); }
        public void SetClimbing(bool isClimbing) { anm.SetBool(climbing, isClimbing); }
        public void SetClimbSpeed(float climbAxis) { anm.SetFloat(climbSpeed, climbAxis); }
        public void TriggerEnterDoor() { anm.SetTrigger(enterDoor); }
        public void PlayJumpDustEffect() { InstantiateDust(); }
        public void SetShoot(bool isShooting) { anm.SetBool(shoot, isShooting); }
        public void TriggerTakeDamage(int currentHealth)
        {
            anm.SetInteger(health, currentHealth);
            anm.SetTrigger(takeDamage);
        }

        public void CheckIfFlip(float flipDirection)
        {
            if (spriteRenderer.flipX && flipDirection > 0) { FlipXAxis(); }
            if (!spriteRenderer.flipX && flipDirection < 0) { FlipXAxis(); }
        }

        private void FlipXAxis()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;

            if (componentsToFlip == null) { return; }

            Vector2 flippedScale = Vector2.one;
            flippedScale.x = spriteRenderer.flipX ? -1 : 1;
            componentsToFlip.localScale = flippedScale;
        }

        private void InstantiateDust()
        {
            Transform dust = Instantiate(dustJumpEffect, transform.position, Quaternion.identity).transform;
            dust.localScale = new Vector2(dust.localScale.x * GetLookingDirection, dust.localScale.y);
            Destroy(dust.gameObject, 1f);
        }
    }
}