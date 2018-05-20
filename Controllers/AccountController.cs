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
       

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IConfiguration configuration,
                                ApplicationDbContext ApplicationDbContext,RoleManager<IdentityRole> RoleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _ApplicationDbContext = ApplicationDbContext;
            _RoleManager = RoleManager;
        
        }

        [Authorize]
        [HttpPost]
        public LoginDto Test([FromBody] LoginDto model)
        {
            return model;
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
                    var a = await _userManager.IsLockedOutAsync(appUser);

                    return await GenerateJwtToken(model.Email, appUser);
                }
                else
                {
                    return new JWTViewModel() { IsValid = false };
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
        public List<ApplicationUser> UserList() {
            List<ApplicationUser> users = _userManager.Users.ToList();

            return users;
        }

        [Authorize]
        [HttpGet]
        public List<MainMenuItem> Menue() {

            List<MainMenuItem> ls  = _ApplicationDbContext.MenuItemMain.Where(a => a.Hidden == false).Include(a => a.Children).ToList();
            
            foreach (var item in ls)
            {
                item.Children = item.Children.Where( a=> a.Hidden == false).OrderBy(x => x.Sort).ToList();
            }

            return ls;

        }

        [Authorize]
        [HttpPost]
        public async Task<bool> EditUser([FromBody] UserUpdateModel model) {
            
            if (ModelState.IsValid)
            {
                ApplicationUser dbUser = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);

                if (dbUser.Id == model.Id)
                {
                    Mapper.Map(model, dbUser);
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
        public async Task<object> Register([FromBody] RegisterDto model)
        {

            try
            {
                var user = new ApplicationUser() { UserName = model.Email };
                Mapper.Map(model, user);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, model.SecurityGroup);
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

            var tt = new JwtSecurityTokenHandler().WriteToken(token);

            //return new JWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = token.Subject, ValidTo = token.ValidTo, IsValid = true } ;//new JwtSecurityTokenHandler().WriteToken(token);
            return new JWTViewModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), UserID = appuser.FirstName + appuser.LastName, ValidTo = token.ValidTo, IsValid = true };//new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet]
        public List<string> SecurityGroups()
        {
            return _RoleManager.Roles.Select(r => r.Name).ToList();
        }
 

  
    }
}