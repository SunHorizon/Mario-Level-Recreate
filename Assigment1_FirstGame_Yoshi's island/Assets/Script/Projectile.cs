using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float lifeTime;

    // Use this for initialization
    void Start () {
        if (lifeTime <= 0)
        {
            lifeTime = 2.0f;
            Debug.Log("LifeTime not set. Defulting to " + lifeTime);
        }

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
