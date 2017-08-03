using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\
    public Sprite[] healthSprites;
    public Image healthUI;

    private Player player;


    // ---------- GAME CODE ---------- \\
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Update ()
    {
        healthUI.sprite = healthSprites[player.health];
	}
}
