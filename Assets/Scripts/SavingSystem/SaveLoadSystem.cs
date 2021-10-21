using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity;
using UnityEngine;

public static class SaveLoadSystem
{

    static string path = Application.persistentDataPath + "/sd.atc";
    public static void SaveGameData(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            binaryFormatter.Serialize(fileStream, saveData);
        }
    }

    public static SaveData LoadGameData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return binaryFormatter.Deserialize(fileStream) as SaveData;
            }
        }

        else return null;
    }
}
