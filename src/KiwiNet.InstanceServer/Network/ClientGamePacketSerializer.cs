using KiwiNet.Core.Network;
using KiwiNet.Protocols;
using KiwiNet.Protocols.Common;
using KiwiNet.Protocols.Instance;

namespace KiwiNet.InstanceServer.Network
{
    public sealed class ClientGamePacketSerializer : PacketSerializer
    {
        public ClientGamePacketSerializer(IPacketHandler packetHandler) : base(packetHandler)
        {
        }

        protected override Packet ConstructAndDeserializePacket(byte packetId, NetworkConnection connection)
        {
            Packet packet = null;

            switch ((PacketId)packetId)
            {
                case PacketId.ClientInstanceLinkItemPacketId:
                    packet = PacketFactory.Get<ClientInstanceLinkItemPacket>();
                    break;

                case PacketId.ClientInstanceChatMessagePacketId:
                    packet = PacketFactory.Get<ClientInstanceChatMessagePacket>();
                    break;

                case PacketId.ClientInstanceQuitRequestPacketId:
                    packet = PacketFactory.Get<SimplePacket>();
                    break;

                case PacketId.ClientInstanceHeartbeatPacketId:
                    packet = PacketFactory.Get<SimplePacket>();
                    break;

                case PacketId.ClientInstanceSkillTargetEntityId:
                    packet = PacketFactory.Get<ClientInstanceSkillTargetEntity>();
                    break;

                case PacketId.ClientInstanceSkillTargetLocationId:
                    packet = PacketFactory.Get<ClientInstanceSkillTargetLocation>();
                    break;

                case PacketId.ClientInstancePickupItemFromGroundId:
                    // TODO
                    break;

                case PacketId.ClientInstanceLiftItemId:
                    packet = PacketFactory.Get<ClientInstanceLiftItem>();
                    break;

                case PacketId.ClientInstanceDropItemId:
                    packet = PacketFactory.Get<SimplePacket>();
                    break;

                case PacketId.ClientInstancePlaceItemId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>();
                    break;

                case PacketId.ClientInstanceLiftSocketableId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>();
                    break;

                case PacketId.ClientInstancePlaceSocketableId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>();
                    break;

                case PacketId.ClientInstanceAllocatePassiveSkillPointPacketId:
                    packet = PacketFactory.Get<IntPacket>();
                    break;

                case PacketId.ClientInstanceRequestActionPacketId:
                    // TODO
                    break;

                case PacketId.ClientInstanceStackItemsPacketId:
                    packet = PacketFactory.Get<ClientInstanceStackItemsPacket>();
                    break;

                case PacketId.ClientInstanceRequestDismissPositiveBuffPacketId:
                    packet = PacketFactory.Get<IntPacket>();
                    break;

                case PacketId.ClientInstanceRequestWaypointUsePacketId:
                    packet = PacketFactory.Get<ClientInstanceRequestWaypointUsePacket>();
                    break;

                case PacketId.ClientInstanceChangeBoundSkillId:
                    packet = PacketFactory.Get<ClientInstanceChangeBoundSkill>();
                    break;

                case PacketId.ClientInstanceRespawnRequestId:
                    packet = PacketFactory.Get<SimplePacket>();
                    break;

                case PacketId.ClientInstanceUseItemId:
                    packet = PacketFactory.Get<ClientInstanceUseItem>();
                    break;

                case PacketId.ClientInstanceUseItemOnItemId:
                    packet = PacketFactory.Get<ClientInstanceUseItemOnItem>();
                    break;

                case PacketId.ClientInstanceUnstackPacketId:
                    packet = PacketFactory.Get<ClientInstanceInventoryMovePacket>();
                    break;

                case PacketId.ClientInstanceTerrainGenerationResultId:
                    packet = PacketFactory.Get<ClientInstanceTerrainGenerationResult>();
                    break;

                case PacketId.ClientInstanceToggleMovieId:
                    // TODO
                    break;

                case PacketId.ClientInstanceAdvanceFrameId:
                    // TODO
                    break;

                case PacketId.ClientInstanceAddFriendPacketId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstanceRemoveFriendPacketId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstanceIgnorePacketId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstanceUnIgnorePacketId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstanceDoNPCChatId:
                    break;

                case PacketId.ClientInstanceFinishedNPCChatId:
                    packet = PacketFactory.Get<SimplePacket>();
                    break;

                case PacketId.ClientInstanceTakeNPCItemId:
                    packet = PacketFactory.Get<ClientInstanceTakeNPCItem>();
                    break;

                case PacketId.ClientInstanceTakeNPCItemToSocketId:
                    packet = PacketFactory.Get<ClientInstanceTakeNPCItem>();
                    break;

                case PacketId.ClientInstancePartyInviteId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstancePartyKickId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstancePartyAcceptId:
                    packet = PacketFactory.Get<IntPacket>();
                    break;

                case PacketId.ClientInstancePartyLeaveId:
                    packet = PacketFactory.Get<IntPacket>();
                    break;

                case PacketId.ClientInstancePartyPromoteId:
                    packet = PacketFactory.Get<StringPacket>();
                    break;

                case PacketId.ClientInstanceChooseInstanceId:
                    packet = PacketFactory.Get<ClientInstanceChooseInstance>();
                    break;

                case PacketId.ClientInstanceOpenTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstanceCancelTradeId:
                    // TODO
                    break;

                case PacketId.ClientInstancePlaceItemInTradeId:
                    packet = PacketFactory.Get<ClientInstancePlaceItemInTrade>();
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

            if (packet != null)
            {
                packet.Id = packetId;
                packet.Deserialize(connection);
            }

            return packet;
        }
    }
}
