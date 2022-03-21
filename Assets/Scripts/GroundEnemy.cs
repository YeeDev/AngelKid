using UnityEngine;
using AK.Movements;
using AK.Collisions;
using AK.UnitsStats;

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
        collisioner.SetStats(stats);

        if (movingDirection < 0) { Flip(true); }
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

    private void OnTriggerExit2D(Collider2D collision) { if (collision.CompareTag("Ground") && !ignoresFalls) { Flip(); } }

    private void OnTriggerEnter2D(Collider2D collision) { if (collision.CompareTag("Wall")) { Flip(); } }

    private void Flip(bool ignoreDirection = false)
    {
        movingDirection = ignoreDirection ? movingDirection : movingDirection * -1;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x = movingDirection;
        transform.localScale = flippedScale;
    }
}
