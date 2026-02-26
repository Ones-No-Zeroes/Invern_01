using System;
using UnityEngine;

public class OptionsButtonController : MonoBehaviour
{   
    [Header("Options Button Attributes")]
    [SerializeField] private OptionsMenuController optionsMenuController;

    // Public Methods 

    /// <summary>
    /// Toggles the desired Sub-Menu on.
    /// </summary>
    /// <param name="indexOfSubMenuToToggle">The index of the Sub-Menu you want to enable</param>
    public void ToggleSubSettingMenu(int indexOfSubMenuToToggle)
    {
        for(int index = 0; index <  optionsMenuController.subOptionMenus.Length; index++)
        {
            // If the Index equals the index of the submenu we want to toggle, then enable it.
            if(index == indexOfSubMenuToToggle)
            {
                optionsMenuController.subOptionMenus[index].SetActive(true);
                continue; 
            }

            // If the element within the array is false, no need to turn it false, therefore just skip.
            if (!optionsMenuController.subOptionMenus[index].activeSelf)
                continue;

            // If all other checks are false, then set the menu's active state to false.
            optionsMenuController.subOptionMenus[index].SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the Main Sub-Menu of the Options Menu. Will take the user back to the Main Sub-Menu no matter what menu they are in.
    /// </summary>
    public void ToggleMainOptionMenu()
    {
        GameObject currentlyActiveSubMenu = Array.Find(optionsMenuController.subOptionMenus, subMenu => subMenu.activeSelf == true);
        currentlyActiveSubMenu.SetActive(false);
        optionsMenuController.subOptionMenus[0].SetActive(true);
    }
}
