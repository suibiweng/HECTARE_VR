using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class CalculateMovement : MonoBehaviour
{
    IPEndPoint remoteEndPoint;
    UdpClient client;
    public GameObject robot;
    public GameObject target;
    private Vector3 r_pos;
    private Vector3 t_pos;
    private float x_rot;
    private float y_rot;
    // public GameObject robot = GameObject.Find("Rumba-1");
    // public GameObject target = GameObject.Find("Rumba-2");

    public class R_Data
    {
        public float dist;
        public float angleDiff;

        public R_Data(float d, float a)
        {
            angleDiff = a;
            dist = d;
        }
    }

    public class DirData
    {
        public float diff;
        public string dir;

        public DirData(string dir, float diff)
        {
            dir = dir;
            diff = diff;
        }
    }

    public class CommandData
    {
        public int left;
        public int right;

        public CommandData(int left, int right)
        {
            left = left;
            right = right;
        }
    }

    public class RvoData
    {
        public float x;
        public float y;
        public float dist;
        public float diff;
        public float angleDiff;

        public RvoData(float x, float y,float dist, float diff,float angleDiff)
        {
            x = x;
            y = y;
            dist=dist;
            diff = diff;
            angleDiff = angleDiff;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.104"), 8585);
    	client = new UdpClient();
        robot = GameObject.Find("Roomba");
        target = GameObject.Find("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            move();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            stop();
        }
    }

    R_Data calculate(){
        robot = GameObject.Find("Roomba");
        target = GameObject.Find("Target");
        r_pos = robot.transform.position;
        t_pos = target.transform.position;
        float dx = r_pos.x - t_pos.x;
        float dy = r_pos.y - t_pos.y;
        double dist = Math.Sqrt(dx*dx + dy*dy);
        float distance = (float) dist;
        double rot_rad = Math.Atan2(dy,  dx);
        double rot_deg = (180/Math.PI)*rot_rad;
        float ang = robot.transform.eulerAngles.z - target.transform.eulerAngles.z; // not sure which angle diff is required
        R_Data myObject = new R_Data(distance,ang);
        Debug.Log("dist"+distance+"ang-"+ang+"rot_deg="+rot_deg);
        return myObject;
    }



    RvoData getRvoVelocity(){
        robot = GameObject.Find("Roomba");
        target = GameObject.Find("Target");

        int acceleration = 1;
        int avoidanceTendency = 10;
  
        int accel = acceleration;
        int w = avoidanceTendency;

        float prefSpeed = 0.5F;

        r_pos = robot.transform.position;
        t_pos = target.transform.position;

        float robotAngle = robot.transform.eulerAngles.z;
        float targetAngle = target.transform.eulerAngles.z;

        float prefVx = r_pos.x - t_pos.x;
        float prefVy = r_pos.y - t_pos.y;
        float dist = (float) Math.Sqrt(prefVx*prefVx + prefVy*prefVy);
        if(dist>1){
            prefVx *= prefSpeed / dist ;
            prefVy *= prefSpeed / dist ;
        }
        float rvoVx = prefVx;
        float rvoVy = prefVy;

        double dir = Math.Atan2(rvoVx,  rvoVy)*(180/Math.PI);
        dir = (-dir + 180) % 360;

        double diff = Math.Min((360) - Math.Abs(robotAngle - dir), Math.Abs(robotAngle - dir));

        if (robotAngle - dir < 0 && Math.Abs(robotAngle - dir) > 180) {
            diff = -diff;
        }
        if (robotAngle - dir > 0 && Math.Abs(robotAngle - dir) < 180) {
            diff = -diff;
        }
        diff = (diff + 360) % 360;
  
        float angleDiff = targetAngle - (robotAngle + 90);
        angleDiff = (angleDiff + 180) % 180;

        float diff1 = (float) diff;

        RvoData rvoObj = new RvoData(rvoVx,rvoVy,dist,diff1,angleDiff);
        return rvoObj ;
    }

    DirData getDirection(float diff,float threshold) {
        Debug.Log("In get direction diff-"+diff+"diff-"+threshold);
    if (0 <= diff && diff < threshold) {
        DirData direction = new DirData("backward", diff);
        Debug.Log("1-"+"dir"+direction.dir+" "+"diff-"+direction.diff);
        return direction;
    }
    if (threshold <= diff && diff < 90) {
        Debug.Log("2");
        DirData dir = new DirData("right", diff);
        return dir;
      //return { dir: 'right', diff: diff }
    }
    if (90 <= diff && diff < 180 - threshold) {
        Debug.Log("3");
        DirData dir = new DirData("left", 180 - diff);
        return dir;
      //return { dir: 'left', diff: 180 - diff }
    }
    if (180 - threshold <= diff && diff < 180 + threshold) {
        Debug.Log("4");
        DirData dir = new DirData("forward", 180 - diff);
        return dir;
      //return { dir: 'forward', diff: 180 - diff }
    }
    if (180 + threshold <= diff && diff < 270) {
        Debug.Log("5");
        DirData dir = new DirData("right", diff - 180);
        return dir;
      //return { dir: 'right', diff: diff - 180 }
    }
    if (270 <= diff && diff < 360 - threshold) {
        Debug.Log("6");
        DirData dir = new DirData("left", 360 - diff);
        return dir;
      //return { dir: 'left', diff: 360 - diff }
    }
    if (360 - threshold <= diff && diff <= 360) {
        Debug.Log("7");
        DirData dir = new DirData("backward", diff - 360);
        return dir;
      //return { dir: 'backward', diff: diff - 360 }
    }
    DirData dummy = new DirData("stop", 0);
    Debug.Log("8");
    return dummy;
  }

    // to stop the roomba
    void stop(){
        CommandData command = new CommandData(0,0);
        sendString(JsonUtility.ToJson(command)); 
    } 

    void move(){
        int distThreshold = 30;
        int dirThreshold = 10;
        int angleThreshold = 5;
        
        int sleepTime = 30;
        
        int dt = 1;

        robot = GameObject.Find("Roomba");
        target = GameObject.Find("Target");

        R_Data res = calculate();
        RvoData rvo = getRvoVelocity();

        float angleDiff = (360 + res.angleDiff) % 360;

        DirData calc = getDirection(rvo.diff, dirThreshold);
        string dir = calc.dir;
        float diff = calc.diff;
        Debug.Log("res.angleDiff-"+res.angleDiff+"rvo.diff-"+rvo.diff);
        Debug.Log("diff-"+diff+"dir-"+dir);
        // float base = Math.min(60, res.dist+50);
        // float Kd = Math.min(8, (res.dist + 200) / 100);
        // int param = 100;
        // int command;
        int val = 150;

        if(res.dist < distThreshold ){
          // console.log('now adjust angle')
          //console.log(Math.min(angleDiff, 360 - angleDiff))
          if (angleDiff < angleThreshold) {
            dir = "stop";
          } else {
            dir = "left";
          }
          if (360 - angleDiff < angleThreshold) {
            dir = "stop";
          } else {
            dir = "right";
          }
        }

        CommandData command = new CommandData(0,0);

        switch (dir) {
          case "forward":
            command = new CommandData(val,val);
            //command = { left: val, right: val }
            break;
          case "backward":
            command = new CommandData(-val,-val);
            //command = { left: -val, right: -val }
            break;
          case "left":
            command = new CommandData(-val,val);
            //command = { left: -val, right: val }
            break;
          case "right":
            command = new CommandData(val,-val);
            //command = { left: val, right: -val }
            break;
          case "stop":
            command = new CommandData(0,0);
            //command = { left: 0, right: 0 }
            string jsonString = JsonUtility.ToJson(command);
            sendString(jsonString);   // we don't need to send ip addr and port 

            // let message = { command: command, ip: this.ips[id], port: this.port }
            // this.socket.emit('move', JSON.stringify(message))
            // if (ok >= 5) {
            //   return
            // }
            break;
        }

        sendString(JsonUtility.ToJson(command)); 

    }

    public void sendString(string message)
   { 
    //i=i+1;
     byte[] stringList = Encoding.UTF8.GetBytes(message);
    client.Send(stringList, stringList.Length, remoteEndPoint);
    print("message " + message + " sent to "+ remoteEndPoint);
   }
}
