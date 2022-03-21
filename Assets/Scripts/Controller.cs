using UnityEngine;
using AK.MovementStates;
using AK.Movements;
using AK.Core;

namespace AK.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Collider2D))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0;

        float xAxis;
        Mover mover;
        Climber climber;
        Collider2D col;
        CameraViewer cameraViewer;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
            mover = GetComponent<Mover>();
            cameraViewer = Camera.main.GetComponent<CameraViewer>();

            climber = GetComponent<Climber>();
            climber.SetMove(mover);
        }

        private void Update()
        {
            ReadWalkInput();
            ReadJumpInput();
            ControlClimbState();
        }

        private void FixedUpdate()
        {
            cameraViewer.IsPlayerGrounded = col.IsTouchingLayers(LayerMask.GetMask("Ground")) || climber.GetIsClimbing;
        }

        private void ReadWalkInput()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            if (climber.GetIsClimbing && Mathf.Abs(xAxis) > Mathf.Epsilon) { climber.StopClimbing(); }

            mover.Move(xAxis, Input.GetAxisRaw("Vertical"), climber.GetIsClimbing);
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && col.IsTouchingLayers(jumpableMask))
            {
                climber.StopClimbing();
                mover.Jump();
            }
            if (Input.GetButtonUp("Jump")) { mover.HaltJump(); }
        }

        private void ControlClimbState()
        {
            StartClimb();
            StopClimb();
        }

        private void StartClimb()
        {
            if (Input.GetButton("Vertical"))
            {
                climber.CheckIfStartClimb(col.IsTouchingLayers(climbableMask), Input.GetAxisRaw("Vertical"), climbableMask);
            }
        }

        private void StopClimb()
        {
            if (climber.GetIsClimbing)
            {
                bool touchingGround = col.IsTouchingLayers(LayerMask.GetMask("Ground"));
                climber.CheckIfStopClimbing(touchingGround, Input.GetAxisRaw("Vertical") < 0, col.bounds.min.y);
            }
        }
    }
}