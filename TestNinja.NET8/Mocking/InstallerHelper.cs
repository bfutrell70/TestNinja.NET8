using System.Net;

namespace TestNinja.NET8.Mocking
{
    public class InstallerHelper
    {
        // assigned a default value to prevent a warning
        private string _setupDestinationFile = string.Empty;

        public bool DownloadInstaller(string customerName, string installerName)
        {
            var client = new HttpClient();
            try
            {
                // HttpClient methods are asynchronous, but the original WebClient code methods are not.
                // To prevent this method from being an async method, I added .Result and
                // .RunSynchronously() to the code below to allow the code to be executed synchronously.
                // Normally this wouldn't be done.
                var bytes = client.GetByteArrayAsync(string.Format("http://example.com/{0}/{1}", customerName, installerName)).Result;
                File.WriteAllBytesAsync(_setupDestinationFile, bytes).RunSynchronously();

                return true;
            }
            catch (WebException)
            {
                return false;
            }

            // Mosh's original code - WebClient is obsolete in .NET 8
            //var client = new WebClient();
            //try
            //{
            //    client.DownloadFile(
            //        string.Format("http://example.com/{0}/{1}",
            //            customerName,
            //            installerName),
            //        _setupDestinationFile);

            //    return true;
            //}
            //catch (WebException)
            //{
            //    return false; 
            //}
        }
    }
}