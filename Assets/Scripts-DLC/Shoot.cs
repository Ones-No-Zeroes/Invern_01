using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 20f;
    private InputAction antiGravityEffectOn, antiGravityEffectOff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);

         // Anti-Gravity Context Finding
        antiGravityEffectOn = InputSystem.actions.FindAction("AntiGravityEffectOn");
        antiGravityEffectOff = InputSystem.actions.FindAction("AntiGravityEffectOff");
    }

    // Update is called once per frame
    void Update()
    {
        if (antiGravityEffectOn.WasPressedThisFrame())
        {
            rb.gravityScale = -2;
        }
        if (antiGravityEffectOff.WasPressedThisFrame())
        {
            rb.gravityScale = 2;
        }
    }
    
}
