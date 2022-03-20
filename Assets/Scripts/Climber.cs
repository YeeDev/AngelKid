using UnityEngine;
using AK.Movements;

namespace AK.MovementStates
{
    public class Climber : MonoBehaviour
    {
        [SerializeField] float climbSpeed = 2f;
        [SerializeField] float ladderTopOffset = 0.2f;
        [SerializeField] LayerMask climbableMask = 0;

        bool isClimbing;
        Mover mover;
        Collider2D ladder;
        EdgeCollider2D topOfLadder;

        
        public bool GetIsClimbing { get => isClimbing; }
        public float ClimbSpeed { get => climbSpeed; }
        public void SetMove(Mover mover) { this.mover = mover; }

        public void CheckIfStartClimb(bool touchingLadder, float yAxis)
        {
            if (touchingLadder && Mathf.Abs(yAxis) > 0 && !isClimbing) { StartClimbing(yAxis); }
        }

        public void StartClimbing(float yAxis)
        {
            ladder = Physics2D.OverlapCircle(transform.position, 1, climbableMask);
            topOfLadder = ladder.transform.GetComponentInChildren<EdgeCollider2D>();

            if (topOfLadder.IsTouchingLayers(LayerMask.GetMask("Player")) && yAxis > 0) { return; }

            isClimbing = true;
            topOfLadder.enabled = false;
            transform.position = new Vector2(ladder.transform.position.x, transform.position.y);

            mover.SetGravity(false, 0);
            mover.StopRigidbody();
        }

        public void CheckIfStopClimbing(bool touchingGround, bool goingDown, float bottomPlayerCollider)
        {
            if ((touchingGround && goingDown) || (ladder.bounds.max.y + ladderTopOffset < bottomPlayerCollider))
            {
                StopClimbing();
            }
        }

        public void StopClimbing()
        {
            isClimbing = false;
            mover.SetGravity(true);
            if (topOfLadder != null) { topOfLadder.enabled = true; }
        }
    }
}