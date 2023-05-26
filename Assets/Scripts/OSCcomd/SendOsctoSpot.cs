using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOsctoSpot : MonoBehaviour
{
    public OSC osc;
    // Start is called before the first frame update
    void Start()
    {


    }


    public void oscSend(int i){


        OscMessage msg= new OscMessage();

        msg.address="/some_addr";
        
        switch(i){
            case 0:

             msg.values.Add("sit");

            break;



            case 1:

             msg.values.Add("a2b");

            break;



            case 2:

             msg.values.Add("b2a");

            break;



        }
       

        osc.Send(msg);










    } 



    // Update is called once per frame
    void Update()
    {
        
    }
}
