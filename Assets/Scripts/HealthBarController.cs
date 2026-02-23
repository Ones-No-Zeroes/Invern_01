using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
   [Header("Attributes")]
   [SerializeField] private Slider characterHealthBar;

    /// <summary>
    /// Will apply the health of the character to its HealthBar
    /// </summary>
    /// <param name="characterHealth"> The current health of the character.</param>
   public void ApplyHealthAmountToHealthBar(int characterHealth)
    {
        if(characterHealth == 0) // If the characterHealth passed in is 0, then hide the HealthBar
        {
            characterHealthBar.gameObject.SetActive(false);
            characterHealthBar.value = characterHealth;
        }
        else
        {
            characterHealthBar.value = characterHealth;
        }
    }


    /// <summary>
    ///  Sets the Max value of the health bar of the character.
    /// </summary>
    /// <param name="maxHealth">The max health of the character</param>
    public void SetHealthBarMaxValue(int maxHealth)
    {
        characterHealthBar.maxValue = maxHealth;
    }

    public void UnhideHealthBar()
    {
        if (!characterHealthBar.gameObject.activeSelf)
        {
            characterHealthBar.gameObject.SetActive(true);
        }
    }

}
