using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    public Transform obj;
    public Vector3 offset;

    public Transform RightHandTracker;
    public Transform RightHhandRig;


    public Transform LeftHandTracker;
    public Transform LeftHandRig;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position=obj.position+offset;
        transform.eulerAngles=new Vector3(transform.eulerAngles.x,obj.eulerAngles.y,transform.eulerAngles.z);
     


        RightHhandRig.position=RightHandTracker.position;
        RightHhandRig.rotation=RightHandTracker.rotation;

        LeftHandRig.position=LeftHandTracker.position;
        LeftHandTracker.rotation=LeftHandTracker.rotation;



           }
}
