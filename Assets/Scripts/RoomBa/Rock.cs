using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public MeshRenderer Rockrenderer;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show(){
  

         Rockrenderer.enabled=true;
         
      


    }

    public void Hide(){

        
         Rockrenderer.enabled=false;
    



    }
}
