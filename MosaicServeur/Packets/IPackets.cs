using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using ZeroFormatter;

namespace Serveur.Packets
{
    public enum TypePackets
    {
        GetAuthentication, GetAuthenticationResponse, SetAuthenticationSuccess, // Authentication
        GetMonitors, GetMonitorsResponse, GetDesktop, GetDesktopResponse, // Remote desktop
        GetExecuteShellCmd, GetExecuteShellCmdResponse, // Remote Shell
        GetAvailableWebcams, GetAvailableWebcamsResponse, GetWebcam, GetWebcamResponse, StopWebcam, // Remote Webcam
        GetDrives, GetDrivesResponse, SetStatusFileManager, GetDirectory, GetDirectoryResponse, DoDownloadFile, DoDownloadFileResponse, DoDownloadFileCancel, // File Manager
        GetProcesses, GetProcessesResponse, // Task Manager
        GetSysInfo, GetSysInfoResponse // System Information
    }

    [Union(typeof(GetAuthentication), typeof(GetAuthenticationResponse), typeof(SetAuthenticationSuccess), // Authentification
        typeof(GetMonitors), typeof(GetMonitorsResponse), typeof(GetDesktop), typeof(GetDesktopResponse), // Remote Desktop
        typeof(GetExecuteShellCmd), typeof(GetExecuteShellCmdResponse), // Remote Shell
        typeof(GetAvailableWebcams), typeof(GetAvailableWebcamsResponse), typeof(GetWebcam), typeof(GetWebcamResponse), typeof(StopWebcam), // Remote Webcam
        typeof(GetDrives), typeof(GetDrivesResponse), typeof(SetStatusFileManager), typeof(GetDirectory), typeof(GetDirectoryResponse), typeof(DoDownloadFile), typeof(DoDownloadFileResponse), typeof(DoDownloadFileCancel), // File Manager
        typeof(GetProcesses), typeof(GetProcessesResponse), // Task Manager
        typeof(GetSysInfo), typeof(GetSysInfoResponse))] // System Information

    public abstract class IPackets
    {
        [UnionKey]
        public abstract TypePackets Type { get; }
    }
}
