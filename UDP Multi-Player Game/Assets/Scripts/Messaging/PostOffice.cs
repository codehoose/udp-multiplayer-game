using System;
using System.Collections.Generic;

class PostOffice : SingletonBehaviour<PostOffice>
{
    private Dictionary<Type, List<IReceiver>> _receivers = new Dictionary<Type, List<IReceiver>>();

    private Stack<IUdpMessage> _messages = new Stack<IUdpMessage>();

    void Update()
    {
        while (_messages.Count > 0)
        {
            var message = _messages.Pop();
            List<IReceiver> receivers = null;
            if (_receivers.TryGetValue(message.GetType(), out receivers))
            {
                foreach (var r in receivers)
                {
                    r.Receive(message);
                }
            }
        }
    }

    public void Register(Type type, IReceiver receiver)
    {
        List<IReceiver> receivers = null;
        if (!_receivers.TryGetValue(type, out receivers))
        {
            _receivers[type] = new List<IReceiver>();
            receivers = _receivers[type];
        }

        if (!receivers.Contains(receiver))
        {
            receivers.Add(receiver);
        }
    }

    public void Post(IUdpMessage message)
    {
        _messages.Push(message);
    }
}
