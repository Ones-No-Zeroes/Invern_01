using System;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Serialized Fields 
    [Header("Boss Movement Attributes")]
    [SerializeField] private float offSetFromStart;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Boss Features Attributes")]

    // Private Fields
    private float startPositionX;
    private float endPositionX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        startPositionX = transform.position.x;
        endPositionX = startPositionX + offSetFromStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
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
        

        if((int)transform.position.x != (int)endPositionX)
        {
            if(endPositionX == startPositionX)
            {
                rigidBody.linearVelocity = new Vector2((-1 * moveSpeed * 2) * Time.deltaTime, rigidBody.linearVelocity.y); // For some reason it freezes the character if the moveSpeed is above 60?
            }
            else
            {
                rigidBody.linearVelocity = new Vector2((moveSpeed * 2) * Time.deltaTime, rigidBody.linearVelocity.y);
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
