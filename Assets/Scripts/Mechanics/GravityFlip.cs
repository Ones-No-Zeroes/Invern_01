using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    [Header("Gravity Flip Attributes")]
    [SerializeField] private bool isGravityOn;
    [SerializeField] private Rigidbody2D rb;

    // Turning on/off Gravity at the beginning of the gaming depending on the Designer
    protected virtual void Start()
    {
        if (isGravityOn)
        {
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else
        {
            SwitchGravity();
        }
    }


    /// <summary>
    /// Switches the Gravity using the GravityScale of the Rigidbody the object has
    /// </summary>
    public virtual void SwitchGravity()
    {
        rb.gravityScale = rb.gravityScale * -1;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }
}
