using UnityEngine;
using System.Collections;

namespace AK.Animations
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Animater : MonoBehaviour
    {
        [SerializeField] float shootTime = 0.5f;
        [SerializeField] GameObject dustJumpEffect = null;
        [SerializeField] Transform componentsToFlip = null;

        #region "Animation Parameters"
        [Header("Animation Parameters")]
        [SerializeField] string walking = "Walking";
        [SerializeField] string grounded = "Grounded";
        [SerializeField] string verticalSpeed = "VerticalSpeed";
        [SerializeField] string climbing = "Climbing";
        [SerializeField] string climbSpeed = "ClimbSpeed";
        [SerializeField] string enterDoor = "EnterDoor";

        #endregion

        float shootLayerTimer;
        Coroutine shootLayerCoroutine;
        SpriteRenderer rend;
        Animator anm;

        public float GetLookingDirection { get => rend.flipX ? -1 : 1; }

        private void Awake()
        {
            anm = GetComponent<Animator>();
            rend = GetComponent<SpriteRenderer>();
        }

        public void SetWalkBool(bool isWalking) { anm.SetBool(walking, isWalking); }
        public void SetGrounded(bool isGrounded) { anm.SetBool(grounded, isGrounded); }
        public void SetFallSpeed(float fallSpeed) { anm.SetFloat(verticalSpeed, fallSpeed); }
        public void SetClimbing(bool isClimbing) { anm.SetBool(climbing, isClimbing); }
        public void SetClimbSpeed(float climbAxis) { anm.SetFloat(climbSpeed, climbAxis); }
        public void TriggerEnterDoor() { anm.SetTrigger(enterDoor); }
        public void PlayJumpDustEffect() { InstantiateDust(); }

        public void CheckIfFlip(float flipDirection)
        {
            if (rend.flipX && flipDirection > 0) { FlipXAxis(); }
            if (!rend.flipX && flipDirection < 0) { FlipXAxis(); }
        }

        public void SetShootLayerWeight()
        {
            anm.SetLayerWeight(1, 1);
            shootLayerTimer = shootTime;
            if (shootLayerCoroutine == null) { shootLayerCoroutine = StartCoroutine(ReturnToNormalLayer()); }
        }

        IEnumerator ReturnToNormalLayer()
        {
            while (shootLayerTimer > 0)
            {
                shootLayerTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            anm.SetLayerWeight(1, 0);
            shootLayerCoroutine = null;
        }

        private void FlipXAxis()
        {
            rend.flipX = !rend.flipX;

            if (componentsToFlip == null) { return; }

            Vector2 flippedScale = Vector2.one;
            flippedScale.x = rend.flipX ? -1 : 1;
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