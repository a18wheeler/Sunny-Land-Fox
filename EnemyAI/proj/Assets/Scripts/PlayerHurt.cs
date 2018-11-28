using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour {
    public int maxHealth = 3;
    public float invulnTimer = 0f;
    public float invuln = 1.0f;
    public int currHealth;
    public GameObject GameOverText, RestartButton;

    // Use this for initialization
    void Start () {
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);
        currHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (currHealth <= 0)
        {
            Gameover();
        }

        if (invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.layer = 8;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.layer = 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("enemy") && invulnTimer <= 0)
        {
            currHealth--;
            invulnTimer = invuln;
        }
    }

    private void Gameover()
    {
        GameOverText.SetActive(true);
        RestartButton.SetActive(true);
        gameObject.SetActive(false);
    }
    
}
