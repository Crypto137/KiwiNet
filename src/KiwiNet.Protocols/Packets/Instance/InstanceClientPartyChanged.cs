using KiwiNet.Core.Network;

namespace KiwiNet.Protocols.Packets.Instance
{
    public readonly struct PartyChangedEntry(string field0, byte field1)
    {
        public readonly string Field0 = field0;
        public readonly byte Field1 = field1;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write(Field1);
        }
    }

    public sealed class InstanceClientPartyChanged : Packet
    {
        public uint Field0 { get; }
        public List<PartyChangedEntry> Entries { get; } = new();

        public InstanceClientPartyChanged() : base(PacketId.InstanceClientPartyChangedId)
        {
        }

        public override void Serialize(NetworkConnection connection)
        {
            connection.Write(Field0);
            connection.Write((byte)Entries.Count);
            foreach (PartyChangedEntry entry in Entries)
                entry.Serialize(connection);
        }
    }
}
