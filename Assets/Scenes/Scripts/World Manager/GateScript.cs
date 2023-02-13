using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public GameObject Entityman;

    //public bool connected = true;
    private float timer = 0;
    private float max_timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        Entityman = GameObject.Find("Entity Manager");
    }

    // Update is called once per frame
    void Update()
    {
        //if (connected)
        //{
            timer += Time.deltaTime;
        //}

        if (timer >= max_timer)
        {
            //Instantiate(soul, this.transform.position, Quaternion.identity);
            Entityman.GetComponent<SoulManager>().AddSoul(SOULLocation.wandering,this.transform.position,this.GetComponentInParent<GridLocation>().grid_data.position,Quaternion.identity);
            timer = 0;
        }
    }
}
