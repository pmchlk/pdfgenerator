using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;

namespace PdfGenerator.Extensions
{
    public static class PuppeteerExtensions
    {
        public static async Task PreparePuppeteerAsync(this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostingEnvironment)
        {
            var browserPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
            var browserOptions = new BrowserFetcherOptions {Path = browserPath};
            var browserFetcher = new BrowserFetcher(browserOptions);
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
        }
    }
}