using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRemoteValues : MonoBehaviour
{
    [SerializeField] private Text YearValue;
    [SerializeField] private Text MoneyValue;
    [SerializeField] private Text SelectionValue;
    [SerializeField] private Text CostValue;

    public void ChangeYearValue(int year)
    {
        YearValue.text = "Year: " + year.ToString();
    }

    public void ChangeMoneyValue(float money)
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

    /// <summary>
    /// BOTTOM SECTION
    /// </summary>
    /// 
    [SerializeField] private Text SoulsValue;
    [SerializeField] private Text LostValue;
    [SerializeField] private Text EMBOValue;
    [SerializeField] private Text PerSoulValue;

    public void ChangeSoulsValue(float souls)
    {
        SoulsValue.text = "SOULs: " + souls.ToString();
    }

    public void ChangeLostValue(int lost)
    {
        LostValue.text = "Lost: " + lost.ToString();
    }
    
    public void ChangeEMBOValue(int EMBOs)
    {
        EMBOValue.text = "EMBOs: " + EMBOs;
    }

    public void ChangePerSoulValue(int perSoul)
    {
        PerSoulValue.text = perSoul.ToString() + "£ per SOUL";
    }
}