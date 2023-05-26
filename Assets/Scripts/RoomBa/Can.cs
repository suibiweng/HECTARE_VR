using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();

    }

    public void CanOpen(bool open){

        animator.SetBool("Open",open);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
