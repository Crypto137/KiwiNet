namespace KiwiNet.Core.Network
{
    public interface INetworkSerializable
    {
        public void Serialize(NetworkConnection connection);
    }
}
