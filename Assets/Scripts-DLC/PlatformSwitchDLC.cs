using UnityEngine;

public class PlatformSwitchDLC : MonoBehaviour
{
    public GameObject light;
    public GameObject dark;
    public bool invern = false; //Pay attention to this line of code. //Unsure still how to have different level starting states. It's desirable to have the option to start a level in a Invern state or not. {Upside-down}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && invern == false)
        {
            Dark();
            invern = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && invern == true)
        {
            Light();
            invern = false;
        }
    }


    private void Light()
    {
        Debug.Log("Dark Off");
        dark.SetActive(false);
        light.SetActive(true);
    }

    private void Dark()
    {
        Debug.Log("Light Off");
        light.SetActive(false);
        dark.SetActive(true);
    }


}
