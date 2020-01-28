using BlazorInputFile;
using System.Threading.Tasks;

namespace VisualAcademy.Admin.Services
{
    public interface IFileUploadService
    {
        Task UploadAsync(IFileListEntry file);
    }
}
