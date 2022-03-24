using UnityEngine;
using AK.MovementStates;
using AK.Movements;
using AK.Core;
using AK.UnitsStats;
using AK.Collisions;
using AK.Attacks;
using AK.Animations;

namespace AK.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Collisioner))]
    [RequireComponent(typeof(Shooter))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] Collider2D feetcol = null;
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0;

        bool isGrounded;
        float xAxis;
        Stats stats;
        Mover mover;
        Climber climber;
        Collisioner collisioner;
        CameraViewer cameraViewer;
        Shooter shooter;
        Animater animater;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            cameraViewer = Camera.main.GetComponent<CameraViewer>();
            shooter = GetComponent<Shooter>();
            stats = GetComponent<Stats>();
            animater = GetComponent<Animater>();

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
            ReadShootInput();
            shooter.AddToTimer();
        }

        private void FixedUpdate() { CheckGroundedState(); }

        private void ReadWalkInput()
        {
            xAxis = Input.GetAxisRaw("Horizontal");

            mover.Move(xAxis, Input.GetAxisRaw("Vertical"), climber.GetIsClimbing);
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown("Jump") && (isGrounded || climber.GetIsClimbing))
            {
                climber.StopClimbing();
                mover.Jump(isGrounded);
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
                climber.CheckIfStopClimbing(isGrounded, Input.GetAxisRaw("Vertical") < 0, feetcol.bounds.min.y);
            }
        }

        private void ReadShootInput() { if (Input.GetButtonDown("Fire")) { shooter.Shoot(); } }

        private void CheckGroundedState()
        {
            isGrounded = feetcol.IsTouchingLayers(jumpableMask);

            cameraViewer.IsPlayerGrounded = isGrounded || climber.GetIsClimbing;
            animater.SetGrounded(isGrounded || climber.GetIsClimbing);
            animater.SetFallSpeed(mover.GetYRigidbodySpeed);
        }
    }
}