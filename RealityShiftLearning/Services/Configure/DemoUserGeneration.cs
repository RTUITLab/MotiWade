using Microsoft.AspNetCore.Identity;
using Models;
using RTUITLab.AspNetCore.Configure.Configure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services.Configure
{
    public class DemoUserGeneration : IConfigureWork
    {
        public DemoUserGeneration(UserManager<MotiWadeUser> user )
        {

        }
        public Task Configure(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
