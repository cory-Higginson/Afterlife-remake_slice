using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindow : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject window;
    
    [SerializeField] private bool windowEnabled;


    // Start is called before the first frame update
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(TaskOnClick);
        
        window.SetActive(false);
        windowEnabled = false;
    }

    void TaskOnClick()
    {
        if (windowEnabled)
        {
            window.SetActive(false);
            windowEnabled = false;
        }
        else
        {
            window.SetActive(true);
            windowEnabled = true;
        }
    }
}
