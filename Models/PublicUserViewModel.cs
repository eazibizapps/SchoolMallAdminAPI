using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class PublicUserViewModel
    {
		public int AccessFailedCount { get; set; }
		public string ConcurrencyStamp { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string FirstName { get; set; }
		public string Id { get; set; }
		public string LastName { get; set; }
		public bool LockoutEnabled { get; set; }
		public string LockoutEnd { get; set; }
		public string NormalizedEmail { get; set; }
		public string NormalizedUserName { get; set; }
		public string PasswordHash { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public string SecurityStamp { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }

		public string SchoolCode  { get; set; }

}
}
