/*****************
 * By Nico
 * Purpose: a timer
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timed : MonoBehaviour
{
    public float time = 1;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
