using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    Rigidbody2D rb;

    public float speed;
    public bool isFacingLeft;
    Animator anim;

    public bool isHead;
    public LayerMask isHeadLayer;
    public Transform headCheck;
    public float headCheckRadius;
    public AudioClip StompSound;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.Log("No Rigidbody2D found.");      
        }
        if (speed <= 0)
        {
            speed = 0.5f;
            Debug.Log("Speed not set. Defaulting to " + speed);
        }
        if (headCheckRadius <= 0)
        {
            headCheckRadius = 0.1f;
            Debug.Log("groundCheckRadius not set. Defaulting to " + headCheckRadius);
        }
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator found on " + name);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Character.CheckingEnemyMovement == true)
        {
            //anim.SetBool("EnemyDie", false);
            if (isFacingLeft)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                anim.SetBool("EnemyEnter", true);

            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                anim.SetBool("EnemyEnter", true);
            }

        }
        else
        {
            anim.SetBool("EnemyEnter", false);
        }
           
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Filpper")
        {
            flip();
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Yoshi")
        {
            flip();
        }
        if (c.gameObject.tag == "Projectile")
        {
           Destroy(gameObject);
        }
        Character cha = c.gameObject.GetComponent<Character>();
        isHead = Physics2D.OverlapCircle(headCheck.position, headCheckRadius, isHeadLayer);
        if (c.gameObject.tag == "Player" && isHead)
        {
            cha.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            speed = 0;
            //anim.SetTrigger("EnemyDie");
            SoundManager.instance.PlaySound(StompSound);
            anim.SetBool("EnemyDie", true);
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
}
