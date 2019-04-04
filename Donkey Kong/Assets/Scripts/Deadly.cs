/*****************
 * By Nico
 * Purpose: changes lives
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player" && collision.enabled)
        {
            collision.gameObject.GetComponent<PlayerController>().ModLives(-1);
        }
    }
}
