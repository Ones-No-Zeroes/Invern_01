using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class PlayButtonPlayGame : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        // this half is needed to close the built application
        #else
        Application.Quit();
        // we won't need this endif in the build version, either
        #endif
    }

}
