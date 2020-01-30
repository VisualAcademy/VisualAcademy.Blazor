using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisualAcademy.Admin.Pages.Administrations.Roles
{
    public partial class RoleList
    {
        [Inject]
        public NavigationManager NavigationManagerRef { get; set; }

        [Inject]
        public RoleManager<IdentityRole> RoleManager { get; set; }

        private List<IdentityRole> models;

        protected override async Task OnInitializedAsync()
        {
            models = RoleManager.Roles.ToList();
            await Task.CompletedTask;
        }
    }
}
