using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class FileManager : MonoBehaviour
{
    [SerializeField]private EconomyManager _economyManager;

    private void Awake()
    {
        // Find the Economy Manager
        _economyManager = GameObject.FindWithTag("EconomyManager").GetComponent<EconomyManager>();
        SaveSystem.Init();
    }

    public void Save()
    {
        SaveFile saveFile = new SaveFile
        {
            pennies = _economyManager.TotalPennies,
            // Way to save locations.
            //buildingLocations = new [] { new Vector3(0f,0f,0f), new Vector3(1f,1f,1f)}
        };
        string json = JsonUtility.ToJson(saveFile);
        SaveSystem.Save(json);
        
        Debug.Log("Saved!");
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveFile saveFile = JsonUtility.FromJson<SaveFile>(saveString);


            _economyManager.TotalPennies = saveFile.pennies;

            Debug.Log("Loaded!");
        }
        else
        {
            Debug.Log("No save");
        }
    }


    // The data that will be saved.
    private class SaveFile
    {
        public float pennies;
        public Vector3[] buildingLocations;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
}



