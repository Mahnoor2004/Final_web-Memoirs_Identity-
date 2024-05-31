using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MyUser : IdentityUser
    {

        [Required(ErrorMessage = "First Name is required")]
        public string? First{ get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? Last { get; set; }

    }
}
