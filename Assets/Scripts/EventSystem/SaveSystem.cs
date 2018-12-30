﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SavePlayerProgress(Experience exp, WaveSpawner lvl, Shooting wp)
    {
      
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.progress";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(exp,lvl,wp);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    

    public static PlayerData LoadPlayerLevel()
    {
        string path = Application.persistentDataPath + "/player.progress";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }

}
