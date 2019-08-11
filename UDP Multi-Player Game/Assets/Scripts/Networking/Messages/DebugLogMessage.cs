using System.Net;

class DebugLogMessage : BaseUdpMessage
{
    public string Message { get; set; }

    protected override void OnPopulate(IPEndPoint remote, DataReader reader)
    {
        Sender = remote;
        Message = reader.GetString();
    }

    public override void Serialize(DataWriter writer)
    {
        writer.Write(Message);
    }
}

