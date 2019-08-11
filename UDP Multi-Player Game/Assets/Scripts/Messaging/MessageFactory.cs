using System;
using System.Collections.Generic;
using System.Net;

class MessageFactory : IMessageFactory
{
    private Dictionary<string, Type> _messageTypes = new Dictionary<string, Type>();

    public MessageFactory()
    {
        foreach (var type in GetType().Assembly.GetTypes())
        {
            if (!type.IsAbstract && !type.IsInterface && type.GetInterface(typeof(IUdpMessage).Name) != null)
            {
                var name = type.Name.Substring(0, type.Name.Length - "Message".Length);
                _messageTypes[name] = type;
            }
        }
    }

    public IUdpMessage CreateMessage(string messageName, IPEndPoint remote, DataReader reader)
    {
        Type messageType = null;
        if (_messageTypes.TryGetValue(messageName, out messageType))
        {
            var message = (IUdpMessage)Activator.CreateInstance(messageType);
            message.Populate(remote, reader);
            return message;
        }

        return null;
    }
}