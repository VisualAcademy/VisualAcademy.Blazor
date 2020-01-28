using VisualAcademy.Admin.Services;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace VisualAcademy.Admin.Pages
{
    public partial class FrmFileUploadTest
    {
        [Inject]
        public IFileUploadService FileUploadServiceReference { get; set; }

        private IFileListEntry[] selectedFiles; 
        protected void HandleSelection(IFileListEntry[] files)
        {
            this.selectedFiles = files;
        }

        //protected async void UploadClick()
        //{
        //    var file = selectedFiles.FirstOrDefault();
        //    if (file != null)
        //    {
        //        await FileUploadServiceReference.UploadAsync(file);
        //    }
        //}
    }
}
