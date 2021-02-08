using System.Threading.Tasks;

namespace PdfGenerator.Services.Meta
{
    public interface ITemplateService
    {
        Task<string> RenderAsync<TViewModel>(string templateFileName, TViewModel viewModel);
    }
}