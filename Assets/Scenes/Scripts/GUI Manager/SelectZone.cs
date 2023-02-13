using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectZone : MonoBehaviour
{
    private Button button;
    [SerializeField] private string SelectName;
    private ChangeRemoteValues CSString;
    [SerializeField] private  ZoneType zone_type;

    // Luryann change UI
    [SerializeField] private uint zone_cost;
    [SerializeField] private EconomyManager _economyManager;
    private int current_cost;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (SelectName.Length == 0)
        { 
            SelectName = gameObject.transform.name;
        }

        CSString = FindObjectOfType<ChangeRemoteValues>();
        _economyManager = FindObjectOfType<EconomyManager>();
        

        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        CSString.ChangeSelection(SelectName);
        SelectionHandler.Instance.current_zoning_type = zone_type;
        SelectionHandler.Instance.changeZoneType(zone_type);
        //_economyManager.updateCost(zone_cost);
    }
}
