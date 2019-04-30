﻿using Serveur.Controllers.Server;
using Serveur.Packets;
using ZeroFormatter;

namespace MosaiqueServeur.Packets.ServerPackets
{
    [ZeroFormattable]
    public class PlaySong : IPackets
    {
        public override TypePackets Type
        {
            get
            {
                return TypePackets.PlaySong;
            }
        }

        [Index(0)]
        public virtual int song { get; set; }

        public PlaySong()
        {
        }

        public PlaySong(int song)
        {
            this.song = song;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
