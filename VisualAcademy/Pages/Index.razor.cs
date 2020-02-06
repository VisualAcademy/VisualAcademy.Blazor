using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace VisualAcademy.Pages
{
    public partial class Index
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            // Empty
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeAsync<object>("RunCarousel");
        }

        public DateTime SelectedDate { get; set; } = DateTime.Now;

        protected void OnSelectedDateChanged(DateTime selectedDate)
        {
            SelectedDate = selectedDate;
            // TODO:
        }
    }
}
