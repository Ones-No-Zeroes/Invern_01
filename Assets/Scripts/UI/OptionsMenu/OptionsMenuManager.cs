using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    // Serialized Fields
   [Header("Options Menu Submenus")]
   [Header("(The first item must be the FIRST sub menu to be opened)")]
    public GameObject[] subOptionMenus;


    [Header("Options Managers")]
    [SerializeField] private UnityEvent OnGameLoad;
   
    // Privates Variables
    private InputAction openOptionsMenu;

    // Properties
    public bool isOptionMenuOpen {get; private set;}


    private void Start()
    {
        openOptionsMenu = InputSystem.actions.FindAction("OpenOptions");
        
        // Loading from a JSON file at start
        SaveableData dataFromJSONFile = JSONLoading.LoadFromJSON("savedOptionsData.json");
        JSONLoading.SetDataFromJSONFile(dataFromJSONFile);

       OnGameLoad.Invoke(); // Tightly coupled logic with Unity Event

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
            // Turn off ALL subMenus that make up the Options Menu
            foreach(GameObject subMenu in subOptionMenus)
            {
                if(!subMenu.activeSelf)
                    continue;
                
                subMenu.SetActive(false);
            }
            
            // Puts the timeScale to 1, and save the needed data to a JSON file.
            isOptionMenuOpen = false;
            Time.timeScale = 1f;
            JSONSaving.SaveInJSON(Controller.saveableData, "savedOptionsData.json");

            
        }
        else
        {
            // Opens the Main sub-menu of the Options Menu
            isOptionMenuOpen = true;
            subOptionMenus[0].SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
