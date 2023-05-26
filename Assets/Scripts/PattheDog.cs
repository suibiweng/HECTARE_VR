using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PattheDog : MonoBehaviour
{

    public DogBehaviourSpot dogspot;
    public CmdCallSpot spot;

    public float DelaySeconds=3f;

        bool TouchOne=false;

    // Start is called before the first frame update
    void Start()
    {
        spot = GameObject.FindObjectOfType<CmdCallSpot>().GetComponent<CmdCallSpot>();
        
        spot.StartBat();
        print("in start");

        dogspot=FindObjectOfType<DogBehaviourSpot>().GetComponent<DogBehaviourSpot>();
        //spot.Task(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision other) {
        print("Touch");
        if(other.gameObject.tag =="rightHand" || other.gameObject.tag =="lefttHand" ){
            if(!TouchOne)
                {
                    TouchOne=true;
                    print("hand dog collision");
                    StartCoroutine(DelayTaskCall(DelaySeconds));

                    if(dogspot!=null) dogspot.updateWayPoint();

                 //   spot.Task(1);
                }

        }  
        
    }




     void OnCollisionExit(Collision other) {
        print("Touch2");
        if(other.gameObject.tag =="rightHand" || other.gameObject.tag =="lefttHand" ){
        TouchOne=false;
        }
        
    }

    IEnumerator DelayTaskCall(float DelaySeconds ){
        yield return new WaitForSeconds(DelaySeconds);

        if(spot.currentTask==CurrentTask.AtoB){

            spot.runTask((int)CurrentTask.BtoA);
        }

           if(spot.currentTask==CurrentTask.BtoA){

            spot.runTask((int)CurrentTask.AtoB);
        }

        




    }


    
}
