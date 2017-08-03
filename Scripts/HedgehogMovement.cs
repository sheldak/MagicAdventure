using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogMovement : MonoBehaviour
{
    public float speed;
    private bool moveDirection;

    private Rigidbody2D my_rigidbody;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;


    void Start ()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
    }
	

	void Update ()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall)
            moveDirection = !moveDirection;

        if (moveDirection)
        {
            my_rigidbody.velocity = new Vector2(speed, my_rigidbody.velocity.y);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else
        {
            my_rigidbody.velocity = new Vector2(-speed, my_rigidbody.velocity.y);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
