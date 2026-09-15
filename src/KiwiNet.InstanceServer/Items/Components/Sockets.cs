using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Items.Components
{
    public enum SocketColor : byte
    {
        Invalid,
        Red,
        Green,
        Blue,
        White,
    }

    public class SocketData
    {
        public SocketColor Color { get; set; }
        public Item Item { get; set; }

        public void Serialize(NetworkConnection connection)
        {
            connection.Write((byte)Color);
            connection.Write(Item != null);
            Item?.Serialize(connection);
        }
    }

    public sealed class Sockets : ItemComponent
    {
        public List<SocketData> SocketList { get; } = new();
        public List<byte> LinkCounts { get; } = new();

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(SocketList.Count);
            foreach (SocketData socketData in SocketList)
                socketData.Serialize(connection);

            // Link counts must sum to the number of sockets (e.g. in a six-socket item: 2+2+2 = triple two-link, 4+2 = four-link and two-link, 6 = six-link)
            connection.Write(LinkCounts.Count);
            foreach (byte link in LinkCounts)
                connection.Write(link);
        }

        public SocketData GetSocket(int index)
        {
            if (index < 0 || index >= SocketList.Count)
                return null;

            return SocketList[index];
        }
    }
}
