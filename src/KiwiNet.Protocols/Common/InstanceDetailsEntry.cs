using KiwiNet.Core.Network;
using System.Text;

namespace KiwiNet.Protocols.Common
{
    public readonly struct InstanceDetailsEntry(string host, string port)
    {
        public readonly string Host = host;
        public readonly string Port = port;

        public void Serialize(NetworkConnection connection)
        {
            connection.Write(Host, Encoding.ASCII);
            connection.Write(Port, Encoding.ASCII);
        }
    }
}
