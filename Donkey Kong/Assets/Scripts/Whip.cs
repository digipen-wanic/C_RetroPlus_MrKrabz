/*****************
 * By Nico
 * Purpose: script for the whip for plus
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    public float crackTime = 1;
    private float crackTimer = 0;
    private bool cracking = false;
    public BoxCollider2D collide;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cracking)
        {
            crackTimer -= Time.deltaTime;
            if (crackTimer <=0)
            {
                UnCrack();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
        }
    }
    void Crack()
    {
        cracking = true;
        crackTimer = crackTime;
        sprite.enabled = true;
        collide.enabled = true;
    }
    void UnCrack()
    {
        cracking = false;
        sprite.enabled = false;
        collide.enabled = false;
    }
}
