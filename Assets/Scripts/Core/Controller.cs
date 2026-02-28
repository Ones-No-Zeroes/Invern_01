using Unity.VisualScripting;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    // Instance of the SaveableData struct
    public static SaveableData saveableData = new SaveableData();
    protected abstract void Update();

    /// <summary>
    /// Method to apply all the needed values from the SaveableData struct
    /// </summary>
    public abstract void ApplyLoadedValuesFromSavedData();
}
