﻿/*****************
 * By Nico
 * Purpose: DESCRIPTION
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Barrel : MonoBehaviour
{
	private Rigidbody2D rigid;
	public float speed = 1;
	public  int going = 1;
    public bool hurdled = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(speed * going, rigid.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D col) {
    	if (col.gameObject.tag == "Border") {
    		going *= -1;
    	} 
    }
}
