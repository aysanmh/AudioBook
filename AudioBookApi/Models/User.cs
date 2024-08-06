using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBookApi.Models
{
    [Table("Users")]
    public class User :IdentityUser
    {
        public List<PersonalPage> Pages { get; set; } = new List<PersonalPage>();
    }
}
