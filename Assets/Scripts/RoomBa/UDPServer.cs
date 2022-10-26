using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPServer : MonoBehaviour
{
    public string datain;
    public Manager manager;
    Thread receiveThread;
    UdpClient client;

    [SerializeField]
    private int port = 8585;

    [SerializeField]
    private bool PrintDebug = false;

    public static event Action<string> OnUDPMessage;

    public void Start()
    {
        init();
        OnUDPMessage += LogMessage;
    }

    private void LogMessage(string obj)
    {
        if (PrintDebug)
            Debug.Log("Received: " + obj);
        datain=obj;

        if (obj.StartsWith("finished")) {
          
            manager.roomba.status=RoomBAStatus.Stop;
            


        }


       





    }

    private void init()
    {
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));

        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true) {
            try {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                print (("Ping was sent from " + anyIP.Address.ToString() +
                             " on their port number " + anyIP.Port.ToString()));
             
                string text = Encoding.ASCII.GetString(data);
                OnUDPMessage(text);

            } catch (Exception err) {
                print(err.ToString());
            }
        }
    }



       public void broadcastTo(string cmd){
        var from = new IPEndPoint(0, 0);
        var data = Encoding.UTF8.GetBytes(cmd);
        client.Send(data, data.Length, "255.255.255.255", port);
    }


    



}