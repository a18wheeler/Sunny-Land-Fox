using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehavior : MonoBehaviour {


    public float speed;
    public int curHealth;
    public Rigidbody2D rb2d;

    private bool m_FacingLeft = true;  // For determining which way the player is currently facing.

    private Transform target;
    private float hoverPos; 

	// Use this for initialization
	void Start () {
        //player becomes target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        hoverPos = target.position.y + 2;
	}
	
	// Update is called once per frame
	void Update () {
        hoverPos = target.position.y + 2;

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }

        if ((transform.position.x - target.position.x) < 0 && m_FacingLeft)
        {
            // ... flip the character.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if ((transform.position.x - target.position.x) > 0 && !m_FacingLeft)
        {
            // ... flip the character.
            Flip();
        }

        //change velocity to move toward player
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

    private bool isFlee = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isFlee = true;
        }
    }

    public int Think()
    {
        if (isFlee)
        {
            return 2;
        }
        else
            return 1;
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
            default:
                Stalk();
                break;
        }
    }

    public void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Stalk()
    {
        float range = 2;
               
        float Direction = Mathf.Sign(target.position.x - transform.position.x + range);
        Vector2 MovePos = new Vector2(
        transform.position.x + Direction,
        hoverPos
     );
        transform.position = Vector2.MoveTowards(transform.position, MovePos, speed * Time.deltaTime); 
    }
    
    public void Flee()
    {
        if (transform.position.y > hoverPos)
        {
            isFlee = false;
        }
        else
        {
 
            transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
        }

    }
}
