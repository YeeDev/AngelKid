using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.Animations
{
    [RequireComponent(typeof(Animator))]
    public class Animater : MonoBehaviour
    {
        [SerializeField] string walkingParameter = "Walking";

        bool isFacingLeft;
        Animator anm;

        private void Awake() { anm = GetComponent<Animator>(); }

        public void SetWalkBool(bool isWalking) { anm.SetBool(walkingParameter, isWalking); }

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
    }
}