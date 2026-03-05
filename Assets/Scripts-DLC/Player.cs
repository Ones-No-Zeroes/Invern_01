using System;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpStrength;
    
    
    public Rigidbody2D rigid2D;
    public SpriteRenderer sprite;
    public Animator animator;
    public TextMeshProUGUI scoreText;

    public bool lowGrounded;
    public bool highGrounded;

    public int score;


    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {


        //if (Checkpoint.savedPosition != Vector2.zero)
        //{
        //    transform.position = Checkpoint.savedPosition; //Bad Logic, character starts in no mans land after level completion, needs to reset back to level start.
            
        //}
    }

    void FixedUpdate()
    {
        //Player Movement Code
        float moveInput = Input.GetAxisRaw("Horizontal");
        rigid2D.linearVelocity = new Vector2(moveInput * moveSpeed, rigid2D.linearVelocityY);

        //Sprite Direction Pointing Code
        if (rigid2D.linearVelocityX < 0)
        {
            sprite.flipX = true;
        }
        else if (rigid2D.linearVelocityX > 0)
        {
            sprite.flipX = false;
        }
    }

    void Update()
    {
     
        //Character Controls
        if (Input.GetKeyDown(KeyCode.Space) && lowGrounded == true)
        {
            UpJump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && highGrounded == true)
        {
            DownJump();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Invern();

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Unvern();
        }

        //Game Over Logic
        if (this.transform.position.y < -30.0f || this.transform.position.y > 73.0f) //Probably even bigger game over conditions dependent on level design
        {

            GameOver();
        }

        //100 Coins Logic
        if (score == 100)
        {
            Coins100();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Dot(collision.GetContact(0).normal, Vector2.up) > 0.8f)
        {
            Debug.Log("Low");
            lowGrounded = true;
            animator.SetBool("lowGrounded", true);
        }
        else if (Vector2.Dot(collision.GetContact(0).normal, Vector2.down) > 0.8f)
        {
            Debug.Log("High");
            highGrounded = true;
            animator.SetBool("highGrounded", true);
        }
    }

    public void UpJump()
    {
        lowGrounded = false;
        animator.SetBool("lowGrounded", false);
        rigid2D.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        Debug.Log("Up Jump");
        
    }

    public void DownJump()
    {
        highGrounded = false;
        animator.SetBool("highGrounded", false);
        rigid2D.AddForce(Vector2.down * jumpStrength, ForceMode2D.Impulse);
        Debug.Log("Down Jump");
    }

    public void Invern()
    {
        lowGrounded = false;
        animator.SetBool("lowGrounded", false);
        animator.SetBool("InvernBool", true);
        rigid2D.gravityScale = -2;
        Debug.Log("Invern");
    }

    public void Unvern()
    {
        highGrounded = false;
        animator.SetBool("InvernBool", false);
        animator.SetBool("highGrounded", false);
        rigid2D.gravityScale = 2;
        Debug.Log("Unvern");
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Coins100()
    {
        SceneManager.LoadScene(3); //How to make this a field in the investigator? To prevent hard-coding.
    }

 }
