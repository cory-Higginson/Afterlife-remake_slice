using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EconomyManager : MonoBehaviour
{
    public float rateMultiplier = 0.01f;
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
        return population / amountOfTiles * (year * rateMultiplier);
    }

    // Add funds.
    public void AddPennies(float amount)
    {
        pennies += amount;
    }

    // Deduct funds.
    public void RemovePennies(float amount)
    {
        TotalPennies -= amount;
    }
}
