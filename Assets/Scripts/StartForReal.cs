using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartForReal : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
