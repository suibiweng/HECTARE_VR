using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class MoveRoomba : MonoBehaviour
{
    IPEndPoint remoteEndPoint;
    UdpClient client;
    public String IP="192.168.0.102";
    public int port=8585;
    public GameObject robot;
    public GameObject target;
    private Vector3 r_pos;
    private Vector3 t_pos;
    private float x_rot;
    private float y_rot;
    //private int i=0;

    public class R_Data
    {
        public float angle;
        public float dist;

        public R_Data(float a, float d)
        {
            angle = a;
            dist = d;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    	// IP and port
    	remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.102"), port);
    	client = new UdpClient();
       // robot = GameObject.Find("Rumba-1");
        //target = GameObject.Find("Rumba-2");
     //   Debug.Log("target-"+target.transform.position);
    }


    public bool Rombaforwad=true;
    public void MoveBackandForward(){

        if(Rombaforwad){
            sendString("1");
            Rombaforwad=false;

        }else{

            sendString("2");
            Rombaforwad=true; 

        }



    }

    




    // Update is called once per frame
    void Update()
    { 
        //  int distance = 500;
        if(Input.GetKeyDown(KeyCode.A)){
            //moveForward(distance);
            //moveForward(distance);
            sendString("1"); // moveForward
        }
        if(Input.GetKeyDown(KeyCode.B)){
            //moveBackward(distance);
            sendString("2"); // moveBackward
        }
        if(Input.GetKeyDown(KeyCode.C)){
            //forwardZig(distance);
            sendString("3"); //forwardZigzag
        }
        if(Input.GetKeyDown(KeyCode.D)){
            sendString("4"); //backwardZigzag
        }


        /*

        if(Input.GetMouseButtonDown(0)) {
            robot = GameObject.Find("Rumba-1");
            r_pos = robot.transform.position;
            target = GameObject.Find("Rumba-2");
            
            Debug.Log("target in upd-"+target.transform.position);
            target.transform.position = new Vector3(10,10,0);
            r_pos = robot.transform.position;
            t_pos = target.transform.position;
            
            //x_rot = robot.transform.rotation.x - target.transform.rotation.x;
            //y_rot = robot.transform.rotation.y - target.transform.rotation.y;
    
            double dx = r_pos.x - t_pos.x;
            double dy = r_pos.y - t_pos.y;
            double dist = Math.Sqrt(dx*dx + dy*dy);
            Debug.Log("x_rot"+ x_rot+y_rot);
            double rot_rad = Math.Atan2(dy,  dx);
            double rot_deg = (180/Math.PI)*rot_rad;
            //float step = 5 * Time.deltaTime;
            //robot.transform.position = Vector3.MoveTowards(transform.position, t_pos, step);
            //Debug.Log("rot_deg"+rot_deg);
            float r = (float) rot_deg;
            float d = (float) dist;


            robot.transform.eulerAngles = new Vector3(r,0,0);
            robot.transform.position = new Vector3(d,0,0);
            string angle = rot_deg.ToString();
            string dt = d.ToString();
            //var data = new Dictionary<string, string>(){{}}
            string a = "angle:"+angle;
            string ds = "dist:"+dt;
            string dataToSend = a+","+ds;
            //sendString(dataToSend+"\n");
            Debug.Log("dataToSend-"+dataToSend);
            //sendString(a+"\n");


            R_Data myObject = new R_Data(0,0);
            // myObject.angle = r;
            // myObject.dist = d;
            Debug.Log("myObject-"+myObject.angle +','+ myObject.dist);
            string jsonStringTrial = JsonUtility.ToJson(myObject);
            sendString(jsonStringTrial);

    	
    }*/
    }

    public string getRData(int ang, int dist){

        R_Data obj = new R_Data(ang, dist);
        string jsonString = JsonUtility.ToJson(obj);
        return jsonString;
    }

    public void moveForward(int dist){
        //R_Data mfObj = new R_Data(0,dist);
        //string jsonStringmf = JsonUtility.ToJson(mfObj);
        string jsonStringmf = getRData(0,dist);
        sendString(jsonStringmf);

    }

    public void moveBackward(int dist){
        //R_Data mbObj = new R_Data(0,-dist);
        string jsonStringmb = getRData(0,-dist);
        sendString(jsonStringmb);

    }

    public void forwardZig(int dist){
        //R_Data fzObj1 = new R_Data(45, dist);
        string jsonStringfz1 = getRData(45,dist);
        sendString(jsonStringfz1);
        //R_Data fzObj2 = new R_Data(0, dist);
        string jsonStringfz2 = getRData(0,dist);
        sendString(jsonStringfz2);
        //sendString(jsonStringfz2);
        //R_Data fzObj3 = new R_Data(90, dist);
        string jsonStringfz3 = getRData(110,dist);
        sendString(jsonStringfz3);
        //R_Data fzObj4 = new R_Data(0, dist);
        string jsonStringfz4 = getRData(0,500);
      
        sendString(jsonStringfz4);
        sendString(jsonStringfz4);
        sendString(jsonStringfz4);

         sendString(jsonStringfz1);
        //sendData(jsonStringfz4);
        //sendData(jsonStringfz4);

    }

    public void sendData(string s){
        sendString(s);
    }

    public void backwardZig(int dist){
        //R_Data bzObj1 = new R_Data(180, dist);
        string jsonStringbz1 = getRData(180,dist);
        sendString(jsonStringbz1);
        //R_Data bzObj2 = new R_Data(0, dist);
        string jsonStringbz2 = getRData(0,dist);
        sendString(jsonStringbz2);
        //R_Data bzObj3 = new R_Data(270, dist);  //360-90=270
        string jsonStringbz3 = getRData(250,dist);
        sendString(jsonStringbz3);
        // bzObj4 = new R_Data(0, dist);
        string jsonStringbz4 = getRData(0,dist);
        sendString(jsonStringbz4);

    }

    public void sendString(string message)
   { 
    //i=i+1;
     byte[] stringList = Encoding.UTF8.GetBytes(message);
    client.Send(stringList, stringList.Length, remoteEndPoint);
    print("message " + message + " sent to "+ remoteEndPoint);
   }
    
        
 
}
