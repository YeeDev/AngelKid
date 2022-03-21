using UnityEngine;
using AK.MovementStates;
using AK.Movements;
using AK.Core;
using AK.UnitsStats;
using AK.Collisions;

namespace AK.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Collisioner))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] Collider2D feetcol = null;
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0; 

        float xAxis;
        Stats stats;
        Mover mover;
        Climber climber;
        Collisioner collisioner;
        CameraViewer cameraViewer;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            cameraViewer = Camera.main.GetComponent<CameraViewer>();
            stats = GetComponent<Stats>();

            collisioner = GetComponent<Collisioner>();
            collisioner.SetStats(stats);

            climber = GetComponent<Climber>();
            climber.SetMove(mover);
        }

        private void Update()
        {
            if (stats.IsUnitDeath)
            {
                mover.StopRigidbody();
                return;
            }

            ReadWalkInput();
            ReadJumpInput();
            ControlClimbState();
        }

        private void FixedUpdate()
        {
            cameraViewer.IsPlayerGrounded = feetcol.IsTouchingLayers(LayerMask.GetMask("Jumpable")) || climber.GetIsClimbing;
        }

        private void ReadWalkInput()
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            if (climber.GetIsClimbing && Mathf.Abs(xAxis) > Mathf.Epsilon) { climber.StopClimbing(); }

            mover.Move(xAxis, Input.GetAxisRaw("Vertical"), climber.GetIsClimbing);
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && feetcol.IsTouchingLayers(jumpableMask))
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
                climber.CheckIfStartClimb(feetcol.IsTouchingLayers(climbableMask), Input.GetAxisRaw("Vertical"), climbableMask);
            }
        }

        private void StopClimb()
        {
            if (climber.GetIsClimbing)
            {
                bool touchingGround = feetcol.IsTouchingLayers(LayerMask.GetMask("Ground"));
                climber.CheckIfStopClimbing(touchingGround, Input.GetAxisRaw("Vertical") < 0, feetcol.bounds.min.y);
            }
        }
    }
}