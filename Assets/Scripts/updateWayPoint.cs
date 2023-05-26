using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateWayPoint : MonoBehaviour
{
   public SimulationManager zigzag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        zigzag= GameObject.FindObjectOfType<SimulationManager>();


        if(Input.GetKeyDown(KeyCode.Space)){

            zigzag.TriggerNextPoint();

        }

        
        
    }
}
