using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\
    public float move_speed;
    public float jump_height;

    public Transform ground_check_1;
    public Transform ground_check_2;
    public LayerMask what_is_ground;
    private bool grounded;

    private Rigidbody2D my_rigidbody;
    private Animator anim;

    public int score;
    public Text scoreText;

    public int health;
    public int maxHealth;
    private float immortalityTime;
    public GameObject deathScreen;

    private bool spawning;
    private float spawningTime;
    private Vector3 spawnpoint;

    public bool looking;
    public GameObject MagicBallLeft, MagicBallRight;
    public Transform MagicBallsSpawn;
    public float fireRate;
    private float nextFire;

    private bool shooting;
    private bool shot;
    private float shotTime;
    private float shotAnimTime;

    private bool knockbackFromRight;
    private float knockbackCount;
    public float knockbackLength;
    public float knockbackPower;
    private bool knockbacked;


    // ---------- GAME CODE ---------- \\
    void Start()
    {
        my_rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        score = 0;
        SetScoreText();

        spawnpoint = new Vector3(-0.5f, -0.5f, 0);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapArea(ground_check_1.position, ground_check_2.position, what_is_ground);


        // ---------- WHEN NO MOVEMENT ---------- \\
        if (knockbackCount <= 0 && !knockbacked && !spawning)
            Movement();
        else if (!spawning)
        {
            Damaged();
            knockbacked = true;
        }


        // ---------- TIMERS ---------- \\
        if (Time.deltaTime > knockbackCount)
            knockbacked = false;

        if (Time.time > spawningTime)
            spawning = false;
    }

    void Update()
    {
        // ---------- PLAYER LOOKING DIRECTION ---------- \\
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            looking = false;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            looking = true;
        }


        // ---------- PLAYER SHOOTING ---------- \\
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shooting = true;
            shot = true;

            shotTime = Time.time + 0.3f;
            shotAnimTime = Time.time + 1f;
        }

        if (shot && Time.time >= shotTime)
        {
            shot = false;
            Shot();
        }

        if (Time.time > shotAnimTime)
            shooting = false;


        // ---------- PLAYER ANIMATIONS ---------- \\
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Shooting", shooting);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ---------- COLLECTING COINS ---------- \\
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            score += 1;
            SetScoreText();
        }


        // ---------- GETTING EXTRA LIVES ---------- \\
        if (other.gameObject.CompareTag("ExtraLife"))
        {
            other.gameObject.SetActive(false);

            if (health < maxHealth)
                health += 1;
        }


        // ---------- COLLIDING WITH SPIKES ---------- \\
        if (other.gameObject.CompareTag("Killer")) 
            Death();


        // ---------- COLLIDING WITH ENEMIES ---------- \\
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.transform.position.x < transform.position.x)
            {
                knockbackFromRight = true;
            }
            else
            {
                knockbackFromRight = false;
            }

            knockbackCount = knockbackLength;
        }


        // ---------- CHECKPOINTS ---------- \\
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Checkpoint>().Checked = true;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            spawnpoint = other.gameObject.GetComponent<Transform>().position;
        }
    }


    // ---------- FUNCIONS ---------- \\
    void Movement()
    {
        // ---------- PLAYER HORIZONTAL MOVEMENT & SPEED ---------- \\
        my_rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * move_speed, my_rigidbody.velocity.y);


        // ---------- PLAYER JUMP ---------- \\
        if (Input.GetButton("Jump") && grounded)
        {
            my_rigidbody.velocity = new Vector2(my_rigidbody.velocity.x, jump_height);
            grounded = false;
        }
    }

    void Damaged()
    {
        // ---------- KNOCKBACK ---------- \\
        gameObject.GetComponent<Animation>().Play("PlayerDamaged");

        if (knockbackFromRight)
        {
            my_rigidbody.velocity = new Vector2(knockbackPower * 3.5f, knockbackPower);
        }
        if (!knockbackFromRight)
        {
            my_rigidbody.velocity = new Vector2(-knockbackPower * 3.5f, knockbackPower);
        }
        knockbackCount -= Time.deltaTime;



        // ---------- DON'T GET DOUBLE+ DAMAGE ---------- \\
        if (Time.time > immortalityTime)
        {
            health -= 1;
            immortalityTime = Time.time + 0.5f;
        }


        // ---------- GAME OVER ---------- \\
        if (health <= 0)
            deathScreen.SetActive(true);
    }
    void Death()
    {
        // ---------- PLAYER TO CHECKPOINT ---------- \\
        if (health > 1)
        {
            my_rigidbody.transform.SetPositionAndRotation(new Vector3 (spawnpoint.x, spawnpoint.y + 2, spawnpoint.z), new Quaternion(0, 0, 0, 0));
            my_rigidbody.velocity = new Vector2(0, 0);

            spawning = true;
            spawningTime = Time.time + 0.45f;

            transform.localScale = new Vector3(1f, 1f, 1f);
            looking = false;
        }


        // ---------- DON'T GET DOUBLE+ DAMAGE ---------- \\
        if (Time.time > immortalityTime)
        {
            health -= 1;
            immortalityTime = Time.time + 0.5f;
        }


        // ---------- GAME OVER ---------- \\
        if (health <= 0)
            deathScreen.SetActive(true);
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Shot()
    {
        if (looking)
            Instantiate(MagicBallRight, MagicBallsSpawn.position, MagicBallsSpawn.rotation);
        else
            Instantiate(MagicBallLeft, MagicBallsSpawn.position, MagicBallsSpawn.rotation);
    }
}
