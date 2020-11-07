using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class MotiWadeUser : IdentityUser
    {
        public List<UserToExercise> UserToExercises { get; set; }
    }
}
