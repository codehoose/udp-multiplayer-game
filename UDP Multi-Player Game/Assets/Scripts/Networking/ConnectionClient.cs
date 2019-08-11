using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class ConnectionClient : MonoBehaviour
{
    private UDPConnection _connection;

    public int localPort = 54124;
    public int serverPort = 54123;

    void Awake()
    {
        _connection = new UDPConnection(localPort);
    }

    public void SendString(string message)
    {
        var serializer = new MessageSerializer();
        //var debug = new DebugLogMessage
        //{
        //    Message = message
        //};

        var welcome = new WelcomeMessage();
        var bytes = serializer.SerializeMessage(welcome);
        _connection.Send(new IPEndPoint(IPAddress.Broadcast, serverPort), bytes);
    }
}
