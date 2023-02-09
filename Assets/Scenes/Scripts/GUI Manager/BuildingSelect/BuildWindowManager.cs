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

    public void MakeActive(Transform position)
    {
        window.SetActive(true);

        window.transform.position = position.position;
    }

    public void Inactive()
    {
        window.SetActive(false);
    }
}

