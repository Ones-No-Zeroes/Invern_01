using UnityEngine;

public class KillZone : MonoBehaviour
{


    [SerializeField] private Color debugColor = Color.purple;

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

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (box != null)
        {
            Gizmos.DrawCube(transform.position + (Vector3)box.offset, box.size);
        }

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
}
