using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    // Serialized Fields
   [Header("Options Menu Submenus")]
   [Header("(The first item must be the FIRST sub menu to be opened)")]
    public GameObject[] subOptionMenus;


    [Header("Audio Settings Saving")]
    [SerializeField] private AudioController audioController;
   
    // Privates Variables
    private InputAction openOptionsMenu;

    // Properties
    public bool isOptionMenuOpen {get; private set;}


    private void Start()
    {
        openOptionsMenu = InputSystem.actions.FindAction("OpenOptions");
    }

    void Update()
    {
        if (openOptionsMenu.WasPressedThisFrame())
        {
           ToggleOptionsMenu();
        }
    }


    /// <summary>
    /// Toggles the Options Menu ON or OFF depending on the value of isOptionMenuOpen
    /// </summary>
    private void ToggleOptionsMenu()
    {
        if (isOptionMenuOpen)
        {
            foreach(GameObject subMenu in subOptionMenus)
            {
                if(!subMenu.activeSelf)
                    continue;
                
                subMenu.SetActive(false);
            }
            isOptionMenuOpen = false;
            Time.timeScale = 1f;
            JSONSaving.SaveInJSON(Controller.saveableData, "savedOptionsData.json");
        }
        else
        {
            isOptionMenuOpen = true;
            subOptionMenus[0].SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
