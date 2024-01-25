using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EquimpentSaverLoader
{
    public void SaveToJSON<T>(T objectToSave, string filename)
    {
        Debug.Log(JsonUtility.ToJson(objectToSave));
        WriteFile(GetPath(filename), JsonUtility.ToJson(objectToSave));
        Debug.Log("Saved to: " + GetPath(filename));
    }

    public T LoadFromJSON<T>(string fileName) 
    {
        return JsonUtility.FromJson<T>(ReadFile(GetPath(fileName)));
    }

    private string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    private string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }
    private void WriteFile(string path, string content)
    {
        Debug.Log("Open File Stream: " + path);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
        fileStream.Close();
        Debug.Log("Close File Stream: " + path);
    }
}

[Serializable]
public class HardwareSaveObject
{
    public string name;
    public int amount;
}

[Serializable]
public class SerializableList<T>
{
    public List<T> list = new List<T>();

}

[SerializeField]
public class SoftwareSaveObject
{
    public string name;
    public Vector3 position;
}