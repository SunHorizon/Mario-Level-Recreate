using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    Rigidbody2D rb;
    //public Rigidbody2D rb2;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public Vector2 velocity;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public GameObject projectile;

    public bool isDamage;
    public LayerMask isDamageLayer;
    public Transform DamageCheck;
    public float DamageCheckRadius;

    public Vector2 offset = new Vector2(0.04f, 0.01f);
    public bool isDamage2;
    public Transform DamageCheck2;
    public float DamageCheckRadius2;
    public LayerMask isDamageLayer2;
    bool CheckProjectile;

    bool DeathCheck;

    public float fireRate;
    float timeSinceLastFire;

    public float AttackTime;

    public float jumpForceBoost;
    public float jumpForceBoostTime;

    public static bool CheckingEnemyMovement = false;
    public static bool CheckingConnonMovement = false;
    Animator anim;
    public bool isFacingLeft;
    bool jumped = false;
    float JumpTime = 0, JumpDelay = .5f;

    public static bool FinishedCheck;
    public static bool DeadCheck;

    public AudioClip LevelMusic;
    public AudioClip SmallJump;
    public AudioClip DeathMusic;
    public AudioClip FireProjectile;
    public AudioClip CollectCoin;
    public AudioClip PowerUpSound;
    public AudioClip FinishedSound;

    // Use this for initialization
    void Start()
    {
        SoundManager.instance.PlayMusic(LevelMusic, 0.2f);
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.Log("No Rigidbody2D found.");
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.mass = 2.0f;

        }
        if (speed <= 0)
        {
            speed = 3.0f;
            Debug.Log("Speed not set. Defaulting to " + speed);
        }
        if (jumpForce <= 0)
        {
            jumpForce = 10.0f;
            Debug.Log("jumpForce not set. Defaulting to " + jumpForce);
        }
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.1f;
            Debug.Log("groundCheckRadius not set. Defaulting to " + groundCheckRadius);
        }
        if (DamageCheckRadius <= 0)
        {
            DamageCheckRadius = 0.1f;
            Debug.Log("groundCheckRadius not set. Defaulting to " + DamageCheckRadius);
        }
        if (DamageCheckRadius2 <= 0)
        {
            DamageCheckRadius2 = 0.1f;
            Debug.Log("groundCheckRadius not set. Defaulting to " + DamageCheckRadius2);
        }
        if (jumpForceBoost <= 0)
        {
            jumpForceBoost = 5.0f;
            Debug.Log("jumpForceBoost not set. Defaulting to " + jumpForceBoost);
        }
        if (jumpForceBoostTime <= 0)
        {
            jumpForceBoostTime = 3.0f;
            Debug.Log("jumpForceBoostTime not set. Defaulting to " + jumpForceBoostTime);
        }
        if (velocity.x <= 0)
        {
            velocity.x = 5.0f;
            Debug.Log("velocity.x not set. Defaulting to " + velocity.x);
        }
        if (fireRate <= 0)
        {
            fireRate = 1f;
            Debug.Log("fireRate not set. Defaulting to " + fireRate);
        }
        if (AttackTime <= 0)
        {
            AttackTime = 6.0f;
            Debug.Log("SpeedBoostTime not set. Defaulting to " + AttackTime);
        }
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator found on " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveValue = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            JumpTime = JumpDelay;
            anim.SetTrigger("Jump");
            SoundManager.instance.PlaySound(SmallJump);
            jumped = true;

        }
        if (Input.GetButtonDown("Fire1") && CheckProjectile == true)
        {
            if (Time.time > timeSinceLastFire + fireRate)
            {
                Debug.Log("Pew");
                GameObject go = Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, transform.rotation);
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
                anim.SetTrigger("Attack");
                SoundManager.instance.PlaySound(FireProjectile);
                timeSinceLastFire = Time.time;
            }
        }
        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        anim.SetFloat("MoveSpeed", Mathf.Abs(moveValue));

        if ((moveValue < 0 && !isFacingLeft)
           || (moveValue > 0 && isFacingLeft))
            flip();


        JumpTime -= Time.deltaTime;
        if (JumpTime <= 0 && isGrounded && jumped == true)
        {
            anim.SetTrigger("Land");
            jumped = false;

        }  
        if(DeathCheck == true)
        { 
             rb.velocity = new Vector2(moveValue * 0, rb.velocity.y);
            
        }

    }
    void OnTriggerEnter2D(Collider2D c)
    {
        isDamage = Physics2D.OverlapCircle(DamageCheck.position, DamageCheckRadius, isDamageLayer2);
        isDamage2 = Physics2D.OverlapCircle(DamageCheck2.position, DamageCheckRadius2, isDamageLayer2);
        if (c.gameObject.tag == "Connon_Destroy" && isDamage || isDamage2)
        {

            Debug.Log("Trigger Detected: " + c.gameObject.tag);
            rb.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            anim.SetTrigger("Death");
            SoundManager.instance.PlayMusic(DeathMusic);
            DeadCheck = true;
            DeathCheck = true;
            Destroy(gameObject, 0.5f);

        }
        if (c.gameObject.tag == "Mario_Enter")
        {
            CheckingEnemyMovement = true;
        }
        if (c.gameObject.tag == "Mario_Exit")
        {
            CheckingEnemyMovement = false;
        }
        if (c.gameObject.tag == "Connon_Start")
        {
            CheckingConnonMovement = true;
        }
        if (c.gameObject.tag == "Connon_End")
        {
            CheckingConnonMovement = false;
        }
        //Debug.Log("Trigger Detected: " + c.gameObject.tag);
        if (c.gameObject.tag == "Collectible")
        {
            Collectible col = c.GetComponent<Collectible>();
            if (col)
            {
                Debug.Log("Collected: " + col.points + " points");
                SoundManager.instance.PlaySound(CollectCoin);
            }
            Destroy(c.gameObject);

        }
        if (c.gameObject.tag == "PowerUp_Jump")
        {
            jumpForce += jumpForceBoost;
            StartCoroutine(stopJumpBoost());
            Debug.Log("Jump Boost");
            SoundManager.instance.PlaySound(PowerUpSound);
            anim.SetTrigger("PowerUp");
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "PowerUp_Projectile")
        {
            Debug.Log("Attack Power Up");    
            CheckProjectile = true;
            StartCoroutine(AttackUp());
            SoundManager.instance.PlaySound(PowerUpSound);
            anim.SetTrigger("PowerUp");
            Destroy(c.gameObject);

        }
        if(c.gameObject.tag == "Finished")
        {
            SoundManager.instance.PlayMusic(FinishedSound);
            FinishedCheck = true;
        }

    }
    void OnCollisionEnter2D(Collision2D c){

        isDamage = Physics2D.OverlapCircle(DamageCheck.position, DamageCheckRadius, isDamageLayer);
        isDamage2 = Physics2D.OverlapCircle(DamageCheck2.position, DamageCheckRadius2, isDamageLayer);
        if (c.gameObject.tag == "Yoshi" && isDamage || isDamage2)
        {
            rb.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            anim.SetTrigger("Death");
            DeadCheck = true;
            SoundManager.instance.PlayMusic(DeathMusic);
            DeathCheck = true;
            Destroy(gameObject, 0.5f);

        }
    }

    void flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1; // scaleFactor.x = -scaleFactor.x;

        transform.localScale = scaleFactor;
    }

    IEnumerator stopJumpBoost()
    {
        yield return new WaitForSeconds(jumpForceBoostTime);
        jumpForce -= jumpForceBoost;
    }

    IEnumerator AttackUp()
    {
        yield return new WaitForSeconds(AttackTime);
        CheckProjectile = false;
    }
}
