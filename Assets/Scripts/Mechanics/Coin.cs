using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreToGive;
    public float bobHeight;
    public float bobSpeed;

    private float startYPos;

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void Update()
    {
        float newY = startYPos + (Mathf.Sin(Time.time * bobSpeed) * bobHeight);
        transform.position = new Vector3(transform.position.x, newY, 0);
    }


    //My Code
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().AddScore(scoreToGive);
            Destroy(this.gameObject);
        }
    }
}
