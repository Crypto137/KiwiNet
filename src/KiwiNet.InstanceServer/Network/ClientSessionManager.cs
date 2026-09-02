using KiwiNet.Core.Network;

namespace KiwiNet.InstanceServer.Network
{
    public class ClientSessionManager
    {
        private readonly Dictionary<uint, ClientSession> _sessions = new();

        public ClientSession GetSession(uint sessionId)
        {
            if (_sessions.TryGetValue(sessionId, out ClientSession session) == false)
            {
                session = new();
                _sessions.Add(sessionId, session);
            }

            return session;
        }

        public ClientSession GetSessionForConnection(NetworkConnection connection)
        {
            foreach (ClientSession session in _sessions.Values)
            {
                if (session.Connection == connection)
                    return session;
            }

            return null;
        }

        public void RemoveSession(uint sessionId)
        {
            _sessions.Remove(sessionId, out ClientSession session);
        }
    }
}
