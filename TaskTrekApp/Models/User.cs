using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTrekApp.Models
{
    public partial class User : IdentityUser
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;
    }
}
