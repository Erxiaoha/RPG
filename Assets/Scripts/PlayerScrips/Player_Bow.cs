using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    private Vector2 aimDiretion = Vector2.right;

    public PlayerMovement playerMovement;

    public float shootCooldown = .5f;
    private float shootTimer;

    public Animator anim;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
        }
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0); //把第0层的权重设置为0
        anim.SetLayerWeight(1, 1); //把第1层的权重设置为1
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1); //把第0层的权重设置为1
        anim.SetLayerWeight(1, 0); //把第1层的权重设置为0
    }

    private  void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //A D 键
        float vertical = Input.GetAxisRaw("Vertical"); //W S 键

        if (horizontal != 0 || vertical != 0)
        {
            aimDiretion = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDiretion.x);
            anim.SetFloat("aimY", aimDiretion.y);
        }
    }

    public void Shoot()
    {
        if(shootTimer <= 0)
        {
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDiretion;
            shootTimer = shootCooldown;
            anim.SetBool("isShooting", false);
            playerMovement.isShooting = false;
        }

    }
}
