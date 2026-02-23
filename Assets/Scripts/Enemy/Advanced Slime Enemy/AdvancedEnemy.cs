using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    // Serialized Fields 
    [Header("Enemy Movement Attributes")]
    [SerializeField] private float offSetFromStart;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    // Private Fields
    private float startPositionX;
    private float endPositionX;

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
        startPositionX = transform.position.x;
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

    private void MoveTowardsPlayer()
    {

        endPositionX = player.transform.position.x;

        if((int)transform.position.x == (int)endPositionX)
        {
            return;
        }
        else
        {
            float direction = endPositionX - transform.position.x;
            rigidBody.linearVelocity = new Vector2(Mathf.Sign(direction) * (moveSpeed * 2) * Time.deltaTime, rigidBody.linearVelocity.y);
        }



        // targetPos.x = player.transform.position.x; // Add a slight offset?

        // if(transform.position.x == targetPos.x)
        // {
        //     return;
        // }
        // else
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);   
        // }
    }
    
    // public void TakeDamage (int damage)
    // {
    //     currentHealth -= damage;
    //     StartCoroutine(FlashBlack());
    //     if(currentHealth <= 0)
    //     {
    //         Die();
    //     }
    // }

}
