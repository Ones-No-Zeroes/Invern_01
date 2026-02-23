using System.Collections;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Update()
    {
        if (IsDead)
        {
            KillCharacter();
        }
    }

    public override void KillCharacter()
    {
        Destroy(gameObject);
    }

    public override void DamageCharacter(int damageAmount)
    {
        base.DamageCharacter(damageAmount);
        StartCoroutine(FlashBlack());
    }

    private IEnumerator FlashBlack()
    {
        
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
