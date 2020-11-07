using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class LearnDbContext : IdentityDbContext<MotiWadeUser>
    {
        public LearnDbContext(DbContextOptions<LearnDbContext> options) : base(options)
        {
        }
        public DbSet<SampleModel> SampleModels { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<TomatoTimer> GlobalTimers { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserToExercise> UserToExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserToExercise>(model =>
            {
                model.HasKey(ute => new { ute.UserId, ute.ExerciseId });

                model.HasOne(ute => ute.User)
                    .WithMany(u => u.UserToExercises)
                    .HasForeignKey(ute => ute.UserId);


                model.HasOne(ute => ute.Exercise)
                    .WithMany(u => u.UserToExercises)
                    .HasForeignKey(ute => ute.ExerciseId);

                model.HasOne(ute => ute.TomatoTimer);
            });

        }
    }
}
