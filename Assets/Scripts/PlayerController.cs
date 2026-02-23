using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   // Private Fields
    [SerializeField] private SwitchBetweenMusic switchBetweenMusic; // TESTING PURPOSES, SHOULD BE REMOVED AND IMPROVED!

    // Public Fields{
    // Note from Leijah: Remove the public access modifier from as many variables as possible. Also, remove he Camera and the Canvas as a child of the Player. This causes issues
    // When trying to Kill the Player (making the player not active). The sound should not come form the Player, but instead from a gameObject dedicated to sound.
    
    // DARREN B. -- we can  kill the player on a delay using the animation controller as a trigger.
    // No need to hard-code or break up the Player prefab
    [SerializeField] private LeverDetectionArea leverDetectionArea;
    public bool isAlive = true;
    public float moveSpeed;
    public Rigidbody2D rig;
    public float jumpForce;
    public TextMeshProUGUI scoreText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InvernLogic invernLogic;
    [SerializeField] private PlayerKnockbackLogic playerKnockbackLogic;
    

    public int score;

    // Variables for Inputs : using InputSystem -- Darren B.
    [SerializeField] private InputAction move, jump, worldSwitch, interact;
    // Used to save the Vector2 input for movement to flip the sprite, if necessary -- DB
    [SerializeField] private Vector2 moveInput;
    // Saves the jump action this frame for any logic and animations that depend on the property -- DB
    [SerializeField] private bool jumpInput;
    
    // [SerializeField] private Vector3 scale;
    
    // Variables to track Grounded and World Void states, respectively
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isVoid;
    
    

    // Animation controller -- Darren B.
    private Animator animator;
    // DEPRECATED -- Darren B.
    //public SpriteRenderer sr;


    //Audio Variables
    private AudioSource playerAudio;
    public AudioClip jumpClip;
    public AudioClip death;
    public AudioClip coin;
    public PlayerShoot playerShoot;
    

    void Start () 
    { 
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        worldSwitch = InputSystem.actions.FindAction("WorldSwitch");
        interact = InputSystem.actions.FindAction("Interact");

    }

    void FixedUpdate()
    {
        // Get the current direction the player is facing, in case it has changed for some reason. -- Darren B
        //scale = rig.transform.localScale;

        // Get the inputs this frame -- Darren B.
        moveInput = move.ReadValue<Vector2>();
        // Debug.Log($"Move Input(s) this frame: (X:{moveInput.x}, Y:{moveInput.y}).");
        jumpInput = jump.IsPressed();

        // TEMP FIX
        if (!playerHealth.IsDead)
        {
            
            if (!playerKnockbackLogic.IsKnockBackEnabled) // DO NOT APPLY MOVEMENT DURING KNOCKBACK!
            {
                //Player Movement Code
                
                rig.linearVelocity = new Vector2(moveInput.x * moveSpeed, rig.linearVelocityY);

                // Feeding the animation controller the current Velocity -- Darren B.
                
                // Debug.Log("Player movement applied");
            }
            // Using my rigidBody flipping methodology from last semester. -- Darren B.
            // Matches X-scale to move input. Flip if needed.
            // Should be normalized X-velocity, not moveInput, if you want it to go with forward momentum instead. This feels faster.
            // Condensed -- Darren B.
            if (rig.transform.localScale.x != moveInput.x && moveInput.x != 0)
            {
                // We may have use for normalizing moveInput outside of this if statement -- Darren B.
                Vector2 moveNormal = moveInput.normalized;
                FlipX(moveNormal.x);
            }
            // if (rig.transform.localScale.x < 0 && moveInput.x > 0)
            // {
            //     FlipX(moveInput.x);
            // } 

            /*
            // Flips the shooting point, as to match with where the player is facing
            // Reworked this to point spell origin where the character is facing, based on scale.x
            // We may not need this any more.
            // Still need to disconnect the mouse-tracking -- Darren B.
            playerShoot.FlipShootingPointPosition(rig.transform.localScale.x);
            */

            // Sprite Direction Pointing Code
            // Deprecated -- Darren B.
            // if (rig.linearVelocityX > 0)
            // {
            //     sr.flipX = true;
            // }
            // else if (rig.linearVelocityX < 0)
            // {
            //     sr.flipX = false;
            // }
        }
    }

    void Update()
    {
        

        // Checks to see if the Player is DEAD and if the SpriteRenderer is enabled
        // 
        if (playerHealth.IsDead)
        {
            playerAudio.PlayOneShot(death);
            playerHealth.KillCharacter();
        }
        
        // This bit is redundant now -- Darren B.
        /*
        else if(playerHealth.IsDead
        // && !sr.enabled -- DEPRECATED
        )
        {
            return;
        }
        */

        // Jump Input Handling
        if (jumpInput && isGrounded)
        {
            isGrounded = false;
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAudio.PlayOneShot(jumpClip);
        }

        // World Switch Input Handling - to add a cooldown, control access to this code block -- Darren B.
        if (worldSwitch.WasPressedThisFrame() && isVoid)
        {
            isVoid = false;
            invernLogic.InvernWorld();
        }
        else if (worldSwitch.WasPressedThisFrame() && !isVoid)
        {
            isVoid = true;
            invernLogic.InvernWorld();
        }

        // Temporarily disabled, pending peer review
        // Gravity Flip input handling - Interact = C
        if (interact.WasPressedThisFrame())
        {
            if (leverDetectionArea.PlayerInLeverActivationArea)
            {
                leverDetectionArea.CurrentLeverEnemy.ToggleLever();
            }
            // Temporary home for gravity flipping
            GravityFlip();
        }

        // FOR TESTING PURPOSES!
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     switchBetweenMusic.SwitchMusic();
        // }

        // Send updates to the animation controller before leaving Update() -- Darren B.
        // Movement updates for animations - changed xVelocity feeder variable to fix twitching caused by surface contact "vibration" -- Darren B.
        if (move != null)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(moveInput.x));
            animator.SetFloat("yVelocity", rig.linearVelocityY);
        }
        else
        {
            animator.SetFloat("xVelocity", 0);
            animator.SetFloat("yVelocity", rig.linearVelocityY);
        }
        // Grounded state for animations
        if (isGrounded) animator.SetBool("isGrounded", true);
        else animator.SetBool("isGrounded", false);
        // World state for animations
        if (isVoid) animator.SetBool("isVoid", true);
        else animator.SetBool("isVoid", false);
        // Feed animator controller when shooting
        if (playerShoot.shooting) animator.SetBool("isCasting", true);
        else animator.SetBool("isCasting", false);
        
    }

    //Is Character grounded code
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Dot(collision.GetContact(0).normal, Vector2.up) > 0.8f)
        {
            isGrounded = true;
        }
    }

    // Needs to be removed from the player

    //GameOver function that reloads the current scene from start
    // public void GameOver()
    // {
       
    //     //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    // }

    public void AddScore(int amount)
    {
        playerAudio.PlayOneShot(coin);
        score += amount;
        scoreText.text = "Score: " + score;
    }

    // Feed a vector 2 derived from move inputs. Flips the Player (all of the player, including its children).
    // This is the way -- Darren B.
    public void FlipX(float moveX)
    {
        // First line modified to be more robust when effects like gravity-flipping are applied -- Darren B.
        Vector3 v = new (moveX, rig.transform.localScale.y, rig.transform.localScale.z);
        rig.transform.localScale = v;
        Debug.Log($"{gameObject.name} Scale of X: {rig.transform.localScale.x}.");
    }

    // Flips the player vertically
    public void FlipY()
    {
        Vector3 v = new (rig.transform.localScale.x, rig.transform.localScale.y * -1, rig.transform.localScale.z);
        rig.transform.localScale = v;
    }

    // Inverts gravity for the player only
    public void InvertPlayerGravity()
    {
        rig.gravityScale *= -1;
    }

    // Handles "Gravity Flip" mechanic
    public void GravityFlip()
    {
        InvertPlayerGravity();
        Invoke(nameof(FlipY), 0.15f);
    }
}
