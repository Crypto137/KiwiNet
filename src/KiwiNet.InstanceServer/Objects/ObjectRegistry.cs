using KiwiNet.Core.Utils;
using KiwiNet.InstanceServer.Resources;

namespace KiwiNet.InstanceServer.Objects
{
    public abstract class ObjectRegistry<T> : IResource where T: ObjectTemplate, new()
    {
        private readonly Dictionary<string, string> _shortNameToNameLookup = new();
        private readonly Dictionary<uint, string> _hashToNameLookup = new();

        public void Load(string fileName)
        {
            fileName = fileName[..^1];  // .csvf -> .csv

            using ResourceHandle<CsvDataTable> csvTableHandle = ResourceManager.Get<CsvDataTable>(fileName);
            CsvDataTable csvTable = csvTableHandle.Resource;

            if (csvTable.NumRows > 1)
            {
                for (int i = 1; i < csvTable.NumRows; i++)
                {
                    string entry = csvTable.Entries[i * csvTable.NumColumns];
                    uint hash = HashUtility.MurmurHash2(entry);
                    _hashToNameLookup.Add(hash, entry);

                    int delimiterIndex = entry.LastIndexOf('/');
                    string shortName = delimiterIndex != -1
                        ? entry.Substring(delimiterIndex + 1, entry.Length - delimiterIndex - 1)
                        : entry;

                    _shortNameToNameLookup.Add(shortName, entry);
                }
            }
        }

        public void Free()
        {
        }

        public ResourceHandle<T> GetTemplate(uint hash)
        {
            if (_hashToNameLookup.TryGetValue(hash, out string fileName) == false)
                return null;

            return ResourceManager.Get<T>(fileName);
        }

        public ResourceHandle<T> GetTemplate(string shortName)
        {
            if (_shortNameToNameLookup.TryGetValue(shortName, out string fileName) == false)
                return null;

            return ResourceManager.Get<T>(fileName);
        }
    }
}
