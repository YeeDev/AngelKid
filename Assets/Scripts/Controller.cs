using UnityEngine;
using AK.Movements;
using AK.MovementStates;

namespace AK.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Collider2D))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0;

        Mover mover;
        Climber climber;
        Collider2D col;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            climber = GetComponent<Climber>();
            col = GetComponent<Collider2D>();
        }

        private void Update()
        {
            ReadWalkInput();
            ReadJumpInput();
            ReadClimbInput();
        }

        private void ReadWalkInput()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            if (climber.GetIsClimbing && Mathf.Abs(xAxis) > Mathf.Epsilon) { PreventClimb(); }

            mover.Move(xAxis, Input.GetAxisRaw("Vertical"));
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(jumpableMask))
            {
                PreventClimb();
                mover.Jump();
            }
            if (Input.GetButtonUp("Jump")) { mover.HaltJump(); }
        }

        private void ReadClimbInput()
        {
            StartClimb();
            StopClimb();
        }

        private void StartClimb()
        {
            if (Input.GetButton("Vertical"))
            {
                if (climber.CheckIfStartClimb(col.IsTouchingLayers(climbableMask), Input.GetAxisRaw("Vertical")))
                {
                    mover.SetGravity(false, 0);
                    mover.StopRigidbody();
                }
            }
        }

        private void StopClimb()
        {
            if (climber.GetIsClimbing)
            {
                bool touchingGround = col.IsTouchingLayers(LayerMask.GetMask("Ground"));
                if (climber.CheckIfStopClimbing(touchingGround, Input.GetAxisRaw("Vertical") < 0, col.bounds.min.y))
                {
                    PreventClimb();
                }
            }
        }

        private void PreventClimb()
        {
            mover.SetGravity(true);
            climber.StopClimbing();
        }
    }
}