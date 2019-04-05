/*****************
 * By Nico
 * Purpose: Player controller for moving and animations
*****************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode jumpKey;
    public KeyCode downKey;
    public KeyCode whipKey;
    public float speed = 10;
    public float jumpTime = 1;
    public float jumpForce = 10;
    public float climbForce = 5;
    public Text lifeText;
    public float jumpThreshold = 1;
    private float jumpTimer = 1;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool canJump = true;
    private new BoxCollider2D collider;
    public GameOver gameOver;
    private Animator animator;
    private bool inLadder = false;
    public static int lives = 2;
    private Hammer hammer;
    public SpriteRenderer hammerSprite;
    public Transform hammerTransform;
    public int score = 0;
    public Text scoreText;
    public static int highScore = 0;
    public Text highScoreText;
    public Text bonusText;
    public int bonusPoints = 5000;
    public int decBonusBy = 100;
    public float decTime = 3;
    private float decTimer = 0;
    public Animator hammerAnim;
    public bool hasWhip = false;
    public AudioSource winMusic;
    public AudioSource finalMusic;
    public AudioSource deathMusic;
    public ScoreDisplay scDisp;
    public int pointsPerLife = 7000;
    private int p = 0;
    public AudioSource jumpSFX;
    public AudioSource footstepSFX;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
        this.animator = GetComponent<Animator>();
        this.hammer = GetComponentInChildren<Hammer>();
        lifeText.text = "M\n" + lives.ToString();
        decTimer = decTime;
        hammer.Disable();
    }
    public void IncScore(int points)
    {
        score += points;
        scoreText.text = score.ToString("I-0000000");
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = score.ToString("TOP-000000");
        }
        if (score >= pointsPerLife * p) {
            p++;
            ModLives(1);
        }
    }
    public void Win(bool final)
    {
        IncScore(bonusPoints);
        if (final)
        {
            finalMusic.Play();
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            winMusic.Play();
            SceneManager.LoadScene("PlusLevel");
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject closest = this.gameObject;
        float cDist = 1000;
        GameObject[] list = GameObject.FindGameObjectsWithTag("Barrel");
        for (int i = 0; i < list.Length; i++)
        {
            float dist = Mathf.Sqrt(Mathf.Pow(list[i].transform.position.x - rigid.transform.position.x, 2) + Mathf.Pow(list[i].transform.position.y - rigid.transform.position.y, 2));
            if (dist < cDist)
            {
                closest = list[i];
                cDist = dist;
            }
        }
        if (closest.tag != "Player" && cDist < jumpThreshold && sprite.bounds.min.y > closest.GetComponent<SpriteRenderer>().bounds.max.y && !closest.GetComponent<Barrel>().hurdled) 
        {
            print("points");
            closest.GetComponent<Barrel>().hurdled = true;

            IncScore(100);
        }
        decTimer -= Time.deltaTime;
        if (decTimer <= 0)
        {
            bonusPoints -= decBonusBy;
            bonusText.text = bonusPoints.ToString("0000");
            decTimer = decTime;
        }
        if (Input.GetKey(this.leftKey))
        {
            //footstep test sound
            footstepSFX.Play();
            inLadder = false;
            Vector3 lScale = new Vector3(-1, 1, 1);
            
            this.sprite.flipX = false;
            hammerTransform.localScale = lScale;

            rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            
            if (canJump)
            {
                animator.SetInteger("State", 1);
              

                hammerAnim.SetInteger("State", 1);
            }
        }
        else if (Input.GetKey(this.rightKey))
        {
            inLadder = false;
            Vector3 lScale = new Vector3(1, 1, 1);
            //foostep test sound
            footstepSFX.Play();
            hammerTransform.localScale = lScale;
            this.sprite.flipX = true;

            rigid.velocity = new Vector2(speed, rigid.velocity.y);
               
            if (canJump)
            {
                animator.SetInteger("State", 1);
                hammerAnim.SetInteger("State", 1);

            }
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if (canJump)
            {
                animator.SetInteger("State", 0);
                hammerAnim.SetInteger("State", 0);
            }

        }
        if (Input.GetKeyDown(this.jumpKey) && canJump && !hammer.enabled)
        {
            canJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpTimer = jumpTime;
            animator.SetInteger("State", 2);
            jumpSFX.Play();
        }
        if (hasWhip && Input.GetKeyDown(whipKey))
        {

        }

    }
    public void ModLives(int to)
    {
        lives += to;
        Debug.Log(lives);
        if (lives < 0)
        {
            Debug.Log("lose");
            gameOver.Lose();
            Destroy(this.gameObject);
        }
        else
        {
            if (to < 0)
            {
                print("die");
                deathMusic.Play();
                SceneManager.LoadScene("BaseGame");
            }
            lifeText.text = "M\n" + lives.ToString();
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ladder" && !hammer.enabled)
        {
            rigid.gravityScale = 0.0f;
            if (Input.GetKey(upKey))
            {
                rigid.transform.position = new Vector3(col.bounds.min.x, rigid.transform.position.y, -1);
                inLadder = true;
                this.rigid.velocity = new Vector2(rigid.velocity.x, climbForce);
            }
            else if (Input.GetKey(downKey))
            {
                rigid.transform.position = new Vector3(col.bounds.min.x, rigid.transform.position.y, -1);
                inLadder = true;
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
            rigid.gravityScale = 1.0f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WinCollide")
        {
            Win(false);
        }
        else if (collision.gameObject.tag == "DoneCollide")
        {
            Win(true);
        }
        if (!canJump && collision.gameObject.tag == "Platform" && collision.transform.position.y < rigid.transform.position.y)
        {
            canJump = true;
        }
        if (collision.gameObject.tag == "Hammer")
        {
            hammer.Enable();
            Destroy(collision.gameObject);
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
