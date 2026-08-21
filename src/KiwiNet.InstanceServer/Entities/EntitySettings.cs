namespace KiwiNet.InstanceServer.Entities
{
    public struct EntitySettings
    {
        public uint Template;       // MurmurHash2 of a file path in GGPK
        public uint Id;             // Runtime id (not sure about this yet)
        public uint PositionX;
        public uint PositionY;
    }
}
