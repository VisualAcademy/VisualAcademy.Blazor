using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace VisualAcademy.Admin.Pages.Administrations.Roles
{
    public partial class RoleCreate
    {
        [Inject]
        public NavigationManager NavigationManagerRef { get; set; }

        [Inject]
        public RoleManager<IdentityRole> _roleManager { get; set; }

        public CreateRoleViewModel CreateRoleViewModelRef { get; set; } = new CreateRoleViewModel();

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public async Task HandleCreation()
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Name = CreateRoleViewModelRef.RoleName,
            };

            IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
            if (identityResult.Succeeded)
            {
                // 리스트로 이동
                NavigationManagerRef.NavigateTo("/Administrations/Roles");
            }

            foreach (var error in identityResult.Errors)
            {
                ErrorMessages.Add(error.Description);
            }
        }
    }

    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role Name please.")]
        public string RoleName { get; set; }
    }
}
