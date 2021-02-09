using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;

namespace PdfGenerator.Extensions
{
    public static class PuppeteerExtensions
    {
        private static string _executablePath;
        public static async Task PreparePuppeteerAsync(this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostingEnvironment)
        {
            var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
            var browserOptions = new BrowserFetcherOptions {Path = downloadPath};
            var browserFetcher = new BrowserFetcher(browserOptions);
            _executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultRevision);
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
        }

        public static string ExecutablePath => _executablePath;
    }
}