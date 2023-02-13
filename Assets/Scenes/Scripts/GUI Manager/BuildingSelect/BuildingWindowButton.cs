using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingWindowButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Button button;
    [SerializeField] private BuildWindowManager window;
    [SerializeField] public int ButtonNumber;
    [SerializeField] public uint cost;
    private ChangeRemoteValues _remoteValues;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (window == null)
        {
            window = GetComponentInParent<BuildWindowManager>();
        }

        _remoteValues = FindObjectOfType<ChangeRemoteValues>();
        
        button.onClick.AddListener(TaskOnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        window.SwitchSelect();
        GetComponentInChildren<Image>().color = Color.cyan;
        _remoteValues.ChangeCostValue(cost);
    }

    void TaskOnClick()
    {
        Debug.Log(ButtonNumber);
        window.Inactive();
    }
}
