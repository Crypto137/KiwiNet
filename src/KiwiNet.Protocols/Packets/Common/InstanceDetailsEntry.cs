namespace KiwiNet.Protocols.Packets.Common
{
    public readonly struct InstanceDetailsEntry(string hostname, string port)
    {
        public readonly string Hostname = hostname;
        public readonly string Port = port;

        public void Serialize(Stream stream)
        {
            PacketIO.WriteStringAscii(stream, Hostname);
            PacketIO.WriteStringAscii(stream, Port);
        }
    }
}
