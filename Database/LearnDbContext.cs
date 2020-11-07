using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class LearnDbContext : IdentityDbContext
    {
        public LearnDbContext(DbContextOptions<LearnDbContext> options) : base(options)
        {
        }
        public DbSet<SampleModel> SampleModels { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
