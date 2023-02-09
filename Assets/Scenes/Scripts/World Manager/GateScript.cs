using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public GameObject soul;

    public bool connected = false;
    private float timer = 0;
    private float max_timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            timer += Time.deltaTime;
        }

        if (timer >= max_timer)
        {
            Instantiate(soul, this.transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
