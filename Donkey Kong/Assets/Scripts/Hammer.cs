using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private PlayerController player;
    private BoxCollider2D collide;
    private SpriteRenderer sprite;
    public bool enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        Disable();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStart2D(Collision2D col)
    {
        if (col.gameObject.tag == "Barrel" && enabled)
        {
            Destroy(col.gameObject);
            Disable();
        }
    }
    public void Enable()
    {
        enabled = true;
        sprite.enabled = true;
        collide.enabled = true;
    }
    public void Disable()
    {
        enabled = false;
        sprite.enabled = false;
        collide.enabled = false;

    }
}
