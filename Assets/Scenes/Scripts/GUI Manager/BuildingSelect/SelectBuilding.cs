using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string SelectName;
    [SerializeField] private BuildWindowManager window;
    [SerializeField] private ChangeRemoteValues CSString;
    
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (window == null)
        {
            window = FindObjectOfType<BuildWindowManager>();
        }

        SelectName = GetComponentInChildren<Text>().text;
        CSString = FindObjectOfType<ChangeRemoteValues>();
        
        button.onClick.AddListener(TaskOnClick);
    }
    
    
    void TaskOnClick()
    {
        CSString.ChangeSelection(SelectName);
        window.MakeActive(button.transform);
    }
}
