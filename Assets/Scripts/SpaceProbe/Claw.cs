using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public Animator animator;
    public GameObject TriggerPoint;
    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
      TriggerPoint= GameObject.Find(transform.parent.name+"/"+pointName);

 animator.SetBool("Catch",!TriggerPoint);
/*
      if(!TriggerPoint)
       
        else{

          animator.SetBool("Catch",true);
        }
*/



  // animator.SetBool("Catch",!TriggerPoint.activeInHierarchy);


      //debug
      if(Input.GetKeyDown(KeyCode.A)){

        animator.SetBool("Catch",true);
      }  

      if(Input.GetKeyUp(KeyCode.A)){
 animator.SetBool("Catch",false);


      }

    }
}
