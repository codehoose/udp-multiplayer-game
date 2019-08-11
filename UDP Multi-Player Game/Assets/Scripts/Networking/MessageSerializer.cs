using System;
using System.Net;
using System.Text;

class MessageSerializer : IMessageSerializer
{
    private static readonly string HEADER_VERIFICATION = "chs";

    public byte[] IsValid(byte[] data)
    {
        if (data.Length < 3)
        {
            return null;
        }

        var sub = new byte[3];
        Array.Copy(data, 0, sub, 0, 3);
        var head = Encoding.ASCII.GetString(sub);
        if (head == HEADER_VERIFICATION)
        {
            var split = new byte[data.Length - 3];
            Array.Copy(data, 3, split, 0, data.Length - 3);
            return split;
        }

        return null;
    }
    
    public DataReader CreateReader(byte[] data)
    {
        return new DataReader(data);
    }

    public byte[] SerializeMessage(IUdpMessage message)
    {
        var writer = new DataWriter();
        writer.Write(HEADER_VERIFICATION, false);
        var cmdName = message.GetType().Name;
        cmdName = cmdName.Substring(0, cmdName.Length - "Message".Length);
        writer.Write(cmdName);
        message.Serialize(writer);
        var bytes = writer.Finalize();
        return bytes;
    }

    public IUdpMessage ParseMessage(IMessageFactory factory, IPEndPoint remote, byte[] data)
    {
        // Check that the data is valid
        var payloadData = IsValid(data);
        if (payloadData != null)
        {
            var reader = new DataReader(payloadData);
            var command = reader.GetString();

            var message = factory.CreateMessage(command, remote, reader);
            reader.Close();
            return message;
        }

        return null;
    }
}