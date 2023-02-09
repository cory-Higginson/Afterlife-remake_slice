using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTopSectionValues : MonoBehaviour
{
    [SerializeField] private Text YearValue;
    [SerializeField] private Text MoneyValue;
    [SerializeField] private Text SelectionValue;
    [SerializeField] private Text CostValue;

    public void ChangeYearValue(int year)
    {
        YearValue.text = "Year: " + year.ToString();
    }

    public void ChangeMoneyValue(int money)
    {
        MoneyValue.text = money.ToString() + " £";
    }
    
    public void ChangeSelection(string name)
    {
        SelectionValue.text = name;
    }

    public void ChangeCostValue(int cost)
    {
        CostValue.text = "Cost: " + cost.ToString() + " £";
    }
}
