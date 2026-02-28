using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class JSONLoading
{
    /// <summary>
    /// Load the required data for the SaveableData struct from a JSON file.
    /// </summary>
    /// <param name="nameOfJSONFile">The name of the JSON file</param>
    /// <returns>Returns an instance of SaveableData</returns>
    public static SaveableData LoadFromJSON(string nameOfJSONFile)
    {
        // Try and Catch while reading the contents of the JSON file
        try
        {
            string jsonAsString = File.ReadAllText(Path.Combine(Application.persistentDataPath, nameOfJSONFile));
            SaveableData deserializedJSONFile = JsonUtility.FromJson<SaveableData>(jsonAsString);
            return deserializedJSONFile; // Returns a deserialized of the JSON file
        }
        catch (System.Exception e)
        {
            Debug.Log($"File exception: {e}");

            SaveableData defaultSaveableData = new SaveableData();

            // Assign Default Values
            defaultSaveableData.musicVolumeAmount = 0.5f;
            defaultSaveableData.sfxVolumeAmount = 0.5f;
            defaultSaveableData.brightnessAmount = 0;


            return defaultSaveableData; // Returns a default of the SaveableData
        }
    }

    /// <summary>
    /// Sets the data from the deserialized file of the JSON file to the Static saveableData of Controller
    /// </summary>
    /// <param name="objectContainingData">The deserialized SaveableData from LoadFromJSON</param>
    public static void SetDataFromJSONFile(SaveableData objectContainingData)
    {
        Controller.saveableData = objectContainingData;
    }

    /// <summary>
    /// Sets the data from the deserialized file of the JSON file by just using the name of the JSON file.
    /// </summary>
    /// <param name="nameOfJSONFile">The name of the JSON file</param>
    public static void SetDataFromJSONFile(string nameOfJSONFile)
    {
        Controller.saveableData = LoadFromJSON(nameOfJSONFile);
    }
}
