using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Extensions;
using PdfGenerator.Services.Meta;
using PdfGenerator.ViewModels;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PdfGenerator.Controllers
{
    [Route("/api/print")]
    public class PrintController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public PrintController(ITemplateService templateService)
        {
            _templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
        }

        [HttpGet]
        public async Task<IActionResult> Print()
        {
            var model = new InvoiceViewModel
            {
                CreatedAt = DateTime.Now,
                Due = DateTime.Now.AddDays(10),
                Id = 12533,
                AddressLine = "Jumpy St. 99",
                City = "Trampoline",
                ZipCode = "22-113",
                CompanyName = "Jumping Rabbit Co.",
                PaymentMethod = "Check",
                Items = new List<InvoiceItemViewModel>
                {
                    new InvoiceItemViewModel("Website design", 621.99m),
                    new InvoiceItemViewModel("Website creation", 1231.99m)
                }
            };
            var html = await _templateService.RenderAsync("Templates/InvoiceTemplate", model);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = PuppeteerExtensions.ExecutablePath
            });
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(html);
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });
            return File(pdfContent, "application/pdf", $"Invoice-{model.Id}.pdf");
        }
    }
}