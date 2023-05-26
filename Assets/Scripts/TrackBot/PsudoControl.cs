using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PsudoControl : MonoBehaviour
{

    float dir=0;
    float rdir=0;
    public float fspeed;
    public float rspeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void wheel(int L, int R) {

        if (L == 0 && R == 0) {

            dir = 0;
            rdir = 0;
        } else if (L > 0 && R > 0) {
            //Forward

            dir = 1;
            rdir = 0;
        } else if (L < 0 && R > 0) {
            //Right
            dir = 0;
            rdir = 1;
            

        } else if (L > 0 && R < 0) {
            //Left
            dir = 0;
            rdir = -1;


        } else if (L < 0 && R < 0) {
            // Back
            dir = -1;
            rdir = 0;

        }





    }




    // Update is called once per frame
    void Update()
    {


        transform.Translate(new Vector3(0, 0, dir * fspeed));
        transform.Rotate(new Vector3(0, rspeed*rdir, 0));



    }


}
