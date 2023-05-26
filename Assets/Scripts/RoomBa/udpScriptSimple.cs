using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
 
public class udpScriptSimple : MonoBehaviour
{
 
   // Simple version of UDP sending script
   // Hardcode IP and port of receiver, no error checking on send
   // Sample string and number list messages you can send with keystrokes, for testing
 
  IPEndPoint remoteEndPoint;
  UdpClient client;

  public RoomBA roomBA;

  public string IP="192.168.0.100";
  public int Port=8585;
  
 
  public float step = 2;
  public string commands="";
  public string strMessage1 = "hello world";
  public string strMessage2 = "how are you";
  public int[] numberMessage = { 0, 10, 50, 100, 150, 200, 250, 255 };
 
   void Start()
   {
     // IP and port
    remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
    client = new UdpClient();
   }

      int steps;

   public void RandomMoveFowrad(){

    
    string cmd="";
    steps =5;
cmd=steps+"F";

roomBA.misson=MissonStatus.isTarget;
roomBA.status=RoomBAStatus.Moving;


    print(cmd);
      sendString(cmd);


   }
    public void MoveBack(){
    string cmd="";
  
      /* 
        string cmd="";

      for(int i=0;i<steps;i++){
          cmd+='B';
    } 
*/
cmd=steps+"B";
    print(cmd);
      sendString(cmd);



roomBA.misson=MissonStatus.isBase;
roomBA.status=RoomBAStatus.Moving;

     
     
    }





 
   void Update()
   {
    


   // int steps =Random.Range(1,5);

  
 


     if (Input.GetKeyDown(KeyCode.E)) // C for "Continuous" messages, one per frame while key held down
     {
      sendString("E\n");
     
     }

  if (Input.GetKeyDown(KeyCode.Q)) // C for "Continuous" messages, one per frame while key held down
     {
      sendString("T\n");
     
     }

       if (Input.GetKeyDown(KeyCode.I)) // C for "Continuous" messages, one per frame while key held down
     {
      sendString("I\n");
     }
 
   }
 
  public void RoombaMove(string commands)
  {
    print(commands);
    for (int i = 0; i < commands.Length; i++)
		{
		   if(commands[i] == 'W'){
        sendString("F\n");
		  // 	transform.position=transform.position+new Vector3(0,step,0);
		   }
		   else if(commands[i] == 'B'){
		   sendString("B\n");
		  // 	transform.position=transform.position+new Vector3(0,-step,0);
		   }
		   else if(commands[i] == 'L'){
		   sendString("L\n");
		   //	transform.position=transform.position+new Vector3(-step,0,0);
		   }
		   else if(commands[i] == 'R'){
		   sendString("R\n");
		  // 	transform.position=transform.position+new Vector3(step,0,0); 
		   }
		}
  }
   // Method to send string messages
   public void sendString(string message)
   {
     byte[] stringList = Encoding.UTF8.GetBytes(message);
    client.Send(stringList, stringList.Length, remoteEndPoint);
    print("message " + message + " sent to " + remoteEndPoint);
   }
 
   // Method to send an int[] array, like texture RGB values, etc
   public void sendNumberList(int[] message)
   {
     byte[] intList = new byte[message.Length];
 
     // There may be a way to convert the whole int array at once instead of looping through it
     // Tried Buffer.blockCopy, but haven't gotten it to work yet
     // https://forum.unity.com/threads/convert-int-array-to-byte-array-all-at-once.1077113/#post-6947678
     for (int i = 0; i < message.Length; i++)
     {
      intList[i] = ((byte)message[i]);
     }
 
    client.Send(intList, intList.Length, remoteEndPoint);
    print("message " + message + " sent to " + remoteEndPoint);
   }
}
 
