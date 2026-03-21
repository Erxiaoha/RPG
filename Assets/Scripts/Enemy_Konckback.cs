using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Konckback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }
    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knocback);
        Vector2 direction = (transform.position - playerTransform.position).normalized;  // 玩家指向敌人
        rb.velocity = direction * knockbackForce;
        StartCoroutine(StunTimer(knockbackTime, stunTime));
        Debug.Log("击退已应用");
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
