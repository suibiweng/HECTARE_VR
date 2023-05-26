using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaManager : MonoBehaviour
{


    public udpScriptSimple RoomBaControl;
    public bool RobotForward=true;


    // Start is called before the first frame update
    void Start()
    {
        
    }


   public void GenerateRock(){
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space)){

                RoomBaControl.RandomMoveFowrad();

        }*/

/*
        if(Input.GetKeyDown(KeyCode.A)){

                RoomBaControl.MoveBack();
        

        }*/

        
    }



    public void RoombaMove(){
        if(RobotForward){

 RoomBaControl.RandomMoveFowrad();
 RobotForward=false;

        }else{

  RoomBaControl.MoveBack();

   RobotForward=true;

        }



    }

  
}
