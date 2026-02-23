using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFlag : MonoBehaviour
{
    public bool finalLevel;
    public string nextLevelName;
    // Darren experimenting with  holding onto the existing player object
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Player"))
        // {
        //     if (finalLevel == true)
        //     {
        //         SceneManager.LoadScene(0);
        //     }
        //     else
        //     {
        //         // Darren experimenting with  holding onto the existing player object
        //         // Doesn't seem to work
        //         DontDestroyOnLoad(target:player);
        //         SceneManager.LoadScene(nextLevelName);
        //     }

        // }
        
    }
}
