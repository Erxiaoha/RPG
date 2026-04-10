using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Idle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.Play("Idle");
        }
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        if(anim != null)
        { 
            anim.Play("Idle");
        }
    }
}
