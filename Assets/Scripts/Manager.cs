using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpotTasks{
    AtoB=0,
    BtoA=1,

    Idle=2



}



public class RoobaManager : MonoBehaviour
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
               

        }


        if(Input.GetKeyDown(KeyCode.A)){

                RoomBaControl.MoveBack();
        

        }

        
    }

  
}
