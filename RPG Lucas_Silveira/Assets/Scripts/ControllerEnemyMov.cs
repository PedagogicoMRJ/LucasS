using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]


public class ControllerEnemyMov : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 moveTimeRange = new Vector2(1.5f, 3f);
    [SerializeField] private Vector2 idleTimeRange = new Vector2(0.5f, 1.2f);
    [SerializeField] private float patrolRadius = 5f;
    [SerializeField] private Transform center;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float obstacleCheckDistance = 0.6f;

    [Header("Parâmetros do Animator")]
    [SerializeField] private string horizontalParam = "Horizontal";
    [SerializeField] private string verticalParam = "Vertical";
    [SerializeField] private string magnitudeParam = "Magnitude";

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 currentDir = Vector2.zero;
    private Vector3 origin;
    private float stateEndTime;
    private bool isMoving;
    private bool inBattle;

    private static readonly Vector2[] fourDirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        origin = center ? center.position : transform.position;
        ScheduleNextState(false); // começa parado
    }

    private void Update()
    {
        if (inBattle)
        {
            StopNow();
            return;
        }

        if (Time.time >= stateEndTime)
        {
            isMoving = !isMoving;
            ScheduleNextState(isMoving);
            if (isMoving)
                currentDir = ChooseDirection();
            else
                currentDir = Vector2.zero;
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (inBattle || !isMoving)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (HitObstacle(currentDir) || OutsideRadiusNextStep(currentDir))
        {
            currentDir = ChooseDirection();
        }

        rb.velocity = currentDir * speed;
    }

    private void ScheduleNextState(bool moving)
    {
        float dur = moving
            ? Random.Range(moveTimeRange.x, moveTimeRange.y)
            : Random.Range(idleTimeRange.x, idleTimeRange.y);

        stateEndTime = Time.time + dur;
    }

    private Vector2 ChooseDirection()
    {
        // Tenta algumas vezes achar uma direção válida
        for (int i = 0; i < 8; i++)
        {
            Vector2 candidate = fourDirs[Random.Range(0, fourDirs.Length)];
            if (!HitObstacle(candidate) && !OutsideRadiusNextStep(candidate))
                return candidate;
        }
        return Vector2.zero; // Sem saída -> fica parado
    }

    private bool HitObstacle(Vector2 dir)
    {
        if (obstacleMask.value == 0) return false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, obstacleCheckDistance, obstacleMask);
        return hit.collider != null;
    }

    private bool OutsideRadiusNextStep(Vector2 dir)
    {
        if (patrolRadius <= 0f) return false;
        Vector3 centerPos = center ? center.position : origin;
        Vector3 next = transform.position + (Vector3)(dir * speed * Time.fixedDeltaTime * 8f);
        return Vector3.Distance(centerPos, next) > patrolRadius;
    }

    private void UpdateAnimator()
    {
        if (!anim) return;
        Vector2 v = rb ? rb.velocity : Vector2.zero;
        float mag = v.magnitude;

        // Para animação direcional 4-way
        Vector2 dirForAnim = mag > 0.01f ? currentDir : Vector2.zero;
        anim.SetFloat(horizontalParam, dirForAnim.x);
        anim.SetFloat(verticalParam, dirForAnim.y);
        anim.SetFloat(magnitudeParam, mag);
    }

    private void StopNow()
    {
        rb.velocity = Vector2.zero;
        if (anim)
        {
            anim.SetFloat(horizontalParam, 0f);
            anim.SetFloat(verticalParam, 0f);
            anim.SetFloat(magnitudeParam, 0f);
        }
    }

    public void SetInBattle(bool value)
    {
        inBattle = value;
        if (value) StopNow();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 c = center ? center.position : transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(c, patrolRadius);
    }
}

