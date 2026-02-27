using Unity.VisualScripting;
using System.IO;
using UnityEngine;

[System.Serializable]
public class JSONSaving
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "playerOptionsSettings.json");

    public static void SaveInJSON(object dataToSave)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new System.Exception("File path does not exist.");
            }

            string savedVolumeDataAsJSON = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(filePath, savedVolumeDataAsJSON);
        }
        catch(System.Exception e)
        {
            Debug.Log($"File exception: {e}");
        }
        
    }
}
