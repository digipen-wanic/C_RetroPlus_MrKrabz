using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float turnInterval = 0.5f;
    public float ascendSpeed = 1;
    public float speed = 2;
    public float chanceUp = 0.5f;
    private float turnTimer = 0;
    private SpriteRenderer sprite;
    private new BoxCollider2D collider;
    private int going = 1;
    public bool ascending = false;
    private bool blockedAsc = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        turnTimer = turnInterval;
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ascending)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0)
            {
                turnTimer = turnInterval;
                float doe = Random.value;
                if (doe <= 0.33)
                {
                    going = 0;
                    //Stay still
                }
                else if (doe <= 0.66)
                {
                    //Should move right
                    sprite.flipX = false;
                    going = 1;
                }
                else
                {
                    //Should move left
                    sprite.flipX = true;
                    going = -1;
                }
            }

            rigid.velocity = new Vector2(speed * going, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(0, ascendSpeed);
        }
    }
    void OnTriggerLeave2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladder" && !ascending && blockedAsc)
        {
            blockedAsc = false;
        } 
    }
    void OnCollisionLeave2D(Collision2D leave)
    {
        if (leave.gameObject.tag == "Platform" && ascending)
        {
            ascending = false;
        }
    }
    void OnTriggerEnter2D(Collider2D colt)
    {
        if (colt.gameObject.tag == "Ladder")
        {
            float doe = Random.value;
            if (doe <= chanceUp)
            {
                ascending = true;
                rigid.MovePosition(new Vector2(colt.transform.position.x, transform.position.y));
            }
            else
            {
                blockedAsc = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            going *= -1;
        }
        foreach (ContactPoint2D cp in collision.contacts)
        {
            if (collision.gameObject.tag == "Platform")
            {
                if (cp.point.y > collider.bounds.min.y)
                {
                    rigid.MovePosition(new Vector2(rigid.transform.position.x, rigid.transform.position.y + 0.04f));
                }
            }
        }
    }
}
