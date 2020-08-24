using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem : MonoBehaviour
{
    public static void SavePlayer(Player player, int money, int level) //save player data - only needed when the level is changed
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, money, level);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SavePlayer(Player player, int money) //save player data without changing level saved
    {
        PlayerData data = LoadPlayer();
        if (data != null)
        {
            SavePlayer(player, money, data.level);
        }
        else
        {
            SavePlayer(player, money, 1); //no saves before so first level is 1
        }
    }

    public static PlayerData LoadPlayer() //return player's data or null if didnt save before
    {
        string path = Application.persistentDataPath + "/player.fun";
       // Debug.Log(path);
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
            return null; //returns null if there is no previous saves (first run time)
        }
    }

    public static void SaveSettings(SettingsScript settings) //save settings data
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settings);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SettingsData LoadSettings() //return settings data or null if didnt save before
    {
        string path = Application.persistentDataPath + "/settings.fun";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;

            stream.Close();
            return data;
        }
        else
        {
            return null; //returns null if there is no previous saves (first run time)
        }
    }


}
