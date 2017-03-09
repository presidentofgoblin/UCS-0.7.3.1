using System.IO;
using UCS.Core;
using UCS.Core.Network;
using UCS.Files.Logic;
using UCS.Helpers.Binary;
using UCS.Logic;
using UCS.Logic.Enums;
using UCS.Packets.Messages.Server;

namespace UCS.Packets.Messages.Client
{
    // Packet 14134
    internal class AttackNpcMessage : Message
    {
        public AttackNpcMessage(Device device, Reader reader) : base(device, reader)
        {
        }

        public int LevelId { get; set; }

        internal override void Decode()
        {
            this.LevelId = this.Reader.ReadInt32();
        }

        internal override void Process()
        {
            if (this.Device.PlayerState == State.IN_BATTLE)
            {
                ResourcesManager.DisconnectClient(Device);
            }
            else
            {
                if (LevelId > 0 || LevelId < 1000000)
                {
                    this.Device.PlayerState = State.SEARCH_BATTLE;
                    new NpcDataMessage(Device, this.Device.Player, this).Send();
                }
            }
        }
    }
}
