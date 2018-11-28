using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehavior : MonoBehaviour {


    public float speed;
    public int curHealth;

    private bool m_FacingLeft = true;  // For determining which way the player is currently facing.

    private Transform target;
    private Vector2 goal;
    public int targetHP;
    public float range = 2;
    private float hoverPos;

    private bool isFlee = false;
    private float isAttack = 0;

    // Use this for initialization
    void Start () {
        //player becomes target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        hoverPos = target.position.y + 2;
	}
	
	// Update is called once per frame
	void Update () {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isFlee = true;
        }
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

        hoverPos = target.position.y + 2;

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
        System.Random rng = new System.Random();

        int r = (rng.Next(0,200*(targetHP)));

        if(r == 8)
        {
            isAttack = 2;
        }

        if (isFlee)
        {
            isAttack = 0;
            goal = -target.position;
            return 2;
        }
        else if(isAttack > 0)
        {
            isAttack-=Time.deltaTime;
            goal = target.position;
            return 1;
        }
        else
        {
            isAttack = 0;
            return 3;
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
                Stalk();
                break;
            default:
                Attack();
                break;
        }
    }

    public void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }

    public void Stalk()
    {
       
        if (transform.position.x == goal.x)
        {
            range = range * -1;
        }
        
        goal = new Vector2(
        target.position.x - range,
        hoverPos
     );
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime); 
    }
    
    public void Flee()
    {
        if (transform.position.y > hoverPos)
        {
            goal = target.position;
            isFlee = false;
        }

        else
        {         
            transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        }

    }
}
