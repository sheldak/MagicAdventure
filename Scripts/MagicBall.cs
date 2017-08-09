using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\
    public Vector2 Speed;
    private Rigidbody2D my_rigidbody;

    public float delay;

    public GameObject damagedCrate;
    public GameObject extraLife;

    private Player player;


    // ---------- GAME CODE ---------- \\
    void Start ()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
        GetVelocity();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Destroy(gameObject, delay);
	}

	void Update ()
    {
        GetVelocity();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ---------- COLLIDINGS ---------- \\
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("PickUp") || other.gameObject.CompareTag("ExtraLife"))
            Destroy(gameObject);

        if (other.gameObject.CompareTag("Crate"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(damagedCrate, other.gameObject.transform.position, other.gameObject.transform.rotation);
        }

        if (other.gameObject.CompareTag("DamagedCrate"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(extraLife, other.gameObject.transform.position, other.gameObject.transform.rotation);

            player.exp += 1;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            player.exp += 1;
        }
    }


    // ---------- FUNCION ---------- \\
    void GetVelocity()
    {
        my_rigidbody.velocity = Speed;
    }
}
