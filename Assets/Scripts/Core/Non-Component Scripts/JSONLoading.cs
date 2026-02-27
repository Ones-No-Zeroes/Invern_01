using System;
using System.IO;
using System.Reflection;
using UnityEngine;

public class JSONLoading
{
    public static object LoadFromJSON(string nameOfJSONFile)
    {
        try
        {
            string jsonAsString = File.ReadAllText(Path.Combine(Application.persistentDataPath, nameOfJSONFile));
            SaveableData objContainingJSONData = JsonUtility.FromJson<SaveableData>(jsonAsString);
            return objContainingJSONData;
        }
        catch (System.Exception e)
        {
            Debug.Log($"File exception: {e}");
            return null;
        }
    }

    public static void ReadData(object objectContainingData)
    {
        FieldInfo[] data = objectContainingData.GetType().GetFields();
        foreach (FieldInfo field in data)
        {
            Debug.Log($"Field Name: {field.Name}... Field Value: {field.GetValue(objectContainingData)}");
        }
    }
}
