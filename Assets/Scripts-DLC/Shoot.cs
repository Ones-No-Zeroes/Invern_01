using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.gravityScale = -2;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.gravityScale = 2;
        }
    }
}
