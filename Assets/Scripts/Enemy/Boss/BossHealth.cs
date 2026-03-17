using UnityEngine;

public class BossHealth : EnemyHealth
{
    [Header("Boss Health Attributes")]
    [SerializeField] private Animator animator;
    public override void DamageCharacter(int damageAmount)
    {
        base.DamageCharacter(damageAmount);
    }
}
