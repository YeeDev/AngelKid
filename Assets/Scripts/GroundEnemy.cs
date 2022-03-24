using UnityEngine;
using AK.Movements;
using AK.Collisions;
using AK.UnitsStats;
using AK.Animations;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Collisioner))]
public class GroundEnemy : MonoBehaviour
{
    [SerializeField] bool ignoresFalls = false;
    [Range(-1, 1)][SerializeField] float movingDirection = 1f;
    [SerializeField] Collider2D mainCollider = null;

    Mover mover;
    Stats stats;
    Collisioner collisioner;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        stats = GetComponent<Stats>();

        collisioner = GetComponent<Collisioner>();
        collisioner.InitializeCollisioner(stats, null);
    }

    private void Update()
    {
        if(stats.IsUnitDeath) { Destroy(gameObject); }

        MoveBehaviour();
    }

    private void MoveBehaviour()
    {
        if (ignoresFalls && !mainCollider.IsTouchingLayers(LayerMask.GetMask("Jumpable")))
        {
            mover.StopRigidbody(true);
            return;
        }

        mover.Move(movingDirection, 0, false);
    }

    private void OnTriggerExit2D(Collider2D collision) { if (collision.CompareTag("Ground") && !ignoresFalls) { FlipMovingDirection(); } }

    private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Wall")) { FlipMovingDirection(); } }

    private void FlipMovingDirection() { movingDirection *= -1; }
}
