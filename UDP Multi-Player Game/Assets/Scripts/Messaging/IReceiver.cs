interface IReceiver
{
    void Receive(IUdpMessage message);
}

interface IReceiver<T> : IReceiver where T: IUdpMessage
{
    void Receive(T message);
}
