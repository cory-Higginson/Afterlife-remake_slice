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

    [SerializeField] private GameObject prefabs;
    
    
    
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
        var tier = prefabs.GetComponent<TierBuilding>();
        var tier2 = tier.NextUpgrade;
        var tier3 = tier2.NextUpgrade;
        var tier4 = tier3;
        var tier5 = tier3;
        if (tier.NextUpgrade.NextUpgrade.NextUpgrade != null)
        {
            tier4 = tier3.NextUpgrade;
            if (tier4.NextUpgrade != null)
            {
                tier5 = tier4.NextUpgrade;
            }
        }
        
        CSString.ChangeSelection(SelectName);
        window.MakeActive( button.transform,tier.TierName,tier2.TierName,tier3.TierName,tier4.TierName,tier5.TierName,
            tier.cost, tier2.cost, tier3.cost, tier4.cost, tier5.cost);
    }
}
