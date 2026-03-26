using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    // Serialized Fields 
    [Header("Enemy Movement Attributes")]
    [SerializeField] private int offSetFromStart;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    // Private Fields
    private int startPositionX;
    private int endPositionX;

    // Public Fields
    public bool isPlayerDetected = false; // Will not be a public access modifier


    // Properties
    // public Vector3 TargetPos
    // {
    //     get{return targetPos; }
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        startPositionX = (int)transform.position.x;
        endPositionX = startPositionX + offSetFromStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPlayerDetected)
        {
            Patrol();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    

    private void Patrol()
    {   
        if(transform.position.x < startPositionX)
        {
            endPositionX = startPositionX + offSetFromStart;
        }
        else if(transform.position.x > startPositionX + offSetFromStart)
        {
            endPositionX = startPositionX;
        }
        

        if((int)transform.position.x != endPositionX)
        {
            if(endPositionX == startPositionX)
            {
                rigidBody.linearVelocity = new Vector3(-1 * moveSpeed, rigidBody.linearVelocity.y, 0); // For some reason it freezes the character if the moveSpeed is above 60?
                Debug.Log("Moving towards Start");
            }
            else
            {
                rigidBody.linearVelocity = new Vector3(moveSpeed, rigidBody.linearVelocity.y, 0);
                Debug.Log("Moving towards End");
            }
        }
        else
        {
            if(endPositionX == startPositionX)
            {
                endPositionX = startPositionX + offSetFromStart;
            }
            else
            {
                endPositionX = startPositionX;
            }
            
        }
    }

    private void MoveTowardsPlayer()
    {

        endPositionX = (int)player.transform.position.x;

        if((int)transform.position.x == endPositionX)
        {
            return;
        }
        else
        {
            float direction = transform.position.x - endPositionX;
            rigidBody.linearVelocity = new Vector2(Mathf.Sign(direction) * (moveSpeed * 2), rigidBody.linearVelocity.y);
        }



      
    }

     private void OnDrawGizmos()
    {
        Vector3 from;
        Vector3 to;
        if (Application.isPlaying)
        {
            from = new Vector3(startPositionX, transform.position.y, 0);
        }
        else
        {
            from = transform.position;
        }

        to = new Vector3(from.x + offSetFromStart, transform.position.y, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawWireSphere(to, 0.2f);
        Gizmos.DrawWireSphere(from, 0.2f);
    }
}
