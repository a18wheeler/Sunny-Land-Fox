using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    public int dmg = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        print("adsf");
        if(col.isTrigger != true && col.CompareTag("enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
