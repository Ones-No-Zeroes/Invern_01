using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Level1Button()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene("Scene1");
    }

    public void Level2Button()
    {
        SceneManager.LoadScene(2);
    }

    public void TitleButton()
    {
        SceneManager.LoadScene(0);
    }
}
