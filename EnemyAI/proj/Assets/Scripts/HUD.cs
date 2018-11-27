using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] Heart;
    public Image HeartsUI;

    private PlayerHurt player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHurt>();

    }

    private void Update()
    {
        HeartsUI.sprite = Heart[player.currHealth];
    }

}
