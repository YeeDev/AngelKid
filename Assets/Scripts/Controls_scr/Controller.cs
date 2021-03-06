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
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(Climber))]
    [RequireComponent(typeof(Animater))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] LayerMask jumpableMask = 0;
        [SerializeField] LayerMask climbableMask = 0;
        [SerializeField] LayerMask doorMask = 0;
        [SerializeField] ControlSettingsSO controlSettings = null;

        bool isGrounded;
        bool controlIsDisabled;
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

        //Called In Animation
        private void TakeDamage()
        {
            mover.PushInDirection(collisioner.GetPusher.position);
            controlIsDisabled = true;
        }

        private void EnableControl()
        {
            controlIsDisabled = false;
            mover.SetGravity(true);
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

        private void InitializeScripts() { climber.InitializeClimber(mover); }

        private void Update()
        {
            if (controlIsDisabled || stats.IsUnitDeath) { return; }

            ReadWalkInput();
            ReadJumpInput();
            ControlClimbState();
            ReadShootInput();
            ReadEnterDoorInput();
        }

        private void FixedUpdate() { CheckGroundedState(); }

        private void ReadWalkInput()
        {
            xAxis = Input.GetAxisRaw(controlSettings.GetHorizontal);

            mover.Move(xAxis, Input.GetAxisRaw(controlSettings.GetVertical), climber.GetIsClimbing);
        }

        private void ReadJumpInput()
        {
            if (Input.GetButtonDown(controlSettings.GetJump) && (isGrounded || climber.GetIsClimbing))
            {
                climber.StopClimbing();
                mover.Jump(isGrounded);
            }
            if (Input.GetButtonUp(controlSettings.GetJump)) { mover.HaltJump(); }
        }

        private void ControlClimbState()
        {
            StartClimb();
            StopClimb();
        }

        private void StartClimb()
        {
            if (Input.GetButton(controlSettings.GetVertical))
            {
                climber.CheckIfStartClimb(collisioner.IsOnLadder(climbableMask),
                    Input.GetAxisRaw(controlSettings.GetVertical), climbableMask);
            }
        }

        private void StopClimb()
        {
            if (climber.GetIsClimbing)
            {
                climber.CheckIfStopClimbing(isGrounded,
                    Input.GetAxisRaw(controlSettings.GetVertical) < 0, collisioner.GetColliderMinYBound);
            }
        }

        private void ReadShootInput()
        {
            if (!climber.GetIsClimbing) { animater.SetShoot(Input.GetButton(controlSettings.GetFire)); }
        }

        private void ReadEnterDoorInput()
        {
            if (Input.GetAxisRaw(controlSettings.GetVertical) > Mathf.Epsilon && collisioner.IsTouchingGround(doorMask))
            {
                LoadBehaviour();
                animater.TriggerEnterDoor();
            }
        }

        private void LoadBehaviour()
        {
            StartCoroutine(levelLoader.LoadLevel(stats.IsUnitDeath));
            mover.FreezePosition();
            enabled = false;
        }

        private void CheckGroundedState()
        {
            isGrounded = collisioner.IsTouchingGround(jumpableMask);

            cameraViewer.IsPlayerGrounded = isGrounded || climber.GetIsClimbing;
            animater.SetGrounded(isGrounded || climber.GetIsClimbing);
            animater.SetFallSpeed(mover.GetYRigidbodySpeed);
        }
    }
}