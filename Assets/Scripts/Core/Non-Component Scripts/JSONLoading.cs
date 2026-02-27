using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class JSONLoading
{
    public static SaveableData LoadFromJSON(string nameOfJSONFile)
    {
        try
        {
            string jsonAsString = File.ReadAllText(Path.Combine(Application.persistentDataPath, nameOfJSONFile));
            SaveableData deserializedJSONFile = JsonUtility.FromJson<SaveableData>(jsonAsString);
            return deserializedJSONFile;
        }
        catch (System.Exception e)
        {
            Debug.Log($"File exception: {e}");

            SaveableData defaultSaveableData = new SaveableData();
            defaultSaveableData.musicVolumeAmount = 0.5f;
            defaultSaveableData.sfxVolumeAmount = 0.5f;

            return defaultSaveableData;
        }
    }

    public static void SetDataFromJSONFile(SaveableData objectContainingData)
    {
        Controller.saveableData = objectContainingData;
    }

    public static void SetDataFromJSONFile(string nameOfJSONFile)
    {
        Controller.saveableData = LoadFromJSON(nameOfJSONFile);
    }
}
