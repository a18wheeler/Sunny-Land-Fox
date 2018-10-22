using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public int playerHealth = 10;
    int damage = 2;

    private void Start()
    {
        print (playerHealth);
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "enemy")
        {
            print("eeee");
        }
    }
}
