using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomBAStatus{
Moving,
Stop
}

public enum MissonStatus{

isTarget,
isBase



}



public class RoomBA : MonoBehaviour
{
   public Rock rock;
   public MeshRenderer Baserenderer ;
    RoombaManager manager;
    public RoomBAStatus status;
    public MissonStatus misson; 
    // Start is called before the first frame update
    void Start()
    {
        manager=FindObjectOfType<RoombaManager>();
    }

    // Update is called once per frame
    void Update(){

    if(status== RoomBAStatus.Stop){
            if(misson== MissonStatus.isBase){

                rock.show();
                 ShowTheBase();

            }else if( misson== MissonStatus.isTarget)
            {

                ShowTheBase();

               
                 
            }
        
        }else{

            Baserenderer.enabled=false;


        }
    
    
       if(status== RoomBAStatus.Stop){
         if(misson== MissonStatus.isTarget){
            if(Vector3.Distance(rock.transform.position,transform.position)<0.3f){
                
                Baserenderer.enabled=false;
                rock.Hide();
                manager.RoomBaControl.RandomMoveFowrad();


            }
         }
       }


       if(status== RoomBAStatus.Stop){
          if(misson== MissonStatus.isBase){
           
             if(Vector3.Distance(rock.transform.position,transform.position)>0.5f){
                  manager.RoomBaControl.MoveBack();
                  Baserenderer.enabled=false;


             }
            }
        }
    
    
    
    

    }


    void ShowTheRock(){


    rock.show();

}

void  ShowTheBase(){


    Baserenderer.enabled=true;

  //  manager.can.CanOpen(true);


} 

}





    
    
    
    
    












   


        
    






   
   
   



