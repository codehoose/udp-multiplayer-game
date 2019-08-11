using System.Net;

interface IUdpMessage
{
    IPEndPoint Sender { get; }

    void Populate(IPEndPoint remote, DataReader reader);
    void Serialize(DataWriter writer);
}
