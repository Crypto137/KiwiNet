using KiwiNet.Core.Logging;

namespace KiwiNet.InstanceServer.Resources
{
    public class CSVTable : IResource
    {
        private static readonly Logger Logger = LogManager.CreateLogger();

        public void Load(string fileName)
        {
            Logger.Info($"Loading CSV: {fileName}");
            LoadFile(fileName);
        }

        public void Free()
        {
        }

        private void LoadFile(string fileName)
        {

        }
    }
}
