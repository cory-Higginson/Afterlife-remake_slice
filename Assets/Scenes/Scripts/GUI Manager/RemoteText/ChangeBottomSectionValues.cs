using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBottomSectionValues : MonoBehaviour
{
    [SerializeField] private Text SoulsValue;
    [SerializeField] private Text LostValue;
    [SerializeField] private Text EMBOValue;
    [SerializeField] private Text PerSoulValue;

    public void ChangeSoulsValue(int souls)
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