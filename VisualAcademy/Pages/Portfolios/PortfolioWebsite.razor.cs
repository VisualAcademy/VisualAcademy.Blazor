using Microsoft.AspNetCore.Components;

namespace VisualAcademy.Pages.Portfolios
{
    public partial class PortfolioWebsite
    {
        [Inject]
        public NavigationManager NavigationManagerReference { get; set; }

        protected override void OnInitialized()
        {
            
        }

        protected void GoToDotNetKorea()
        {
            NavigationManagerReference.NavigateTo("/About");
        }
    }
}
