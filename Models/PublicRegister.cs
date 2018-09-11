using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class PublicRegister
    {
		
		public string FirstName {get;set;}
		public string LastName {get;set;}
		public string Email {get;set;}
		public string Cell {get;set;}
		public string SchoolName {get;set;}
		public int SchoolCode {get;set;}
		public string Password {get;set;}
		public bool Terms { get; set; }
	}
}
