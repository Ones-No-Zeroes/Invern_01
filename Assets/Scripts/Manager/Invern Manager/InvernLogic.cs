using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class InvernLogic : MonoBehaviour
{   
    // Private Variables -- canSwitch will track whether or not the player can use the worldSwitch
    // We will need a var. to track time, and at a set value (2 seconds?), invoke a method to reenable canSwitch
    // Optimal: timer dies until needed (maybe only increment if !canSwitch)
    // worldVoid now tracks whether we are in the Void(true = white tiles and Nameless-1 sprite) -- Darren B.


    [SerializeField] private bool canSwitch = true, worldVoid = false;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyInvernLogic enemyInvernLogic;
    [SerializeField] private MusicManager MusicManager;
    private GameObject[] voidModeObjects;
    private GameObject[] lightModeObjects;


    // Public Properties
    public bool WorldVoid { get { return worldVoid; } }

    void Start()
    {
        lightModeObjects = GameObject.FindGameObjectsWithTag("Light");
        voidModeObjects = GameObject.FindGameObjectsWithTag("Void");
        InvernStateOnStart();

    }

    

    /// <summary>
    /// Inverns the entire scene. If Invern Mode is active, then disable it, else if Invern Mode is inactive, enable it.
    /// </summary>
    public void InvernWorld()
    {
        if(player.transform.parent != null)
        {
            player.transform.parent = null;
        }

        if (worldVoid) // if enabled, disables switching
        {
            Debug.Log("Disable Void Mode");
            foreach(GameObject lightObj in lightModeObjects)
            {
                lightObj.SetActive(true);
            }
            foreach(GameObject voidObj in voidModeObjects)
            {
                voidObj.SetActive(false);
            }

            canSwitch = false;
            worldVoid = false;
            ChangeBackGround();
            MusicManager.SwitchMusic();
        }
        else // Else for if isInvernActivate is false, enables Invern Mode
        {
            Debug.Log("Enable Void Mode");
            foreach(GameObject lightObj in lightModeObjects)
            {
                lightObj.SetActive(false);
            }
            foreach(GameObject voidObj in voidModeObjects)
            {
                voidObj.SetActive(true);
            }
            worldVoid = true;
            ChangeBackGround();
            MusicManager.SwitchMusic();
        }
        enemyInvernLogic.EnemyWorldState(worldVoid);
        
    }

    // PRIVATE METHODS

    /// <summary>
    /// Private method, will hide and unhide the needed objects for if Invern Mode is enabled or disabled. Used on start only.
    /// </summary>
    private void InvernStateOnStart()
    {
        //ChangeBackGround();
        enemyInvernLogic.EnemyWorldState(worldVoid);
        // Game Designer can set if Invern is on at the beginning of the game or not.
        if (worldVoid)
        {
            // Hide ALL white objects, unhide ALL black objects
            foreach(GameObject voidObj in voidModeObjects)
            {
                voidObj.SetActive(true);
            }
           foreach(GameObject lightObj in lightModeObjects)
            {
                lightObj.SetActive(false);
            }
        }
        else
        {
            // Hide ALL Void objects, unhide ALL Non-Void objects
            foreach(GameObject lightObj in lightModeObjects)
            {    
                lightObj.SetActive(true);
            }
            foreach(GameObject voidObj in voidModeObjects)
            {
                voidObj.SetActive(false);
            }

        }
    }

    
    /// <summary>
    /// Changes the background of the world
    /// </summary>
    private void ChangeBackGround()
    {
        if (worldVoid)
        {
            cam.backgroundColor = Color.black; 
        }
        else
        {
            cam.backgroundColor = Color.white; 
        }
    }
}
