using System;
using System.IO;
using UnityEngine;

public class JSONLoading
{
    public object LoadFromJSON<T>(string filePathToJSONFile)
    {
        try
        {
            string jsonAsString = File.ReadAllText(filePathToJSONFile);
            object objContainingJSONData = JsonUtility.FromJson<SaveableData>(jsonAsString);
            return objContainingJSONData;
        }
        catch (System.Exception e)
        {
            Debug.Log($"File exception: {e}");
            return null;
        }
    }
}
