using UnityEngine;

class DebugMessageHandler : MonoBehaviour, IReceiver<DebugLogMessage>
{
    void Awake()
    {
        PostOffice.Instance.Register(typeof(DebugLogMessage), this);
    }

    public void Receive(DebugLogMessage message)
    {
        if (message != null)
        {
            Debug.Log(message.Message);
        }
    }

    public void Receive(IUdpMessage message)
    {
        Receive(message as DebugLogMessage);
    }
}
