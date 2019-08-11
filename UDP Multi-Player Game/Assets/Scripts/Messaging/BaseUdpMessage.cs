using System.Net;

abstract class BaseUdpMessage : IUdpMessage
{
    public IPEndPoint Sender { get; protected set; }

    public void Populate(IPEndPoint remote, DataReader reader)
    {
        Sender = remote;
        OnPopulate(remote, reader);
    }

    protected virtual void OnPopulate(IPEndPoint remote, DataReader reader)
    {

    }

    public virtual void Serialize(DataWriter writer)
    {
        
    }
}

