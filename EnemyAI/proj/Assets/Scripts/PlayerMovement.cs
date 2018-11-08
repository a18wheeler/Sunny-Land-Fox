using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb2d;

    float horizontalMove = 0f;
    
    public float runSpeed = 40f;


    

    bool jump = false;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update () {

       

       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

       animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
			animator.SetBool("IsJumping", true);
        }
       
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}
	
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        jump = false;
    }

   
    /* Knock back for later use. 
    public IEnumerator Knockback(float knockDur, float knockBackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        
        while(knockDur > timer)
        {
            timer += Time.deltaTime;

            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockBackPwr, transform.position.z));

        }
        yield return 0;
    }*/
}
