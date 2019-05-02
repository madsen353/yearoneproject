using System.Configuration;


namespace CheckInApp.ServerConfiguration
{
    public static class ConfigurationReader
    {
        public static RemoteServerConfig Read()
        {
            return new RemoteServerConfig
            {
                Username = ConfigurationManager.AppSettings["username"],
                Password = ConfigurationManager.AppSettings["password"],
                RemoteAddress = ConfigurationManager.AppSettings["remoteAddress"],
                FilePath = ConfigurationManager.AppSettings["filePath"] //SFTP folder from where the file is to be download
            };
        }
    }
}
