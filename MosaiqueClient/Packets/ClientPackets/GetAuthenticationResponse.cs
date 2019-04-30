using Client.Controllers;
using ZeroFormatter;

namespace Client.Packets.ClientPackets
{
    [ZeroFormattable]
    public class GetAuthenticationResponse : IPackets
    {

        public override TypePackets Type
        {
            get
            {
                return TypePackets.GetAuthenticationResponse;
            }
        }

        [Index(0)]
        public virtual string version { get; set; }

        [Index(1)]
        public virtual string operatingSystem { get; set; }

        [Index(2)]
        public virtual string accountType { get; set; }

        [Index(3)]
        public virtual string country { get; set; }

        [Index(4)]
        public virtual string countryCode { get; set; }

        [Index(5)]
        public virtual string city { get; set; }

        [Index(6)]
        public virtual int imageIndex { get; set; }

        [Index(7)]
        public virtual string id { get; set; }

        [Index(8)]
        public virtual string name { get; set; }

        [Index(9)]
        public virtual string clientID { get; set; }

        public GetAuthenticationResponse()
        {
        }

        public GetAuthenticationResponse(string version, string operatingsystem, string accounttype, string country, string countrycode,
            string city, int imageindex, string id, string name, string clientID)
        {
            this.version = version;
            this.operatingSystem = operatingsystem;
            this.accountType = accounttype;
            this.country = country;
            this.countryCode = countrycode;
            this.city = city;
            this.imageIndex = imageindex;
            this.id = id;
            this.name = name;
            this.clientID = clientID;
        }

        public void Execute(ClientMosaique client)
        {
            client.send(this);
        }
    }
}
