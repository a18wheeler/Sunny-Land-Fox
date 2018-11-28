using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehaviour : MonoBehaviour {

    public int curHealth;
    public Rigidbody2D rb2d;
    private Animator anim;
    private float m_JumpForce = 600f;
    private float jumpCD = 2.0f;
    private float jumpTimer = 0f;
    private bool isJumping;
    private bool isLanding;
    private bool landed;

    private bool m_FacingLeft = true;  // For determining which way the player is currently facing.

    private Transform target;
    private Vector2 goal;
    public int targetHP;

    // Use this for initialization
    void Start()
    {
        //player becomes target
        isJumping = false;
        isLanding = false;
        landed = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Perceive();

        Execute();

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingLeft = !m_FacingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        //gameObject.GetComponent<Animation>().
    }

    public void Perceive()
    {

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            targetHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHurt>().currHealth;
        }

        if (rb2d.velocity.y < 0)
        {
            isJumping = false;
            isLanding = true;
        }
        else if (rb2d.velocity.y == 0)
        {
            isLanding = false;
            landed = true;
        }

        if (landed)
        {
            if (jumpTimer > 0)
            {
                jumpTimer -= Time.deltaTime;
            }
        }
        anim.SetBool("isJumping", isJumping);

        anim.SetBool("isLanding", isLanding);

        if ((transform.position.x - goal.x) < 0 && m_FacingLeft)
        {
            // ... flip the character.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if ((transform.position.x - goal.x) > 0 && !m_FacingLeft)
        {
            // ... flip the character.
            Flip();
        }


    }

    public int Think()
    {
        if (!isJumping && jumpTimer <= 1)
        {
            goal = target.position;
            return 1;
        }
        return 3;
    }

    public void Execute()
    {
        int T = Think();
        switch (T)
        {
            case 1:
                Attack();
                break;
            case 2:
                Flee();
                break;
            case 3:
                Idle();
                break;
            default:
                Idle();
                break;
        }
    }
    public void Idle()
    {

    }
    public void Attack()
    {
        float x = goal.x - transform.position.x;
        float y = m_JumpForce;
        Jump(x, y);
    }

    public void Flee()
    {

    }

    public void Jump(float x, float y)
    {
        landed = false;
        isJumping = true;
        jumpTimer = jumpCD;
        rb2d.AddForce(new Vector2(x*50, y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}

