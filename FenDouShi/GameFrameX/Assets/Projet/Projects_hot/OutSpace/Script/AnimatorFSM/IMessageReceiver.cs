public enum MessageType2
{
    DAMAGED,
    DEAD,
    RESPAWN,
    //Add your user defined message type after
}
public interface IMessageReceiver
{
    void OnReceiveMessage(MessageType2 type, object sender, object msg);
}