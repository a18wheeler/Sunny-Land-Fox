using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {


    public float speed;

    private bool m_FacingLeft = true;  // For determining which way the player is currently facing.

    private Transform target;

	// Use this for initialization
	void Start () {
        //player becomes target
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

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
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
}
