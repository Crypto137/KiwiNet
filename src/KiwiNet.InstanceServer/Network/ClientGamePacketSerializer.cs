using KiwiNet.Core.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Packets.Common;
using KiwiNet.Protocols.Packets.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class ClientGamePacketSerializer : PacketSerializer
    {
        public ClientGamePacketSerializer(IPacketHandler packetHandler) : base(packetHandler)
        {
        }

        protected override Packet ConstructAndDeserializePacket(PacketId packetId, NetworkConnection connection)
        {
            Packet packet = null;

            switch (packetId)
            {
                case PacketId.ClientInstanceLinkItemPacketId:
                    packet = PacketFactory.Get<ClientInstanceLinkItemPacket>(packetId);
                    break;

                case PacketId.ClientInstanceChatMessagePacketId:
                    packet = PacketFactory.Get<ClientInstanceChatMessagePacket>(packetId);
                    break;

                case PacketId.ClientInstanceQuitRequestPacketId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientInstanceHeartbeatPacketId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientInstanceSkillTargetEntityId:
                    packet = PacketFactory.Get<ClientInstanceSkillTargetEntity>(packetId);
                    break;

                case PacketId.ClientInstanceSkillTargetLocationId:
                    packet = PacketFactory.Get<ClientInstanceSkillTargetLocation>(packetId);
                    break;

                case PacketId.ClientInstancePickupItemFromGroundId:
                    // TODO
                    break;

                case PacketId.ClientInstanceLiftItemId:
                    packet = PacketFactory.Get<ClientInstanceLiftItem>(packetId);
                    break;

                case PacketId.ClientInstanceDropItemId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientInstancePlaceItemId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>(packetId);
                    break;

                case PacketId.ClientInstanceLiftSocketableId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>(packetId);
                    break;

                case PacketId.ClientInstancePlaceSocketableId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>(packetId);
                    break;

                case PacketId.ClientInstanceAllocatePassiveSkillPointPacketId:
                    packet = PacketFactory.Get<IntPacket>(packetId);
                    break;

                case PacketId.ClientInstanceRequestActionPacketId:
                    // TODO
                    break;

                case PacketId.ClientInstanceStackItemsPacketId:
                    packet = PacketFactory.Get<ClientInstanceStackItemsPacket>(packetId);
                    break;

                case PacketId.ClientInstanceRequestDismissPositiveBuffPacketId:
                    packet = PacketFactory.Get<IntPacket>(packetId);
                    break;

                case PacketId.ClientInstanceRequestWaypointUsePacketId:
                    packet = PacketFactory.Get<ClientInstanceRequestWaypointUsePacket>(packetId);
                    break;

                case PacketId.ClientInstanceChangeBoundSkillId:
                    packet = PacketFactory.Get<ClientInstanceChangeBoundSkill>(packetId);
                    break;

                case PacketId.ClientInstanceRespawnRequestId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientInstanceUseItemId:
                    packet = PacketFactory.Get<ClientInstanceUseItem>(packetId);
                    break;

                case PacketId.ClientInstanceUseItemOnItemId:
                    packet = PacketFactory.Get<ClientInstanceUseItemOnItem>(packetId);
                    break;

                case PacketId.ClientInstanceUnstackPacketId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>(packetId);
                    break;

                case PacketId.ClientInstanceTerrainGenerationResultId:
                    packet = PacketFactory.Get<ClientInstanceTerrainGenerationResult>(packetId);
                    break;

                case PacketId.ClientInstanceToggleMovieId:
                    // TODO
                    break;

                case PacketId.ClientInstanceAdvanceFrameId:
                    // TODO
                    break;

                case PacketId.ClientInstanceAddFriendPacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstanceRemoveFriendPacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstanceIgnorePacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstanceUnIgnorePacketId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstanceDoNPCChatId:
                    break;

                case PacketId.ClientInstanceFinishedNPCChatId:
                    packet = PacketFactory.Get<SimplePacket>(packetId);
                    break;

                case PacketId.ClientInstanceTakeNPCItemId:
                    packet = PacketFactory.Get<ClientInstanceTakeNPCItem>(packetId);
                    break;

                case PacketId.ClientInstanceTakeNPCItemToSocketId:
                    packet = PacketFactory.Get<ClientInstanceTakeNPCItem>(packetId);
                    break;

                case PacketId.ClientInstancePartyInviteId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstancePartyKickId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstancePartyAcceptId:
                    packet = PacketFactory.Get<IntPacket>(packetId);
                    break;

                case PacketId.ClientInstancePartyLeaveId:
                    packet = PacketFactory.Get<IntPacket>(packetId);
                    break;

                case PacketId.ClientInstancePartyPromoteId:
                    packet = PacketFactory.Get<StringPacket>(packetId);
                    break;

                case PacketId.ClientInstanceChooseInstanceId:
                    packet = PacketFactory.Get<ClientInstanceChooseInstance>(packetId);
                    break;

                case PacketId.ClientInstanceOpenTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstanceCancelTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstancePlaceItemInTradeId:
                    packet = PacketFactory.Get<ClientInstancePlaceItemInTrade>(packetId);
                    break;

                case PacketId.ClientInstanceRemoveItemFromTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstanceAcceptTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstanceCancelAcceptTradeId:
                    // TODO
                    break;
            }

            packet?.Deserialize(connection);
            return packet;
        }
    }
}
