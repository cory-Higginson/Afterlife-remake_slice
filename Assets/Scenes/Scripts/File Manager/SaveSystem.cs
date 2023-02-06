using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        // Check if directory exists, if not create it
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "Save.json", saveString);
    }

    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "Save.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "Save.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
