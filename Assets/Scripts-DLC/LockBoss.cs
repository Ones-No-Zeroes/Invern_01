using UnityEngine;
using UnityEngine.SceneManagement;

public class LockBoss : MonoBehaviour
{
    public int healthPoints = 10;
    public float moveSpeed;
    public Vector3 moveOffset;
    private Vector3 startPos;
    private Vector3 targetPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (transform.position == targetPos)
        {

            if (targetPos == startPos)
            {
                targetPos = startPos + moveOffset;
            }
            else
            {
                targetPos = startPos;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 from;
        Vector3 to;
        if (Application.isPlaying)
        {
            from = startPos;
        }
        else
        {
            from = transform.position;
        }

        to = from + moveOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawWireSphere(to, 0.2f);
        Gizmos.DrawWireSphere(from, 0.2f);
    }
    public void LoseHP()
    {
        healthPoints -= 1;
        if (healthPoints < 0)
        {

            BossDefeat();
        }

    }

    private void BossDefeat()
    {
        Debug.Log("Winner!");
        Destroy(this.gameObject);
        SceneManager.LoadScene(5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //other.GetComponent<LockBoss>().LoseHP();
        //Destroy(other.GetComponent<Shoot>());
    }
}
