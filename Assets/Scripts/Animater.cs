using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.Animations
{
    [RequireComponent(typeof(Animator))]
    public class Animater : MonoBehaviour
    {
        [SerializeField] string walkingParameter = "Walking";
        [SerializeField] string jumpParameter = "Jumping";
        [SerializeField] string groundedParameter = "Grounded";
        [SerializeField] GameObject dustJumpEffect = null;

        bool isFacingLeft;
        Animator anm;

        private void Awake() { anm = GetComponent<Animator>(); }

        public void SetWalkBool(bool isWalking) { anm.SetBool(walkingParameter, isWalking); }
        public void SetGrounded(bool isGrounded) { anm.SetBool(groundedParameter, isGrounded); }

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

        public void TriggerJump()
        {
            InstantiateDust();
            anm.SetTrigger(jumpParameter);
        }

        private void InstantiateDust()
        {
            Transform dust = Instantiate(dustJumpEffect, transform.position, Quaternion.identity).transform;
            dust.localScale = transform.localScale;
            Destroy(dust.gameObject, 1f);
        }
    }
}