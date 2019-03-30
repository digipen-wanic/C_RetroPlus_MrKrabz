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
    public KeyCode downKey;
    public float speed = 10;
    public float jumpTime = 1;
    public float jumpForce = 10;
    public float climbForce = 5;
    public Text lifeText;
    private float jumpTimer = 1;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool canJump = true;
    private BoxCollider2D collider;
    public GameOver gameOver;
    private bool inLadder = false;
    private Animator animator;
    public static int lives = 2;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.Lose();
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
        this.animator = GetComponent<Animator>();
        lifeText.text = "M\n"+lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(this.leftKey))
        {
            this.sprite.flipX = false;
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            animator.SetInteger("State", 1);
        }
        else if (Input.GetKey(this.rightKey))
        {
            this.sprite.flipX = true;
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
            animator.SetInteger("State", 1);

        } 
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetInteger("State", 0);

        }
        if (Input.GetKeyDown(this.jumpKey) && canJump)
        {
            canJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpTimer = jumpTime;
        }
    }
    public void ModLives(int to) {
        lives += to;
        Debug.Log(lives);
        if (lives < 0) {
            Debug.Log("lose");
            gameOver.Lose();
            Destroy(gameObject);
        }
        lifeText.text = "M\n"+lives.ToString();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            if (Input.GetKey(upKey))
            {
                this.rigid.velocity = new Vector2(rigid.velocity.x, climbForce);
            } else if (Input.GetKey(downKey) ){
                this.rigid.velocity = new Vector2(rigid.velocity.x, -climbForce);
            }
            else
            {
                this.rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }
        }
        
        }
        void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            inLadder = false;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canJump && collision.gameObject.tag == "Platform" && collision.transform.position.y < rigid.transform.position.y)
        {
            canJump = true;
        }
       
        foreach (ContactPoint2D cp in collision.contacts)
        {
            if (collision.gameObject.tag == "Platform")
            {
                if (cp.point.y > collider.bounds.min.y && canJump)
                {
                    rigid.MovePosition(new Vector2(rigid.transform.position.x, rigid.transform.position.y + 0.04f));
                }
            }
        }
    }
}
