using UnityEngine;

namespace AK.MovementStates
{
    public class Climber : MonoBehaviour
    {
        [SerializeField] float climbSpeed = 2f;
        [SerializeField] float ladderTopOffset = 0.2f;
        [SerializeField] LayerMask climbableMask = 0;

        bool isClimbing;
        Collider2D ladder;
        EdgeCollider2D topOfLadder;

        public bool GetIsClimbing { get => isClimbing; }
        public float ClimbSpeed { get => climbSpeed; }

        public bool CheckIfStartClimb(bool touchingLadder, float yAxis)
        {
            if (touchingLadder && Mathf.Abs(yAxis) > 0 && !isClimbing) { return StartClimbing(yAxis); }
            return false;
        }

        public bool StartClimbing(float yAxis)
        {
            ladder = Physics2D.OverlapCircle(transform.position, 1, climbableMask);
            topOfLadder = ladder.transform.GetComponentInChildren<EdgeCollider2D>();

            if (topOfLadder.IsTouchingLayers(LayerMask.GetMask("Player")) && yAxis > 0) { return false; }

            isClimbing = true;
            topOfLadder.enabled = false;
            transform.position = new Vector2(ladder.transform.position.x, transform.position.y);

            return true;
        }

        public bool CheckIfStopClimbing(bool touchingGround, bool goingDown, float bottomPlayerCollider)
        {
            if ((touchingGround && goingDown) || (ladder.bounds.max.y + ladderTopOffset < bottomPlayerCollider))
            {
                StopClimbing();
                return true;
            }

            return false;
        }

        public void StopClimbing()
        {
            isClimbing = false;
            if (topOfLadder != null) { topOfLadder.enabled = true; }
        }
    }
}