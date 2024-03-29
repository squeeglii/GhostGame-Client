namespace me.cg360.spookums.core.network.packet.generic {

    public class PacketInOutChatMessage : NetworkPacket {

        public string Text { get; protected set; }

        public PacketInOutChatMessage() : this(null) { }
        public PacketInOutChatMessage(string text) {
            Text = text;
        }

        protected override byte GetPacketTypeID()
        {
            return VanillaProtocol.PACKET_CHAT_MESSAGE;
        }

        protected override ushort EncodeBody()
        {
            string selectedText = Text ?? " ";

            Body.Reset();
            int size = Body.PutUTF8String(selectedText);

            return (ushort) size;
        }

        protected override void DecodeBody(ushort inboundSize)
        {
            if (inboundSize < 2)
            {
                Text = " ";
            }
            else
            {
                Text = Body.GetUTF8String();
            }

            Body.Reset();
        }

        public override string ToString() {
            return "Content: {" +
                    "text='" + Text + "'" +
                    "}";
        }
    }
}
