using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AK.Animations
{
    public class Animater : MonoBehaviour
    {
        bool isFacingLeft;

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