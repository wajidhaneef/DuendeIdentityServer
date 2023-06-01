using DuendeIdentityServer.Data;
using DuendeIdentityServer.Models;
using DuendeIdentityServer.StaticDetails;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DuendeIdentityServer.IDBInitializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(ApplicationDBContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {


                if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
                }
                //else { return; }

                ApplicationUser adminUser = new()
                {
                    UserName = "admin1@gmail.com",
                    Email = "admin1@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "111111111111",
                    Name = "Ben Admin",
                };

                _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

                var claims1 = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                    new Claim(JwtClaimTypes.Name,adminUser.Name),
                    new Claim(JwtClaimTypes.Role,SD.Admin)
                }).Result;



                ApplicationUser customerUser = new()
                {
                    UserName = "customer1@gmail.com",
                    Email = "customer1@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "111111111111",
                    Name = "Ben Customer",
                };

                _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

                var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                     new Claim(JwtClaimTypes.Name,customerUser.Name),
                    new Claim(JwtClaimTypes.Role,SD.Customer),
                }).Result;
            }
            catch (Exception ex)
            {

            }
        }
        //private readonly ApplicationDBContext _dbContext;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        //public DBInitializer(ApplicationDBContext context, UserManager<ApplicationUser> userManager,
        //    RoleManager<IdentityRole> roleManager)
        //{
        //    _dbContext = context;
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}
        //public void Initialize()
        //{
        //    if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
        //    {
        //        _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
        //        _roleManager.CreateAsync(new IdentityRole(SD.User)).GetAwaiter().GetResult();

        //    }
        //    else return;

        //    // Create an admin user
        //    ApplicationUser adminUser = new()
        //    {
        //        Name = "Admin",
        //        Email = "wajid.haneef@devsinc.com",
        //        Phone = "03067071981",
        //    };

        //    _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
        //    _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

        //    var claim1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
        //    {
        //        new Claim(JwtClaimTypes.Name, adminUser.Name),
        //        new Claim(JwtClaimTypes.Role, SD.Admin)
        //        // Custom claim can be added as
        //        // new Claim("dob", "10May")
        //    }).Result;


        //    // create a simple user
        //    ApplicationUser user1 = new()
        //    {
        //        Name = "User",
        //        Email = "user@devsinc.com",
        //        Phone = "03200000000",
        //    };

        //    _userManager.CreateAsync(user1, "User123*").GetAwaiter().GetResult();
        //    _userManager.AddToRoleAsync(user1, SD.User).GetAwaiter().GetResult();

        //    var claim2 = _userManager.AddClaimsAsync(user1, new Claim[]
        //    {
        //        new Claim(JwtClaimTypes.Name, user1.Name),
        //        new Claim(JwtClaimTypes.Role, SD.User)
        //        // Custom claim can be added as
        //        // new Claim("dob", "10May")
        //    }).Result;
        //}
    }
}
