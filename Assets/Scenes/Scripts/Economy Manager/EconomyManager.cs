using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EconomyManager : MonoBehaviour
{
    public float rateMultiplier = 0.01f;
    [SerializeField] private float tempPopulation; 
    [SerializeField] private float tempAmountOfTiles; 
    [SerializeField] private int tempYear;
    [SerializeField] private float soulRate;
    [SerializeField] private GameObject remotesec;

    private SoulManager _soulManager;
    private WorldManager _worldManager;

    public int current_cost;
    
    public float TotalPennies
    {
        get {return pennies; }
        set {pennies = value; }
    }
    public float pennies;

    
    public float YearlyPaycheck
    {
        get {return paycheckAmount; }
        set {paycheckAmount = value; }
    }
    public float paycheckAmount;
    
    
    public float PenniesPerSoul
    {
        get {return penniesPerSoulAmount; }
        set {penniesPerSoulAmount = value; }
    }
    public float penniesPerSoulAmount;

    // Calculate the SOUL Rate.
    public float CalculateSoulRate(float population, float amountOfTiles, int year)
    {
        var value = population / amountOfTiles * (year * rateMultiplier) > 0
            ? population / amountOfTiles * (year * rateMultiplier)
            : 0;
        return value;
    }

    // Add funds.
    public void AddPennies(float amount)
    {
        pennies += amount;
        remotesec.GetComponent<ChangeRemoteValues>().ChangeMoneyValue(pennies);
    }

    // Deduct funds.
    public void RemovePennies(float amount)
    {
        TotalPennies -= amount;
        remotesec.GetComponent<ChangeRemoteValues>().ChangeMoneyValue(pennies);
    }

    public void updateCost(int amount)
    {
        current_cost = amount;
        remotesec.GetComponent<ChangeRemoteValues>().ChangeCostValue(amount);
    }
    
    // Round to 2 decimal places
    public static float Round(float value, int digits)
    {
        var mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    // Update the soul rate every 0.2s
    private void Update()
    {
        StartCoroutine(UpdateSoulRate());
        //AddPennies(1.0f);
    }

    IEnumerator UpdateSoulRate()
    {
        soulRate = Round(CalculateSoulRate(_soulManager.AmountOfSouls(), _worldManager.amount_of_changed_tiles, tempYear), 2);
        Debug.Log(Round(CalculateSoulRate(_soulManager.AmountOfSouls(), _worldManager.amount_of_changed_tiles, tempYear), 10));
        remotesec.GetComponent<ChangeRemoteValues>().ChangeSoulsValue(soulRate);
        yield return new WaitForSeconds(0.9f);
    }


    private void Start()
    {
        // Update the money at the start
        remotesec.GetComponent<ChangeRemoteValues>().ChangeMoneyValue(TotalPennies);
        // Load references
        _worldManager = GameObject.Find("World Manager").GetComponent<WorldManager>();
        _soulManager = GameObject.Find("Entity Manager").GetComponent<SoulManager>();
    }
}
