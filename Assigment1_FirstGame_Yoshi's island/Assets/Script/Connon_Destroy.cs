﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connon_Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Connon_Destroy")
        {
            Destroy(c.gameObject);
        }    
    }
}
