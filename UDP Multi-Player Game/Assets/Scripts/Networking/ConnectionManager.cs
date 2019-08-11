using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    private UDPConnection _connection;
    private IMessageSerializer _serializer;
    private IMessageFactory _messageFactory;
    private Dictionary<string, ConnectedClient> _clients = new Dictionary<string, ConnectedClient>();

    public int port = 54123;

    void Awake()
    {
        _connection = new UDPConnection(port);
        _connection.Listen(CommandReceived, 5000);
        _serializer = new MessageSerializer();
        _messageFactory = new MessageFactory();

        RegisterMessageHandlers();
    }

    private void CommandReceived(IPEndPoint remote, byte[] data)
    {
        var message = _serializer.ParseMessage(_messageFactory, remote, data);
        if (message != null)
        {
            PostOffice.Instance.Post(message);
        }

        _connection.Listen(CommandReceived, 5000); // This must be the last line!!
    }

    private void RegisterMessageHandlers()
    {
        PostOffice.Instance.Register(typeof(WelcomeMessage), new CallbackReceiver<WelcomeMessage>(msg =>
        {
            _clients[msg.Sender.ToString()] = new ConnectedClient(msg.Sender);
        }));
    }
}
