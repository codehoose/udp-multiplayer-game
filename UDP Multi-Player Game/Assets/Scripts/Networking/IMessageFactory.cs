using System.Net;

interface IMessageFactory
{
    IUdpMessage CreateMessage(string messageName, IPEndPoint remote, DataReader reader);
}
