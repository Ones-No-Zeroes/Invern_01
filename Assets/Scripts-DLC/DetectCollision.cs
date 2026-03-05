using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //other.GetComponent<LockBoss>().LoseHP();
        //Destroy(other.GetComponent<Shoot>());


        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Target"))
        {
            Debug.Log("Hit");
            collision.GetComponent<LockBoss>().LoseHP();
        }

            //Destroy(this.gameObject);
            //Destroy(other.gameObject);
        }
}
