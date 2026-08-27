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

        public void RemoveSession(uint sessionId)
        {
            _sessions.Remove(sessionId, out ClientSession session);
            session?.CurrentClient?.Disconnect();
        }
    }
}
