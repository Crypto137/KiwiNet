namespace KiwiNet.Core.Threading
{
    public abstract class Job
    {
        public uint Id { get; internal set; }

        public abstract void Process();
    }
}
