using Unity.VisualScripting;
using System.IO;
using UnityEngine;

[System.Serializable]
public class JSONSaving
{
    private static string filePath = Application.persistentDataPath;

    /// <summary>
    /// Save data in a JSON file. 
    /// </summary>
    /// <param name="dataToSave">Given data must be an object</param>
    /// <param name="fileName">The filename is what the JSON file will be saved as</param>
    public static void SaveInJSON(object dataToSave, string fileName)
    {
        try
        {
            string savedVolumeDataAsJSON = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(Path.Combine(filePath, fileName), savedVolumeDataAsJSON);
        }
        catch(System.Exception e)
        {
            Debug.Log($"File exception: {e}");
        }
        
    }
}
