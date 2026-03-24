using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossHealth : EnemyHealth
{
    [Header("Boss Health Attributes")]
    [SerializeField] private Animator animator;
    [SerializeField] private BossMovement bossMovement;

    [Header("For Calling Methods OUTSIDE of The Boss GameObject")]
    [SerializeField] private UnityEvent OnDamaged;


    [Header("On Boss Death")]
    [SerializeField] private UnityEvent OnKilled;
    
    [Header("Components to Disable on Death")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D[] AllBoxColliders;
    

    public override void DamageCharacter(int damageAmount)
    {
        if (!bossMovement.CanBossMove)
        {
            return;
        }
        base.DamageCharacter(damageAmount);
        bossMovement.StunBoss();
        OnDamaged.Invoke();
        Debug.Log(CurrentHealth);
        animator.SetFloat("CurrentHealth", CurrentHealth);
    }

    protected override void Update()
    {
        if (IsDead)
        {
            KillCharacter();
        }
        
    }

    public override void KillCharacter()
    {
        bossMovement.StopBossMovement();
        OnKilled.Invoke();
        animator.SetBool("IsMoving", false);

        Destroy(rb);
        foreach(BoxCollider2D box in AllBoxColliders)
        {
            Destroy(box);
        }

        SceneManager.LoadScene(0);
        
    }


}
