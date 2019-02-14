using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using ZeroFormatter;

namespace Serveur.Packets
{
    public enum TypePackets
    {
        GetAuthentication, GetAuthenticationResponse, SetAuthenticationSuccess, // Authentication
        GetMonitors, GetMonitorsResponse, GetDesktop, GetDesktopResponse // Remote desktop
    }

    [Union(typeof(GetAuthentication), typeof(GetAuthenticationResponse), typeof(SetAuthenticationSuccess)
        ,typeof(GetMonitors), typeof(GetMonitorsResponse), typeof(GetDesktop), typeof(GetDesktopResponse))]
    public abstract class IPackets
    {
        [UnionKey]
        public abstract TypePackets Type { get; }
    }
}
