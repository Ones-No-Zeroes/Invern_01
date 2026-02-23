// We need this to close the player in the editor
using UnityEditor;
using UnityEngine;
// We need to use this for controller support and cheat codes, as below
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Lets us put in cheat codes for testing
    Keyboard keyboard;
    void Start()
    {
        keyboard = InputSystem.GetDevice<Keyboard>();
    }
    public void OnPlayButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void OnQuitButton()
    {
        // Closes the Unity editor player on clicking Quit button
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        // This is all we need in the final build
        Application.Quit();
        #endif
    }
    void FixedUpdate()
    {
        // Jump to level 2
        if (keyboard.digit2Key.IsPressed()) SceneManager.LoadSceneAsync(2);
        Debug.Log("Cheat code: Jump to L2");
        // Jump to level 3
        if (keyboard.digit3Key.IsPressed()) SceneManager.LoadSceneAsync(3);
        Debug.Log("Cheat code: Jump to L3");
    }
}