using KiwiNet.Core.Config;

namespace KiwiNet.InstanceServer
{
    public sealed class GameConfig : IConfig
    {
        public string WorldAreaId { get; private set; } = "1_1_1";
        public int WorldAreaSeed { get; private set; } = 666;
        public int StartPositionX { get; private set; } = 300;
        public int StartPositionY { get; private set; } = 540;
        public string CharacterTemplate { get; private set; } = "Metadata/Characters/StrDexInt/StrDexInt";
    }
}
