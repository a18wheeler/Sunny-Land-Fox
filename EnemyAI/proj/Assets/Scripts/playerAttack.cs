using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0f;
    private float attackCd = 0.4f;

    public Collider2D attackTrigger;
    public Collider2D dAir;

    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
        dAir.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("z") && !attacking && Input.GetKey(KeyCode.DownArrow))
        {
            attacking = true;
            attackTimer = attackCd;

            dAir.enabled = true;
        }
        else if (Input.GetKeyDown("z") && !attacking)
        {
            attacking = true;
            attackTimer = attackCd;

            attackTrigger.enabled = true;

        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
                dAir.enabled = false;
            }
        }
        anim.SetBool("Attacking", attacking);
    }
}
