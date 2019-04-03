using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private PlayerController player;
    private BoxCollider2D collide;
    private SpriteRenderer sprite;
    public SpriteRenderer parentSprite;
    public bool enabled = false;
    public float hTime = 13;
    private float hTimer = 0;
    public AudioSource stageMusic;
    public AudioSource hammerMusic;
    public ScoreDisplay scDisp;
    public AudioSource destSFX;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
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
        	destSFX.Play();
        	print("hit");
        	player.IncScore(500);
            Destroy(col.gameObject);
            scDisp.DisplayScore(500, transform.position);
        }
    }
    public void Enable()
    {
        enabled = true;
        sprite.enabled = true;
        collide.enabled = true;
        hTimer = hTime;
        parentSprite.enabled = false;
        //stageMusic.Stop();
        hammerMusic.Play();
    }
    public void Disable()
    {
        enabled = false;
        sprite.enabled = false;
        collide.enabled = false;
        parentSprite.enabled = true;
        if (!stageMusic.isPlaying)
        {
            stageMusic.Play();
        }

    }
}
