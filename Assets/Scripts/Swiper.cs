using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swiper : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    public Rigidbody2D rb;

    public Transform groundCheck;

    public float groundCheckRadius;

    public LayerMask whatIsGround;

    public bool onGround;

    public bool isPlaying;

    public Animator animator;

    public LvlGenerator generator;

    public bool swinging;

    public int doubleJump;

    //Start
    private void Start()
    {
        isPlaying = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        Score.scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(KeyCode.Mouse0) && rb.position.x < 0)
        {
            isPlaying = true;
        }
        if (isPlaying)
        {
            Score.scoreValue += 1;

            //Increase velocity as game goes on
            if (rb.position.x < 100)
            {
                rb.velocity = new Vector2(5f, rb.velocity.y);
            }
            if (rb.position.x >= 100 && rb.position.x <= 250)
            {
                rb.velocity = new Vector2(5.5f, rb.velocity.y);
            }
            if (rb.position.x > 250 && rb.position.x <= 500)
            {
                rb.velocity = new Vector2(6f, rb.velocity.y);
            }
            if (rb.position.x > 500 && rb.position.x <= 750)
            {
                rb.velocity = new Vector2(6.5f, rb.velocity.y);
            }
            if (rb.position.x > 750 && rb.position.x <= 1000)
            {
                rb.velocity = new Vector2(7f, rb.velocity.y);
            }
            if (rb.position.x > 1000)
            {
                rb.velocity = new Vector2(8f, rb.velocity.y);
            }

            //Basic animation control
            if (onGround)
            {
                doubleJump = 0;
                animator.SetBool("Walk", true);
                animator.SetBool("Jump", false);
            }
            else
            {
                animator.SetBool("Jump", true);
            }

            //Swipe detection
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Reset double jump
            if (onGround)
            {
                doubleJump = 0;
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        //Debug.Log("Swipe UP");
        if (doubleJump < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            animator.SetBool("Jump", true);
            doubleJump += 1;
            if (doubleJump == 1 || doubleJump == 2)
            {
                animator.Play("Walk");
            }
        }
    }

    void OnSwipeDown()
    {
        //Debug.Log("Swipe Down");
        animator.Play("Attack");
        StopAllCoroutines();
        StartCoroutine("Swinging");
    }

    void OnSwipeLeft()
    {
        //Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        //Debug.Log("Swipe Right");
    }

    //Collision handling
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }

        if (collision.gameObject.tag == "DestroyableEnemy" && swinging)
        {
            Score.scoreValue += 100;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "DestroyableEnemy" && !swinging)
        {
            Die();
        }
    }

    //Death method
    private void Die()
    {
        StopAllCoroutines();
        StartCoroutine("RespawnDelay");
    }

    public IEnumerator RespawnDelay()
    {
        isPlaying = false;
        animator.SetTrigger("Die");
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
        if (Score.highScore < Score.scoreValue)
        {
            Score.highScore = Score.scoreValue;
            PlayerPrefs.SetInt("highscore", Score.highScore);
        }
        SceneManager.LoadScene("Start");
    }

    //Change boolean swinging variable
    public IEnumerator Swinging()
    {
        swinging = true;
        yield return new WaitForSeconds(1f);
        swinging = false;
    }
}
