using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_cam : MonoBehaviour
{
    // Start is called before the first frame update

    private Button button;
    [SerializeField] private bool zoomIn;
    private CameraManager _cameraManager;
    
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        _cameraManager = FindObjectOfType<CameraManager>();
        
        button.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        if (zoomIn)
        {
            _cameraManager.ZoomView(-10.0f);
        }
        else
        {
            _cameraManager.ZoomView(10.0f);
        }
    }
}
