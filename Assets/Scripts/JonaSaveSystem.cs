using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class JonaSaveSystem 
{
    private const string SavesPath = "/jonaSaves/";

    public static void Save<T>(T obj, string key)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + SavesPath;
        Directory.CreateDirectory(path);
        
        FileStream stream = new FileStream(path + key, FileMode.Create);
        bf.Serialize(stream, obj);
        stream.Close();
    }

    public static T Load<T>(string key)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + SavesPath;

        T obj = default;
        
        if (File.Exists(path + key))
        {
            FileStream stream = new FileStream(path + key, FileMode.Open);
            obj = (T)bf.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.LogError("File does not exist at :" +path + key);
        }
        return obj;
    }

    public static bool FileExist(string key)
    {
        string path = Application.persistentDataPath + SavesPath;

        return File.Exists(path + key);
    }
}
