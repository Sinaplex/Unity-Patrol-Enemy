//ُSinaPlex
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    
    public enum EnemyState
    {
        Patrol,
        Chase,
        SearchWait
    }

    [Header("References")]
    public Transform player;
    public Transform[] checkpoints;
    public LayerMask wallLayer;

    [Header("Movement")]
    public float patrolSpeed = 1f;
    public float chaseSpeed = 1f;
    public float checkpointDistance = 0.15f;

    [Header("Detection")]
    public float visionRange = 5f;

    [Header("Search")]
    public float waitAfterLost = 3f;

    private Rigidbody2D rb;
    private EnemyState state = EnemyState.Patrol;

    private int currentCheckpoint = 0;
    private float waitTimer = 0f;

    private Vector2 lastDirection = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Enemy needs a Rigidbody2D!");
        }
    }

    private void Update()
    {
        if (player == null || rb == null)
            return;

        bool canSeePlayer = CanSeePlayer();

        switch (state)
        {
            case EnemyState.Patrol:

                if (canSeePlayer)
                {
                    state = EnemyState.Chase;
                }

                break;

            case EnemyState.Chase:

                if (!canSeePlayer)
                {
                    state = EnemyState.SearchWait;
                    waitTimer = waitAfterLost;
                }

                break;

            case EnemyState.SearchWait:

                if (canSeePlayer)
                {
                    state = EnemyState.Chase;
                }
                else
                {
                    waitTimer -= Time.deltaTime;

                    if (waitTimer <= 0f)
                    {
                        state = EnemyState.Patrol;
                    }
                }

                break;
        }
    }

    private void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        switch (state)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.SearchWait:
                // پس از گم کردن بازیکن، ۳ ثانیه می‌ایستد.
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }

    private void Patrol()
    {
        if (checkpoints == null || checkpoints.Length == 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (currentCheckpoint >= checkpoints.Length)
            currentCheckpoint = 0;

        Transform target = checkpoints[currentCheckpoint];

        if (target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveTowards(target.position, patrolSpeed);

        float distance = Vector2.Distance(
            rb.position,
            target.position
        );

        if (distance <= checkpointDistance)
        {
            currentCheckpoint =
                (currentCheckpoint + 1) % checkpoints.Length;
        }
    }

    private void Chase()
    {
        // فقط وقتی دید مستقیم دارد تعقیب می‌کند.
        if (!CanSeePlayer())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        MoveTowards(player.position, chaseSpeed);
    }

    private void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = target - rb.position;
 
        if (direction.sqrMagnitude < 0.0001f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        direction.Normalize();
        lastDirection = direction;

        rb.linearVelocity = direction * speed;
    }

    private bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector2 origin = rb.position;
        Vector2 target = player.position;
        Vector2 direction = target - origin;

        float distance = direction.magnitude;

        if (distance > visionRange)
            return false;

        if (distance <= 0.01f)
            return true;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction.normalized,
            distance,
            wallLayer
        );

        // اگر دیواری بین دشمن و بازیکن نباشد، بازیکن دیده می‌شود.
        return hit.collider == null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        if (checkpoints != null)
        {
            Gizmos.color = Color.cyan;

            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i] == null)
                    continue;

                Gizmos.DrawSphere(checkpoints[i].position, 0.12f);

                if (checkpoints.Length > 1)
                {
                    int next = (i + 1) % checkpoints.Length;

                    if (checkpoints[next] != null)
                    {
                        Gizmos.DrawLine(
                            checkpoints[i].position,
                            checkpoints[next].position
                        );
                    }
                }
            }
        }
    }
}
