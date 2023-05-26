using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBa2 : MonoBehaviour
{
	public float step=2;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,5*step,0);
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetMouseButtonDown(0)) { 
		if(transform.position.x == 0 && transform.position.y == 5*step){
			transform.position = new Vector3(6*step,0,0);
		}
		else if(transform.position.x == 6*step && transform.position.y == 0){
		transform.position = new Vector3(6*step,9*step,0);
		}
		else if(transform.position.x == 6*step && transform.position.y == 9*step){
		transform.position = new Vector3(0,5*step,0);
		}
	}
        
    }
}
