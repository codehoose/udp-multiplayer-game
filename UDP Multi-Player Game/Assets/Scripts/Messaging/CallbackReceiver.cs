using System;

class CallbackReceiver<T> : IReceiver<T> where T: class, IUdpMessage, new()
{
    private readonly Action<T> _callback;

    public CallbackReceiver(Action<T> callback)
    {
        _callback = callback;
    }

    public void Receive(T message)
    {
        _callback?.Invoke(message);
    }

    public void Receive(IUdpMessage message)
    {
        Receive(message as T);
    }
}