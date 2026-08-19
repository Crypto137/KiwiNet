namespace KiwiNet.Protocols.Packets.Instance
{
    public sealed class InstanceClientObjectAddPacket : Packet
    {
        public uint ObjectTemplate { get; set; }                // MurmurHash2 reference to a file in GGPK
        public uint Field1 { get; set; }                        // runtime id?
        public List<(uint, uint)> Field2 { get; } = new();
        public byte[] Blob { get; set; } = Array.Empty<byte>();

        public InstanceClientObjectAddPacket() : base((PacketId)100)
        {
        }

        protected override void SerializeData(Stream stream)
        {
            PacketIO.WriteUInt32(stream, ObjectTemplate);
            PacketIO.WriteUInt32(stream, Field1);

            PacketIO.WriteUInt8(stream, (byte)Field2.Count);
            foreach (var kvp in Field2)
            {
                PacketIO.WriteUInt32(stream, kvp.Item1);
                PacketIO.WriteUInt32(stream, kvp.Item2);
            }

            stream.Write(Blob);
        }
    }
}
