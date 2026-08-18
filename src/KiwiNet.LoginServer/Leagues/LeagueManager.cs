namespace KiwiNet.LoginServer.Leagues
{
    public class LeagueManager
    {
        private readonly List<League> _leagues = new();

        private ulong _currentLeagueId = 0;

        public bool Initialize()
        {
            // Names and descriptions as of Aug 16, 2011 from here: https://www.youtube.com/watch?v=C7hyUnvksqk
            AddLeague("Default", "The default game mode. If a character is removed from another league it ends up here.", false);
            AddLeague("Hardcore", "A character killed in the hardcore league cannot respawn, but is moved to the default league.", true);
            return true;
        }

        public League AddLeague(string name, string description, bool isHardcore)
        {
            League league = new(++_currentLeagueId, name, description, isHardcore);
            _leagues.Add(league);
            return league;
        }

        public League GetLeague(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            foreach (League league in _leagues)
            {
                if (string.Equals(name, league.Name, StringComparison.OrdinalIgnoreCase))
                    return league;
            }

            return null;
        }

        public List<League>.Enumerator GetEnumerator()
        {
            return _leagues.GetEnumerator();
        }
    }
}
