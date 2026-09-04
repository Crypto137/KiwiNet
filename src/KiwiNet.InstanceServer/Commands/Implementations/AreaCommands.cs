using KiwiNet.Core.Math;
using KiwiNet.InstanceServer.Network;
using KiwiNet.InstanceServer.Resources.Tables;

namespace KiwiNet.InstanceServer.Commands.Implementations
{
    [CommandGroup]
    public static class AreaCommands
    {
        [CommandHandler("areachange")]
        public static string AreaChange(object invoker, ReadOnlySpan<string> args)
        {
            if (invoker is not RemotePlayer remotePlayer)
                return "This command must be invoked in-game.";

            if (args.Length == 0)
                return "Please provide a valid world area id";

            string worldAreaId = args[0];
            if (WorldAreaTable.IsValidAreaId(worldAreaId) == false)
                return $"'{worldAreaId}' is not a valid world area id.";

            Vector2Int startPosition = default;
            if (args.Length >= 3)
            {
                if (int.TryParse(args[1], out int x) == false)
                    return $"Failed to parse '{args[1]}' as an x coordinate.";

                if (x < 0)
                    return $"x coordinate must be positive.";

                if (int.TryParse(args[2], out int y) == false)
                    return $"Failed to parse '{args[2]}' as a y coordinate.";

                if (y < 0)
                    return "y coordinate must be positive.";

                startPosition = new(x, y);
            }

            remotePlayer.BeginAreaTransfer(worldAreaId, startPosition);

            return string.Empty;
        }
    }
}
