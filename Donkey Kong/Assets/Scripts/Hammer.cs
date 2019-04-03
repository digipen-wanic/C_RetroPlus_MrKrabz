using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private PlayerController player;
    private BoxCollider2D collide;
    private SpriteRenderer sprite;
    private SpriteRenderer parentSprite;
    public bool enabled = false;
    public float hTime = 13;
    private float hTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        parentSprite = GetComponentInParent<SpriteRenderer>();
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
    	hTimer -= Time.deltaTime;
    	if (hTimer <= 0) {
    		Disable();
    	}
    }
    void OnCollisionEnter2D(Collision2D col)
    {
    	print("collds");
        if (col.gameObject.tag == "Barrel" && enabled)
        {
        	print("hit");
        	player.IncScore(500);
            Destroy(col.gameObject);
        }
    }
    public void Enable()
    {
        enabled = true;
        sprite.enabled = true;
        collide.enabled = true;
        hTimer = hTime;
    }
    public void Disable()
    {
        enabled = false;
        sprite.enabled = false;
        collide.enabled = false;

    }
}
