class MessagePayload
{
    public string MessageType { get; }

    public byte[] Payload { get; }

    public MessagePayload(string messageType, byte[] payload)
    {
        MessageType = messageType;
        Payload = payload;
    }
}
