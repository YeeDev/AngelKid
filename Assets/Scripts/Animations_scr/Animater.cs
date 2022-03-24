using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.Animations
{
    [RequireComponent(typeof(Animator))]
    public class Animater : MonoBehaviour
    {
        [SerializeField] GameObject dustJumpEffect = null;
        [Header("Animation Parameters")]
        [SerializeField] string walking = "Walking";
        [SerializeField] string grounded = "Grounded";
        [SerializeField] string verticalSpeed = "VerticalSpeed";
        [SerializeField] string climbing = "Climbing";
        [SerializeField] string climbSpeed = "ClimbSpeed";
        [SerializeField] string enterDoor = "EnterDoor";

        bool isFacingLeft;
        Animator anm;

        private void Awake() { anm = GetComponent<Animator>(); }

        public void SetWalkBool(bool isWalking) { anm.SetBool(walking, isWalking); }
        public void SetGrounded(bool isGrounded) { anm.SetBool(grounded, isGrounded); }
        public void SetFallSpeed(float fallSpeed) { anm.SetFloat(verticalSpeed, fallSpeed); }
        public void SetClimbing(bool isClimbing) { anm.SetBool(climbing, isClimbing); }
        public void SetClimbSpeed(float climbAxis) { anm.SetFloat(climbSpeed, climbAxis); }
        public void TriggerEnterDoor() { anm.SetTrigger(enterDoor); }
        public void PlayJumpDustEffect() { InstantiateDust(); }

        public void CheckIfFlip(float flipDirection)
        {
            if (isFacingLeft && flipDirection > 0) { Flip(flipDirection); }
            if (!isFacingLeft && flipDirection < 0) { Flip(flipDirection); }
        }

        public void Flip(float flipDirection)
        {
            isFacingLeft = !isFacingLeft;
            Vector3 flippedScale = transform.localScale;
            flippedScale.x = flipDirection;
            transform.localScale = flippedScale;
        }

        private void InstantiateDust()
        {
            Transform dust = Instantiate(dustJumpEffect, transform.position, Quaternion.identity).transform;
            dust.localScale = transform.localScale;
            Destroy(dust.gameObject, 1f);
        }
    }
}