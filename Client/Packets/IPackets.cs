using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using ZeroFormatter;

namespace Client.Packets
{
    public enum TypePackets
    {
        GetAuthentication, GetAuthenticationResponse, SetAuthenticationSuccess, // Authentication
        GetMonitors, GetMonitorsResponse, GetDesktop, GetDesktopResponse // Remote Desktop
    }

    [Union(typeof(GetAuthentication), typeof(GetAuthenticationResponse), typeof(SetAuthenticationSuccess), typeof(GetMonitors), typeof(GetMonitorsResponse), typeof(GetDesktop), typeof(GetDesktopResponse))]
    public abstract class IPackets
    {
        [UnionKey]
        public abstract TypePackets Type { get; }
    }
}
