using UnityEngine;
using System.Collections.Generic;


public class DATA : MonoBehaviour
{
    [SerializeField] private GameObject a;


    private void Awake()
    {
        
        HardCodedData(a,1,1);
    }

    public void HardCodedData(GameObject buidling_to_be_built,int type,int size)
    {
        switch (type)
        {
            case 0:
            {
                buidling_to_be_built.AddComponent<GenericBuilding>();
                break;
            }
            case 1:
            {
                buidling_to_be_built.AddComponent<TierBuilding>();
                break;
            }
        }
    }
}