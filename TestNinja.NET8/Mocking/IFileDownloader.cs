namespace TestNinja.NET8.Mocking
{
	public interface IFileDownloader
	{
		void DownloadFile(string url, string path);
	}
}