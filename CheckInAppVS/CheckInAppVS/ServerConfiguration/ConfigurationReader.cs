using System.Configuration;

namespace CheckInAppVS//.ServerConfiguration
{
    //Made by Rasmus

    public static class ConfigurationReader
    {
        public static RemoteServerConfig Read()
        {
            return new RemoteServerConfig
            {
                Username = ConfigurationManager.AppSettings["username"],
                Password = ConfigurationManager.AppSettings["password"],
                RemoteAddress = ConfigurationManager.AppSettings["remoteAddress"],
                FilePath = ConfigurationManager.AppSettings["filePath"]
            };
        }
    }
}