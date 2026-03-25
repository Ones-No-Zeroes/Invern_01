using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   // Private Fields
    

    // Public Fields
    
    // DARREN B. -- we can  kill the player on a delay using the animation controller as a trigger.
    // No need to hard-code or break up the Player prefab
    [SerializeField] private LeverDetectionArea leverDetectionArea;
    public float moveSpeed;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float jumpForce;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InvernLogic invernLogic;
    [SerializeField] private PlayerKnockbackLogic playerKnockbackLogic;
    

    public int score;

    // Variables for Inputs : using InputSystem -- Darren B.
    [SerializeField] private InputAction move, jump, worldSwitch, interact, antiGravityEffectOn, antiGravityEffectOff, openOptionsMenu;
    // Used to save the Vector2 input for movement to flip the sprite, if necessary -- DB
    [SerializeField] private Vector2 moveInput;
    // Saves the jump action this frame for any logic and animations that depend on the property -- DB
    [SerializeField] private bool jumpInput;
    
    // [SerializeField] private Vector3 scale;
    
    // Variables to track Grounded and World Void states, respectively
    [SerializeField]private bool isGrounded;
    [SerializeField]private bool isVoid;
    
    

    // Private Variables
    private Animator animator;
    private bool isGravityOn = true;
    private int amountOfGravityFlips = 2;

    [SerializeField] private PlayerShoot playerShoot;



    // Options Menu
    [Header("Options Menu Controller Script")]
    [SerializeField] private OptionsMenuManager optionsMenuController;

    [Header("Audio Managers")]
    [SerializeField] private PlayerSFXManager sfxManager;
    

    void Start () 
    { 
        // Component Searching
        animator = GetComponent<Animator>();

        // Movement and Interaction Context Finding
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        worldSwitch = InputSystem.actions.FindAction("WorldSwitch");
        interact = InputSystem.actions.FindAction("Interact");

        // Anti-Gravity Context Finding
        antiGravityEffectOn = InputSystem.actions.FindAction("AntiGravityEffectOn");
        antiGravityEffectOff = InputSystem.actions.FindAction("AntiGravityEffectOff");

        // Options Menu Pressed Context Finding
       

    }

    void FixedUpdate()
    {
        // Get the inputs this frame -- Darren B.
        moveInput = move.ReadValue<Vector2>();
        jumpInput = jump.IsPressed();

        // TEMP FIX
        if (!playerHealth.IsDead || optionsMenuController.isOptionMenuOpen)
        {
            
            if (!playerKnockbackLogic.IsKnockBackEnabled) // DO NOT APPLY MOVEMENT DURING KNOCKBACK!
            {
                //Player Movement Code
                
                rig.linearVelocity = new Vector2(moveInput.x * moveSpeed, rig.linearVelocityY);
            }
            if (rig.transform.localScale.x != moveInput.x && moveInput.x != 0)
            {
                FlipX(-math.sign(moveInput.x));
            }
        }
    }

    void Update()
    {
        if (optionsMenuController.isOptionMenuOpen)
        {
            return;
        }

        playerHealth.OutOfBoundsDeathChecker(sfxManager.GetComponent<AudioSource>(), sfxManager.PlayerDeathAudioClip);
        // Checks to see if the Player is DEAD and if the SpriteRenderer is enabled
        // 
        if (playerHealth.IsDead)
        {
            sfxManager.PlaySFX(sfxManager.PlayerDeathAudioClip);
            playerHealth.KillCharacter();
        }
        
        // Jump Input Handling
        if (jumpInput && isGrounded)
        {
            Jump();
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
        }

        if(amountOfGravityFlips > 0)
        {
            // Triggers the Gravity on and off effect.
            if (antiGravityEffectOn.WasPressedThisFrame() && isGravityOn)
            {
                InvernGravity();
                amountOfGravityFlips -= 1;
            }
            else if (antiGravityEffectOff.WasPressedThisFrame() && !isGravityOn)
            {
                UnvernGravity();
                amountOfGravityFlips -= 1;
            }
        }
        
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
        if(Vector2.Dot(collision.GetContact(0).normal, new Vector2 (0, math.sign(transform.localScale.y))) > 0.8f)
        {
            isGrounded = true;
            amountOfGravityFlips = 2;
        }      
    }

    public void AddScore(int amount)
    {
        sfxManager.PlaySFX(sfxManager.PlayerCoinAudioClip);
        score += amount;
        scoreText.text = "Score: " + score;
    }

    // Feed a vector 2 derived from move inputs. Flips the Player (all of the player, including its children).
    // This is the way -- Darren B.
    private void FlipX(float moveX)
    {
        // First line modified to be more robust when effects like gravity-flipping are applied -- Darren B.
        Vector3 v = new (moveX, rig.transform.localScale.y, rig.transform.localScale.z);
        rig.transform.localScale = v;
    }








    // Methods for handling the Gravity Flip mechanic
    // Note from Leijah, the names should be modified, is Inverning the name of the gravity mechanic or the ability to flip between worlds? Are they linked?

    private void Jump()
    {

        isGrounded = false;
        //animator.SetBool("lowGrounded", false);
        if(gameObject.transform.localScale.y == 1)
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
             rig.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        }
        
        Debug.Log("Up Jump");
        
    }
    
    private void InvernGravity()
    {
        rig.gravityScale = -2;
        gameObject.transform.localScale = new (gameObject.transform.localScale.x, -gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        isGravityOn = false;
        Debug.Log("Invern");
    }

    private void UnvernGravity()
    {
        rig.gravityScale = 2;
        gameObject.transform.localScale = new (gameObject.transform.localScale.x, -gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        isGravityOn = true;
        Debug.Log("Unvern");
    }

    public void ResetGravity()
    {
        rig.gravityScale = 2;
        gameObject.transform.localScale = new (gameObject.transform.localScale.x, -gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        amountOfGravityFlips = 2;
        isGravityOn = true;
    }

}
