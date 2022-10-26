using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomBAStatus{
Moving,
Stop
}

public enum MissonStatus{

PlaceTheRock,
MoveBacktoBase


}



public class RoomBA : MonoBehaviour
{
    public Rock rock;
    Manager manager;
    public RoomBAStatus status;
    public MissonStatus misson; 
    // Start is called before the first frame update
    void Start()
    {
        manager=FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update(){

    if(status== RoomBAStatus.Stop){
            if(misson== MissonStatus.PlaceTheRock){

                ShowTheRock();

            }else if( misson== MissonStatus.MoveBacktoBase)
            {

                OpenTheCollector();
                 
            }
        
        }else{

            


        }
    
    
       if(status== RoomBAStatus.Stop){
         if(misson== MissonStatus.MoveBacktoBase){
            if(Vector3.Distance(rock.transform.position,transform.position)<0.3f){
                 manager.can.CanOpen(false);
                 rock.Hide();

            }
         }
       }


       if(status== RoomBAStatus.Stop){
          if(misson== MissonStatus.PlaceTheRock){
             if(Vector3.Distance(rock.transform.position,transform.position)>0.5f){
                  manager.RoomBaControl.MoveBack();
             }
            }
        }
    
    
    
    

    }


    void ShowTheRock(){


    rock.show();

}

void OpenTheCollector(){

    manager.can.CanOpen(true);


} 

}





    
    
    
    
    












   


        
    






   
   
   



