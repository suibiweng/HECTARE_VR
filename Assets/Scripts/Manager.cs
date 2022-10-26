using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public udpScriptSimple RoomBaControl;
    public RoomBA roomba;
    public Can can;
    // Start is called before the first frame update
    void Start()
    {
        
    }


   public void GenerateRock(){
        RoomBaControl.RandomMoveFowrad();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){

                GenerateRock();
                can.CanOpen(false);

        }


        if(Input.GetKeyDown(KeyCode.A)){

                RoomBaControl.MoveBack();
                can.CanOpen(true);

        }

        
    }

  
}
