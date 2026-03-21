using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    public float attackRange = 2;
    public float attackCooldown = 1; //攻击冷却时间
    public float playerDetectRange = 5; // 玩家检测距离
    public Transform detectionPoint;  // 检测点
    public LayerMask playerLayer;

    private float attackCooldownTimer;  // 攻击冷却计时器
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    private int facingDirection = -1;
    private EnemyState enemyState, newState;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if(enemyState != EnemyState.Knocback)
        {
            CheckForPlayer();
            if (attackCooldownTimer > 0)
            {
                attackCooldownTimer -= Time.deltaTime;

            }
            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Chase()
    {

        if (player.position.x > transform.position.x && facingDirection == 1 ||
                player.position.x < transform.position.x && facingDirection == -1
                )
        {
            Flip();
        }
        // 玩家的位置减去敌人的位置能获得敌人到玩家的方向向量 normalized是为了让大小模长归一
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking); // 每次攻击结束的末尾会触发事件切换成idle，在动画里有添加
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeState(EnemyState newState)
    {
        if(enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if(enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}


public enum EnemyState {
    Idle,
    Chasing,
    Attacking,
    Knocback,
}
