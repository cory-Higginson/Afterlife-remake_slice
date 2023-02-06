using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindow : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject window;
    
    private bool windowEnabled;


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
            window.transform.SetAsLastSibling();
        }
    }
}
