using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Data
{
    public class SecurityDbContextData
    {
        public static async Task SeedUserAsync(UserManager<UserEntities> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new UserEntities("Hector Bryan", "Almonte Soto")
                {
                    Email = "Hbalmontess272@gmail.com",
                    UserName = "SstewiieHA"
                };

                await userManager.CreateAsync(user, "Bryan12s#");
            }
        }
    }
}
