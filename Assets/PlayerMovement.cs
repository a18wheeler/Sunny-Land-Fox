using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    
    public float runSpeed = 40f;

    public GameObject GameOverText, RestartButton;

    bool jump = false;
    // Update is called once per frame
    private void Start()
    {
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("enemy"))
        {
            GameOverText.SetActive(true);
            RestartButton.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
