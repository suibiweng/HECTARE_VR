using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public MeshRenderer Rockrenderer,Bumprender;
    public ParticleSystem hintParticle;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show(){
        hintParticle.Play();

         Rockrenderer.enabled=true;
         
         Bumprender.enabled=true;


    }

    public void Hide(){

         hintParticle.Stop();
         Rockrenderer.enabled=false;
         Bumprender.enabled=false;



    }
}
