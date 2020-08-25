using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartKnight : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform groundCheck;

    public float groundCheckRadius;

    public LayerMask whatIsGround;

    private bool onGround;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
