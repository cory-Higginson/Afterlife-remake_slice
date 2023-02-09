using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindow : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject window;
    [SerializeField] private string SelectName;
    [SerializeField] private ChangeTopSectionValues CSString;
    
    private bool windowEnabled;
    
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(TaskOnClick);

        if (SelectName.Length == 0)
        {
            SelectName = GetComponentInChildren<Text>().text;
        }
        
        CSString = FindObjectOfType<ChangeTopSectionValues>();
        
        window.SetActive(false);
    }

    void TaskOnClick()
    {
        if (window.activeSelf)
        {
            window.SetActive(false);
        }
        else
        {
            window.SetActive(true);
            window.transform.SetAsLastSibling();
            CSString.ChangeSelection(SelectName);
        }
    }
}
