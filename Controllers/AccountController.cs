using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using WebApiJwt.ViewModels;
using WebApiJwt.Entities;
using WebApiJwt.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using WebApiJwt.Services;

namespace WebApiJwt.Controllers
{
	
	[Route("[controller]/[action]")]
    public class AccountController : Controller
    {
	

		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _ApplicationDbContext;
        private readonly RoleManager<IdentityRole> _RoleManager;
		private static IHostingEnvironment _env;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IConfiguration configuration,
                                ApplicationDbContext ApplicationDbContext,RoleManager<IdentityRole> RoleManager, IHostingEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _ApplicationDbContext = ApplicationDbContext;
            _RoleManager = RoleManager;
			_env = env;

		}

        [Authorize]
        [HttpPost]
        public LoginDto Test([FromBody] LoginDto model)
        {
            return model;
        }


		[HttpPost]
		public async Task<bool> ResetPassword([FromBody] ForgotPasswordViewModel model) {

			var user = await _userManager.FindByEmailAsync(model.Email);


			if (user != null)
			{
				var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

                //var rolecode = _ApplicationDbContext.Roles.Where(a => a.Name == model.Role).Select(b => b.Id).FirstOrDefault();


                //token = token.Replace('+', "@@@",);

                user.ResetPasswordToken = token;
				var result = await _userManager.UpdateAsync(user);


				string t = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

				string baseurl = "";

				if (_env.IsProduction())
				{
					baseurl = $"http://www.schoolmall.co.za/#/users/reset-password?id=";
				}
				else
				{
					baseurl = $"http://localhost:4200/#/users/reset-password?id=";
				}

				string url = System.Web.HttpUtility.UrlPathEncode(baseurl + t);

				string body = _ApplicationDbContext.EmailTemplates.Where(w => w.TemplateType == "Reset").Select(m => m.Template).FirstOrDefault();

				body = body.Replace("{{name}}", user.FirstName);
				body = body.Replace("{{action_url}}", url);

				MailService mailService = new MailService("mail.eazibiz.co.za", "eazibkpl", "3Wnk7usY", "info@schoolmall.co.za", model.Email, body, "School Mall Reset Passord");
				Thread mgrThread = new Thread(new ThreadStart(mailService.SendMail));
				mgrThread.Start();


				//SmtpClient client = new SmtpClient("mail.eazibiz.co.za");
				//client.UseDefaultCredentials = false;
				//client.Credentials = new NetworkCredential("eazibkpl", "3Wnk7usY");
				//
				//MailMessage mailMessage = new MailMessage();
				//mailMessage.From = new MailAddress("setet@schoolmall.co.za");
				//mailMessage.To.Add("mathhys.smith@gmail.com");
				//mailMessage.Body = url;
				//mailMessage.Subject = "School Mall Reset Passord";
				//client.Send(mailMessage);

				return result.Succeeded;


			}
			else
			{
				return false;
			}



			
		}

		[HttpPost]
		public async Task<bool> Reset([FromBody] ResetPassword model) {

			var token = Encoding.UTF8.GetString(Convert.FromBase64String(model.Id));
			ApplicationUser user = _userManager.Users.Where(m => m.ResetPasswordToken == token).FirstOrDefault();

			if (user != null)
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
				user.ResetPasswordToken = null;
				var result = await _userManager.UpdateAsync(user);
				return result.Succeeded;
			}
			else
			{
				return false;
			}

		}



		[HttpPost]
		public async Task<JWTViewModel> Login([FromBody] LoginDto model)
        {
            try
            {
				
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);

                    var secGroup = _ApplicationDbContext.UserRoles.Where(m => m.UserId == appUser.Id).FirstOrDefault();

                    if (secGroup.RoleId == "P") {
                        return new JWTViewModel() { IsValid = false };
                    }
                    
                    
                    return await GenerateJwtToken(model.Email, appUser);
                }
                else
                {
                    return new JWTViewModel() { IsValid = false };
                }
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }



		[HttpPost]
		public async Task<PublicJWTViewModel> LoginPublic([FromBody] PublicLoginDto model)
		{
			try
			{

				var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

				if (result.Succeeded)
				{
					var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Username && !String.IsNullOrEmpty(r.SchoolCode));

                    var appSchool = _ApplicationDbContext.Schools.Where(m => m.SchoolId2.ToString() == appUser.SchoolCode).FirstOrDefault();

                    if (appSchool.SMActive != null)
                    {
                        if (appSchool.SMActive == false)
                        {
                            return new PublicJWTViewModel() { IsValid = false };
                        }
                    }
                    else {
                        return new PublicJWTViewModel() { IsValid = false };
                    }
                    


                    if (appUser != null)
					{
						return await PublicGenerateJwtToken(model.Username, appUser);
					}
					else
					{
						return new PublicJWTViewModel() { IsValid = false };
					}
					
				}
				else
				{
					return new PublicJWTViewModel() { IsValid = false };
				}
			}
			catch (Exception ex)
			{

			}

			throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
		}





		[HttpPost]
        public async Task<bool> Logout([FromBody] LoginDto model)
        {
            try
            {

                await _signInManager.SignOutAsync();
                return true;    
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> ResetUserPassword([FromBody] ResetUserPasswordViewModel model) {

            if (ModelState.IsValid)
            {
                ApplicationUser dbUser = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);

                if (dbUser.Id == model.Id)
                {
                    dbUser.PasswordHash = _userManager.PasswordHasher.HashPassword(dbUser,model.Password);
                    var result = await _userManager.UpdateAsync(dbUser);
                    return result.Succeeded;
                }
                else {
                    return false;
                }
            }
            else
            {
                return false;
            }
              
        }

        [Authorize]
        [HttpGet]
        public List<UserViewModel> UserList() {
            

            var userList = from a in _userManager.Users
                           join b in _ApplicationDbContext.UserRoles on a.Id equals b.UserId
                           join c in _ApplicationDbContext.Roles on b.RoleId equals c.Id
                           select new UserViewModel() {
                                    AccessFailedCount = a.AccessFailedCount,
                                    ConcurrencyStamp = a.ConcurrencyStamp,
                                    Email  = a.Email,
                                    EmailConfirmed  = a.EmailConfirmed,
                                    FirstName = a.FirstName,
                                    Id  = a.Id,
                                    LastName = a.LastName,
                                    LockoutEnabled = a.LockoutEnabled,
                                    NormalizedEmail = a.NormalizedEmail,
                                    NormalizedUserName  = a.NormalizedUserName,
                                    PasswordHash = a.PasswordHash,
                                    PhoneNumber = a.PhoneNumber,
                                    PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                                    SecurityStamp = a.SecurityStamp,
                                    TwoFactorEnabled = a.TwoFactorEnabled,
                                    UserName = a.UserName,
                                    Role = c.Name,
									SchoolId2 = a.SchoolCode
									
                                    };


            return userList.ToList();
        }

        [Authorize]
        [HttpGet]
        public List<MainMenuItem> Menue(string id) {

            List<MainMenuItem> ls = new List<MainMenuItem>();

            try
            {
                ls = _ApplicationDbContext.MenuItemMain.Where(a => a.Hidden == false && a.SecGroup == id).Include(a => a.Children).ThenInclude(x => x.Children).ToList();

                foreach (var item in ls)
                {
                    item.Children = item.Children.Where(a => a.Hidden == false && a.SecGroup == id).OrderBy(x => x.Sort).ToList();

                    foreach (var items in item.Children)
                    {
                        items.Children = items.Children.Where(a => a.Hidden == false && a.SecGroup == id).OrderBy(x => x.Sort).ToList();
                    }

                }
            }
            catch (Exception ex) {
                var a = ex.InnerException;
            }



            return ls.OrderBy(m => m.Sort).ToList() ;

        }

        [Authorize]
        [HttpPost]
        public async Task<bool> EditUser([FromBody] UserUpdateModel model) {
            
            if (ModelState.IsValid)
            {
                ApplicationUser dbUser = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);


                var rolecode = _ApplicationDbContext.Roles.Where(a => a.Name == model.Role).Select(b => b.Id).FirstOrDefault();

                var userSec = (from a in _userManager.Users
                               join b in _ApplicationDbContext.UserRoles on a.Id equals b.UserId
                               join c in _ApplicationDbContext.Roles on b.RoleId equals c.Id
                               where a.Id == model.Id
                               select new UserViewModel()
                               {
                                   AccessFailedCount = a.AccessFailedCount,
                                   ConcurrencyStamp = a.ConcurrencyStamp,
                                   Email = a.Email,
                                   EmailConfirmed = a.EmailConfirmed,
                                   FirstName = a.FirstName,
                                   Id = a.Id,
                                   LastName = a.LastName,
                                   LockoutEnabled = a.LockoutEnabled,
                                   NormalizedEmail = a.NormalizedEmail,
                                   NormalizedUserName = a.NormalizedUserName,
                                   PasswordHash = a.PasswordHash,
                                   PhoneNumber = a.PhoneNumber,
                                   PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                                   SecurityStamp = a.SecurityStamp,
                                   TwoFactorEnabled = a.TwoFactorEnabled,
                                   UserName = a.UserName,
                                   Role = c.Name
                               }).FirstOrDefault();




                if (userSec != null) {

                    ApplicationUser user = _ApplicationDbContext.Users.Where(u => u.Id == model.Id).FirstOrDefault();

                    if (userSec.Role != model.Role)

                        await _userManager.RemoveFromRoleAsync(user, userSec.Role);
                        var add = await _userManager.AddToRoleAsync(user, model.Role);

                    



                }



                if (dbUser.Id == model.Id)
                {
                    Mapper.Map(model, dbUser);

					if (dbUser.LockoutEnabled == true)
					{
						dbUser.LockoutEnd = new DateTime(2099, 1, 1);
					}
					else {
						dbUser.LockoutEnd = null;
					}

					dbUser.SchoolCode = model.SchoolId2;
					var result  = await _userManager.UpdateAsync(dbUser);
                    return result.Succeeded;
                }
                else {
                    return false;
                }
            }
            else
                return false;

        }



		[HttpPost]
		public async Task<PublicRegisterReturn> RegisterPublic([FromBody] PublicRegister model)
		{
			PublicRegisterReturn returnData = new PublicRegisterReturn();

			try
			{

				var SchoolCode = _ApplicationDbContext.Schools.Where(m => m.SchoolId2 == model.SchoolCode).FirstOrDefault();
				if (SchoolCode == null)
				{
					returnData.Valid = false;
					returnData.Error = "School not found! Please enter a valid school code.";
					returnData.ValidSchoolCode = false;
					return returnData;
				}

				var useist = _ApplicationDbContext.Users.Where(m => m.Email == model.Email).FirstOrDefault();
				if (useist != null) {
					returnData.Valid = false;
					returnData.Error = "Email already registered!";
					returnData.ValidSchoolCode = true;
					returnData.ValidEmail = false;
					return returnData;
				}
				



				var user = new ApplicationUser() { UserName = model.Email, LockoutEnabled = false,Cell= model.Cell, Email=model.Email,FirstName=model.FirstName,Terms= model.Terms,SchoolCode =model.SchoolCode.ToString(),SchoolName=model.SchoolName };
				Mapper.Map(model, user);
				user.LockoutEnabled = false;
				
				
				var result = await _userManager.CreateAsync(user, model.Password);
				
				if (result.Succeeded)
				{
				
					//await _signInManager.SignInAsync(user, false);
					await _userManager.AddToRoleAsync(user, "Public");
					//await _userManager.UpdateAsync(user);
				
				
					ApplicationUser dbUser = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
					dbUser.LockoutEnabled = false;
					if (dbUser != null)
					{
						await _userManager.UpdateAsync(dbUser);
					}


					returnData.Valid = true;
					returnData.Error = "";
					returnData.ValidSchoolCode = true;
					returnData.ValidEmail = true;
					return returnData;

				}
				else
				{
					returnData.Valid = false;
					returnData.Error = "UNKNOWN_ERROR";
					returnData.ValidSchoolCode = true;
					returnData.ValidEmail = true;
					return returnData;

				}

			}
			catch (Exception ex)
			{
				var error = ex.InnerException;
				return new PublicRegisterReturn { Error="Error"};
			}
			throw new ApplicationException("UNKNOWN_ERROR");
		}


		[HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {

            try
            {
			

				var user = new ApplicationUser() { UserName = model.Email, LockoutEnabled = false };
				Mapper.Map(model, user);
				user.LockoutEnabled = false;
				

				var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    //await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, model.SecurityGroup);
					//await _userManager.UpdateAsync(user);


					ApplicationUser dbUser = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
					dbUser.LockoutEnabled = false;
					if (dbUser != null)
					{
						await _userManager.UpdateAsync(dbUser);
					}
					

					return true;
                }
                else {
                    return false;
                }

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async Task<JWTViewModel> GenerateJwtToken(string email, IdentityUser user)
        {
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            ApplicationUser appuser = _ApplicationDbContext.Users.Where(u => u.Email == email).FirstOrDefault();

            var userSec =  (from a in _userManager.Users
                           join b in _ApplicationDbContext.UserRoles on a.Id equals b.UserId
                           join c in _ApplicationDbContext.Roles on b.RoleId equals c.Id
                           where a.Email == email     
                           select new UserViewModel()
                           {
                               AccessFailedCount = a.AccessFailedCount,
                               ConcurrencyStamp = a.ConcurrencyStamp,
                               Email = a.Email,
                               EmailConfirmed = a.EmailConfirmed,
                               FirstName = a.FirstName,
                               Id = a.Id,
                               LastName = a.LastName,
                               LockoutEnabled = a.LockoutEnabled,
                               NormalizedEmail = a.NormalizedEmail,
                               NormalizedUserName = a.NormalizedUserName,
                               PasswordHash = a.PasswordHash,
                               PhoneNumber = a.PhoneNumber,
                               PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                               SecurityStamp = a.SecurityStamp,
                               TwoFactorEnabled = a.TwoFactorEnabled,
                               UserName = a.UserName,
                               Role = c.Name
                           }).FirstOrDefault();


            var tt = new JwtSecurityTokenHandler().WriteToken(token);

            //return new JWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = token.Subject, ValidTo = token.ValidTo, IsValid = true } ;//new JwtSecurityTokenHandler().WriteToken(token);
            return new JWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = appuser.FirstName + appuser.LastName, ValidTo = token.ValidTo, IsValid = true,Role= userSec.Role };//new JwtSecurityTokenHandler().WriteToken(token);
        }

		private async Task<PublicJWTViewModel> PublicGenerateJwtToken(string email, IdentityUser user)
		{

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

			var token = new JwtSecurityToken(
				_configuration["JwtIssuer"],
				_configuration["JwtIssuer"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			ApplicationUser appuser = _ApplicationDbContext.Users.Where(u => u.Email == email).FirstOrDefault();

			var userSec = (from a in _userManager.Users
						   join b in _ApplicationDbContext.UserRoles on a.Id equals b.UserId
						   join c in _ApplicationDbContext.Roles on b.RoleId equals c.Id
						   where a.Email == email
						   select new PublicUserViewModel()
						   {
							   AccessFailedCount = a.AccessFailedCount,
							   ConcurrencyStamp = a.ConcurrencyStamp,
							   Email = a.Email,
							   EmailConfirmed = a.EmailConfirmed,
							   FirstName = a.FirstName,
							   Id = a.Id,
							   LastName = a.LastName,
							   LockoutEnabled = a.LockoutEnabled,
							   NormalizedEmail = a.NormalizedEmail,
							   NormalizedUserName = a.NormalizedUserName,
							   PasswordHash = a.PasswordHash,
							   PhoneNumber = a.PhoneNumber,
							   PhoneNumberConfirmed = a.PhoneNumberConfirmed,
							   SecurityStamp = a.SecurityStamp,
							   TwoFactorEnabled = a.TwoFactorEnabled,
							   UserName = a.UserName,
							   Role = c.Name,
							   SchoolCode = a.SchoolCode
						   }).FirstOrDefault();


			var tt = new JwtSecurityTokenHandler().WriteToken(token);

			Schools school = _ApplicationDbContext.Schools.Where(m => m.SchoolId2.ToString() == userSec.SchoolCode).FirstOrDefault();

			//return new JWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = token.Subject, ValidTo = token.ValidTo, IsValid = true } ;//new JwtSecurityTokenHandler().WriteToken(token);
			return new PublicJWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = appuser.Email, ValidTo = token.ValidTo, IsValid = true, Role = userSec.Role, SchoolCode= appuser.SchoolCode, School = school.Name };//new JwtSecurityTokenHandler().WriteToken(token);
		}

		[Authorize]
        [HttpGet]
        public List<string> SecurityGroups()
        {
            return _RoleManager.Roles.Select(r => r.Name).ToList();
        }

		//[Authorize]
		[HttpGet]
		public bool AddSecurity(string Group)
		{
            _ApplicationDbContext.Roles.Add(new IdentityRole { Id = Group.Substring(0,1), Name = Group, NormalizedName = Group.ToUpper() });
			_ApplicationDbContext.SaveChanges();
			return true;
		}




	}
}