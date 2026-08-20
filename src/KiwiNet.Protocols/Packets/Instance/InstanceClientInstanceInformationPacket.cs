namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientInstanceInformationPacket : Packet
    {
        public uint Field0 { get; set; }                        // probably some kind of runtime instance id?
        public string WorldAreaId { get; set; } = string.Empty; // id column in the WorldAreas table
        public string League { get; set; } = string.Empty;      // league name
        public uint Seed { get; set; }                          // DRLG seed
        public List<uint> Field4 { get; } = new();              // hashes of things to preload?

        public InstanceClientInstanceInformationPacket() : base(PacketId.InstanceClientInstanceInformationPacketId)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, Field0);
            PacketIO.WriteString(stream, WorldAreaId);
            PacketIO.WriteString(stream, League);
            PacketIO.WriteUInt32(stream, Seed);

            PacketIO.WriteInt16(stream, (short)Field4.Count);
            foreach (uint value in Field4)
                PacketIO.WriteUInt32(stream, value);
        }
    }
}
