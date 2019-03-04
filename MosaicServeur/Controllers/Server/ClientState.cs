using Serveur.Views;

namespace Serveur.Controllers.Server
{
    public class ClientState
    {
        public string version { get; set; }
        public string operatingSystem { get; set; }
        public string accountType { get; set; }
        public int    imageIndex { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string countryWithCode { get { return string.Format("{0} [{1}]", country, countryCode); } }
        public string downloadDirectory { get; set; }

        public FrmRemoteDesktop frmRdp { get; set; }
        public FrmRemoteShell frmRms { get; set; }
        public FrmRemoteWebcam frmWbc { get; set; }
        public FrmFileManager frmFm { get; set; }
        public FrmTaskManager frmTm { get; set; }
        public FrmSystemInformation frmSi { get; set; }

        public bool receivedLastDirectory { get; set; }


        public bool processingDirectory
        {
            get
            {
                lock (_processingDirectoryLock)
                {
                    return _processingDirectory;
                }
            }
            set
            {
                lock (_processingDirectoryLock)
                {
                    _processingDirectory = value;
                }
            }
        }
        private bool _processingDirectory;
        private readonly object _processingDirectoryLock;

        public ClientState()
        {
            receivedLastDirectory = true;
            _processingDirectory = false;
            _processingDirectoryLock = new object();
        }
    }
}
