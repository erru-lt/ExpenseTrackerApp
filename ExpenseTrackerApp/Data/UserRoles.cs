using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerApp.Data
{
    public class UserRoles : IdentityRole
    {
        public const string User = "User";
        public const string Admin = "Admin";
    }
}
