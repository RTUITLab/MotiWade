using Database;
using Microsoft.EntityFrameworkCore;
using RTUITLab.AspNetCore.Configure.Configure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services.Configure
{
    public class AutoMigration : IConfigureWork
    {
        private readonly LearnDbContext context;

        public AutoMigration(LearnDbContext context)
        {
            this.context = context;
        }
        public async Task Configure(CancellationToken cancellationToken)
        {
            await context.Database.MigrateAsync();
        }
    }
}
