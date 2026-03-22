using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Serialized Fields 
    [Header("Boss Movement Attributes")]
    [SerializeField] private float offSetFromStart;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    // Private Fields
    private float startPositionX;
    private float endPositionX;
    private bool canBossMove = true;

    // Public Properties
    public bool CanBossMove => canBossMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        startPositionX = transform.position.x;
        endPositionX = startPositionX + offSetFromStart;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canBossMove)
        {
            MoveBoss();
        }
        
    }

    

    private void MoveBoss()
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
                
                if(math.sign(transform.localScale.x) != 1)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                rigidBody.linearVelocity = new Vector2((moveSpeed * 2) * Time.deltaTime, rigidBody.linearVelocity.y);
                if(math.sign(transform.localScale.x) != -1)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }

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

    public void StunBoss()
    {
        if (!canBossMove)
        {
            return;
        }

        rigidBody.linearVelocity = Vector3.zero;
        StartCoroutine(StunBossCoroutine());

    }

    public void StopBossMovement()
    {
        if (canBossMove)
        {
            canBossMove = false;
            rigidBody.linearVelocity = Vector3.zero;
        }
        
    }
    private IEnumerator StunBossCoroutine()
    {
        canBossMove = false;
        animator.SetBool("IsMoving", false);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsMoving", true);
        canBossMove = true;
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
