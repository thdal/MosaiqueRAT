namespace Serveur.Models
{
    public class RemoteDrive
    {
        public string displayName { get; private set; }

        public string rootDirectory { get; private set; }

        public RemoteDrive(string displayName, string rootDirectory)
        {
            this.displayName = displayName;
            this.rootDirectory = rootDirectory;
        }
    }
}
