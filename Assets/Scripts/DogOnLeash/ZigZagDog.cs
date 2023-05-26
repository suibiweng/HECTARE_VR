using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public enum DogMissionStatus{
    Sit,AtoB,BtoA

}

public enum Dogstatus{
    Sit,Moving


}


public class ZigZagDog : MonoBehaviour
{
    public AudioSource audioSource;
    Animator animator;

    public int MaskTasks=10;
    
    public Transform ModelTransform;

    public Text TasksCount,TasksFinished;
    public DogMissionStatus mission;

    public Dogstatus status;

    public float speed=0.01f;
    public GameObject [] RealTargets;
    public bool spotTracking,debug;
    public Transform Target;

    public CmdCallSpot SpotCall;
    public Vector3 modelPosition;


    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        StartCoroutine(getRealTargets());
    if(spotTracking)    {
        SpotCall.StartBat();
        modelPosition=ModelTransform.localPosition;


    }




    }

    IEnumerator getRealTargets(){
        yield return new WaitForSeconds(1f);
        RealTargets=GameObject.FindGameObjectsWithTag("RealTarget");
        switchTarget();



    }
public void Move(){

    status=Dogstatus.Moving;

}
public void sit(){

 status=Dogstatus.Sit;

}

public void Barking(){
    
  audioSource.Play();

}

public void spotAtoB(){
    StartCoroutine(DelaytoRun());
     switchTarget();
     audioSource.Play();
     currentTask++;

}

public void spotBtoA(){
     StartCoroutine(DelaytoRun());
     switchTarget();
     audioSource.Play();
     currentTask++;


    
}


    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Space)){

            status=Dogstatus.Moving;
            CallNextTask();
            switchTarget();
            audioSource.Play();

        }

     if(Input.GetKeyDown(KeyCode.T)){

        
        StartCoroutine(AutoRunforTimes(10,75));

        }

        




        
        switch (status){
            case Dogstatus.Sit:
                animator.SetBool("isWalking",false);
                animator.SetBool("isSitting",true);

               if(spotTracking){
                    
                  //  ModelTransform.localPosition= new Vector3(modelPosition.x,modelPosition.y,0f);

                }


            
            break;

            case Dogstatus.Moving:

                animator.SetBool("isWalking",true);
                animator.SetBool("isSitting",false);
                faceToTarget();
                MoveToTarget();


                if(spotTracking){
                    
            //  ModelTransform.localPosition= modelPosition;

                }

            
            break;


        }



        if(currentTask>=MaskTasks){

            TasksFinished.text="Done!!";
           // Application.LoadLevel(2);
        }
        else{

            TasksCount.text=""+currentTask;
        }




    
    
    }


    IEnumerator AutoRunforTimes(int times,int waitSeconds)

    {

        for(int i=0;  i<times/2; i++){
            status=Dogstatus.Moving;
            CallNextTask();
            yield return new WaitForSeconds(waitSeconds);





        }





    }

    void faceToTarget(){
          transform.LookAt( new Vector3(Target.position.x,transform.position.y,Target.position.z) );




    }

    int OscIndex;

    void switchTarget(){

       if(NeartoWhichTargets()==1){setMoveTarget(0);
       OscIndex=2;
       }


       if(NeartoWhichTargets()==0){setMoveTarget(1);
       OscIndex=1;
       }




    }


    public void MoveToTarget(){

        faceToTarget();

        if((Vector3.Distance(transform.position,
        Target.position)<nearRange)){




            if(!spotTracking)
            {

                status=Dogstatus.Sit;


            }else{

                StartCoroutine(DelaytoSit());
            }
                  

                  
                  
                  
                   NeartoWhichTargets();
                   switchTarget();
        }
      if(!spotTracking)  transform.Translate(new Vector3(0,0,speed));

        



    }

    IEnumerator DelaytoRun(){
        yield return new WaitForSeconds(15f);
        if(!debug)
status=Dogstatus.Moving;

    }

       IEnumerator DelaytoSit(){
        yield return new WaitForSeconds(8f);
           if(!debug)
status=Dogstatus.Sit;

    }



    void setMoveTarget(int targetIndex){

        Target=RealTargets[targetIndex].transform;

    }

    public float nearRange=1f;

    int NeartoWhichTargets(){

        if(Vector3.Distance(transform.position,
            RealTargets[0].transform.position)<nearRange)
        {

     
            return 0;

        }


        if(Vector3.Distance(transform.position,
        RealTargets[1].transform.position)<nearRange)
        {
          
            return 1;

        }



     
            return -1;

        



     }


    bool Touchonce;

    void OnCollisionEnter(Collision other) {
    if(other.gameObject.tag == "RightHand" ||other.gameObject.tag == "LeftHand"){
        if(!Touchonce && status==Dogstatus.Sit){
            Touchonce=true;
            
             
          

            


        }
     }

    }


    void CallNextTask(){


          StartCoroutine( ReciveTouch());







    }

    public int currentTask=0;
    IEnumerator ReciveTouch(){


        currentTask++;
        yield return new WaitForSeconds(1.5f);
        status=Dogstatus.Moving;

       if(spotTracking) SpotCall.runTask(OscIndex);


    }    



    void OnCollisionExit(Collision other) {
    if(other.gameObject.tag == "RightHand" ||other.gameObject.tag == "LeftHand"){

        Touchonce=false;
     }

    }

            


    
}
