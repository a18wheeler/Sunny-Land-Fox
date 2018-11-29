using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumBehaviour : MonoBehaviour
{

    public int curHealth;
    public Rigidbody2D rb2d;
    private Animator anim;
    public float speed;
    public float range = 2.0f;
    public float visionRangeX = 2.0f;
    public float visionRangeY = 2.0f;
    public float chargeMult = 2.0f;
    public float chargeThreshold;
    public float chargeEnergy = 0f;
    public bool isCharge = false;
    public float chargeTime = 2.0f;
    public float currCharge = 0f;

    private float m_JumpForce = 100f;
    private float jumpCD = 2.0f;
    private float jumpTimer = 0f;
    private bool isJumping;
    private bool isLanding;
    private bool landed;

    private float isFlee = 0;
    private bool isAttack = false;
    private bool targetSpotted = false;
    private float startPositionX;

    private bool m_FacingLeft = true;  // For determining which way the enemy is currently facing.

    private Transform target;
    private Vector2 goal;
    private Vector2 vision;
    public int targetHP;

    // Use this for initialization
    void Start()
    {
        //player becomes target
        chargeThreshold = 15;
        isJumping = false;
        isLanding = false;
        landed = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        rb2d.freezeRotation = true;
        vision = new Vector2(
            transform.position.x + visionRangeX,
            transform.position.y + visionRangeY
     );
        startPositionX = transform.position.x;
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
        //check if dead
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }

        //get player hp
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            targetHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHurt>().currHealth;
        }

        //Vision range
        vision = new Vector2(
            transform.position.x + visionRangeX,
            transform.position.y + visionRangeY
         );

        //Check if player is in vision
        if((vision.x <= target.position.x && transform.position.x <= transform.position.x) || 
            (transform.position.x <= target.position.x && target.position.x <= vision.x))
        {
            targetSpotted = true;
        }
        //charge code
        if (chargeEnergy < chargeThreshold && !isCharge)
        {
            chargeEnergy += Time.deltaTime;
        }
        else if(chargeEnergy > chargeThreshold && !isCharge && isAttack)
        {
            isCharge = true;
            currCharge = chargeTime;
            chargeEnergy = 0;
        }
        print(chargeEnergy);
        if (isCharge)
        {
            currCharge -= Time.deltaTime;
        }
        if(currCharge < 0)
        {
            isCharge = false;
        }
        //////////////////
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

        if ((transform.position.x - goal.x) < 0 && m_FacingLeft)
        {
            // ... flip the character.
            Flip();
            visionRangeX = 2;
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if ((transform.position.x - goal.x) > 0 && !m_FacingLeft)
        {
            // ... flip the character.
            Flip();
            visionRangeX = -2;
        }


    }

    public int Think()
    {
        if (!targetSpotted)
        {
            isAttack = false;
            return 3;
        }
        else if (isFlee > 0)
        {
            isAttack = false;
            goal = new Vector2(-target.position.x, 8);
            return 2;
        }
        else if(isAttack && isCharge)
        {
            goal = target.position;
            return 4;
        }
        else
        {
            isAttack = true;
            goal = target.position;
            return 1;
        }
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
                Patrol();
                break;
            case 4:
                Charge();
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
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }

    public void Charge()
    {
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime*chargeMult);
    }

    public void Flee()
    {
        isFlee -= Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }

    public void Patrol()
    {

        if (transform.position.x == goal.x)
        {
            range = range * -1;
        }

        goal = new Vector2(
        startPositionX - range,
        transform.position.y
     );
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }

    public void Jump(float x, float y)
    {
        landed = false;
        isJumping = true;
        jumpTimer = jumpCD;
        rb2d.AddForce(new Vector2(x * 50, y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            
            float x = goal.x - transform.position.x;
            float y = m_JumpForce;
            Jump(x, y);
            isFlee = 3;
            
        }
    }
}

