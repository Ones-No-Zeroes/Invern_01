using Unity.VisualScripting;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public static SaveableData saveableData = new SaveableData();
    protected abstract void Update();

    public abstract void ApplyLoadedValuesFromSavedData();
}
