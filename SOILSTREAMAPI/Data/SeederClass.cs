
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SOILSTREAMAPI.Models;
using System.Collections;

namespace SOILSTREAMAPI.Data
{
    public class Seeder
    {
        private readonly SoilStreamsDbContext _context;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public Seeder(SoilStreamsDbContext context, UserManager<User> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _context = context;
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task SeedMe(string envName)
        {
            _context.Database.EnsureCreated();
            var roles = new string[] { "Farmer", "Agent", "Consumer" };
            if (!_roleMgr.Roles.Any())
            {
                foreach (var role in roles)
                    await _roleMgr.CreateAsync(new IdentityRole(role));
            }

            var path_userSeed = "/app/Seeds.json";
            var path_productSeed = "/app/Products.json";
           
            if (envName.Equals("Development"))
            {
                path_userSeed = @"../SOILSTRAMAPI/Data/Seeds.json";
                path_productSeed = @"../SOILSTRAMAPI/Data/Product.json";
                
            }

            var productDData = File.ReadAllText(path_productSeed);
            var products = JsonConvert.DeserializeObject<List<Product>>(productDData);

            var userData = File.ReadAllText(path_userSeed);
            var users = JsonConvert.DeserializeObject<List<User>>(userData);


            if (!_context.Products.Any())
                _context.Products.AddRange(products);
           
            if (!_userMgr.Users.Any())
            {
                int count = 0;
                var stackCount = 0;
                var squadCount = 0;
                var role = roles[2];
                foreach (var user in users)
                {
                    user.UserName = user.Email;
                    await _userMgr.CreateAsync(user, "P@ssw0rd");
                    if (count > 2)
                        role = roles[1];
                    else if (count > 5)
                        role = roles[0];
                     
                    await _userMgr.AddToRoleAsync(user, role);
                    count++;
                }
            }
            _context.SaveChanges();
        }
    }
}