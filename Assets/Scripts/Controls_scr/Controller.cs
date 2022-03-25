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
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0;
        [SerializeField] LayerMask doorMask = 0;

        bool isGrounded;
        float xAxis;

        Stats stats;
        Mover mover;
        Climber climber;
        Collisioner collisioner;
        CameraViewer cameraViewer;
        Animater animater;
        LevelLoader levelLoader;

        private void Awake()
        {
            GetAndSetScripts();
            InitializeScripts();
        }

        private void GetAndSetScripts()
        {
            mover = GetComponent<Mover>();
            stats = GetComponent<Stats>();
            climber = GetComponent<Climber>();
            animater = GetComponent<Animater>();
            collisioner = GetComponent<Collisioner>();
            cameraViewer = Camera.main.GetComponent<CameraViewer>();
            levelLoader = GameObject.FindWithTag("GameController").GetComponent<LevelLoader>();
        }

        private void InitializeScripts()
        {
            collisioner.InitializeCollisioner(stats);
            climber.InitializeClimber(mover);
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
            ReadEnterDoorInput();
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
                climber.CheckIfStartClimb(collisioner.IsTouchingLayer(climbableMask), Input.GetAxisRaw("Vertical"), climbableMask);
            }
        }

        private void StopClimb()
        {
            if (climber.GetIsClimbing)
            {
                climber.CheckIfStopClimbing(isGrounded, Input.GetAxisRaw("Vertical") < 0, collisioner.GetColliderMinYBound);
            }
        }

        private void ReadShootInput()
        {
            if (Input.GetButtonDown("Fire") && !climber.GetIsClimbing) { animater.TriggerShoot(); }
        }

        private void ReadEnterDoorInput()
        {
            if (Input.GetAxisRaw("Vertical") > Mathf.Epsilon && collisioner.IsTouchingLayer(doorMask))
            {
                StartCoroutine(levelLoader.LoadLevel());
                animater.TriggerEnterDoor();
                mover.StopRigidbody();
                enabled = false;
            }
        }

        private void CheckGroundedState()
        {
            isGrounded = collisioner.IsTouchingLayer(jumpableMask);

            cameraViewer.IsPlayerGrounded = isGrounded || climber.GetIsClimbing;
            animater.SetGrounded(isGrounded || climber.GetIsClimbing);
            animater.SetFallSpeed(mover.GetYRigidbodySpeed);
        }
    }
}