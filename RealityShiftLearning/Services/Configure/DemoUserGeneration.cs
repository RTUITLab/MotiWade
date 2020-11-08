using Microsoft.AspNetCore.Identity;
using Models;
using RTUITLab.AspNetCore.Configure.Configure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services.Configure
{
    public class DemoUserGeneration : IConfigureWork
    {
        private readonly UserManager<MotiWadeUser> userManager;

        public DemoUserGeneration(UserManager<MotiWadeUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Configure(CancellationToken cancellationToken)
        {
            var demoUser = await userManager.FindByEmailAsync("demo@motiwade.rtuitlab.dev");
            if (demoUser == null)
            {
                var user = new MotiWadeUser { UserName = "demo@motiwade.rtuitlab.dev", Email = "demo@motiwade.rtuitlab.dev" };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddClaimsAsync(user, new Claim[] { 
                        new Claim("urn:github:login", "DEMO_USER"),
                        new Claim("urn:github:avatar", "img/tinyLogo.png")
                    });
                }
            }
        }
    }
}
