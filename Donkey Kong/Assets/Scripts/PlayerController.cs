using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode jumpKey;
    public float speed = 10;
    public float jumpTime = 1;
    public float jumpForce = 10;
    private float jumpTimer = 1;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(this.leftKey))
        {
            this.sprite.flipX = false;
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        } else if (Input.GetKey(this.rightKey))
        {
            this.sprite.flipX = true;
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        }
        if (Input.GetKeyDown(this.jumpKey) && canJump)
        {
            canJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpTimer = jumpTime;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canJump && collision.gameObject.tag == "Platform" && collision.transform.position.y < rigid.transform.position.y) {
            canJump = true;
        }
    }
}
