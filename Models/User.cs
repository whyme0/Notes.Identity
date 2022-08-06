using Microsoft.AspNetCore.Identity;

namespace Notes.Identity.Models
{
    public class User : IdentityUser
    {
        public readonly DateTime RegistrationDate = DateTime.Now;
    }
}
