using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildWindowManager : MonoBehaviour
{
    private GameObject window;
    private Button[] buttons;

    private void Awake()
    {
        if (window == null)
        {
            window = gameObject;
        }

        buttons = GetComponentsInChildren<Button>();
        
        window.SetActive(false);
    }

    public void SwitchSelect()
    {
        foreach (Button butt in buttons)
        {
            butt.GetComponentInChildren<Image>().color = Color.white;
        }
    }

    public void MakeActive(Transform position, string one, string two, string three, string four, string five,
        uint costOne, uint costTwo, uint costThree, uint costFour, uint costFive)
    {
        window.SetActive(true);
        buttons[0].GetComponentInChildren<Text>().text = one;
        buttons[0].GetComponent<BuildingWindowButton>().cost = costOne;
        
        buttons[1].GetComponentInChildren<Text>().text = two;
        buttons[1].GetComponent<BuildingWindowButton>().cost = costTwo;
        
        buttons[2].GetComponentInChildren<Text>().text = three;
        buttons[2].GetComponent<BuildingWindowButton>().cost = costThree;
        
        if (buttons.Length >= 4)
        {
            buttons[3].GetComponentInChildren<Text>().text = four;
            buttons[3].GetComponent<BuildingWindowButton>().cost = costFour;
        }

        if (buttons.Length >= 5)
        {
            buttons[4].GetComponentInChildren<Text>().text = five;
            buttons[4].GetComponent<BuildingWindowButton>().cost = costFive;
        }
        

        window.transform.position = position.position;
    }

    public void Inactive()
    {
        window.SetActive(false);
    }
}

