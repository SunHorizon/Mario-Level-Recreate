using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connon_Enemy : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public bool isFacingLeft;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.Log("No Rigidbody2D found.");
        }
        if (speed <= 0)
        {
            speed = 2f;
            Debug.Log("Speed not set. Defaulting to " + speed);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Character.CheckingConnonMovement == true)
        {
            if (isFacingLeft)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);

            }
        }else if (Character.CheckingConnonMovement == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
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
