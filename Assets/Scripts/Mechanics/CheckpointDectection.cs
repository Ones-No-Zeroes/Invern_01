using Unity.VisualScripting;
using UnityEngine;

public class CheckpointDectection : MonoBehaviour
{
    [Header("Checkpoint Dectection Attributes")]
    [SerializeField] private CheckPointLogic checkPointLogic;



    void OnTriggerEnter2D(Collider2D collision)
    {
        // Remove string literal
        if (collision.CompareTag("Player") && !checkPointLogic.isCheckPointActive)
        {
            checkPointLogic.ActivateCheckPoint();
            checkPointLogic.isCheckPointActive = true;
        }
    }
}
