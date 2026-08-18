using KiwiNet.Core.Extensions;

namespace KiwiNet.Protocols.Packets.Common
{
    public readonly struct InstanceDetailsEntry(string hostname, string port)
    {
        public readonly string Hostname = hostname;
        public readonly string Port = port;

        public void Serialize(Stream stream)
        {
            stream.WriteNetworkAsciiString(Hostname);
            stream.WriteNetworkAsciiString(Port);
        }
    }
}
