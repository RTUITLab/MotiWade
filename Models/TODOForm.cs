using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class TODOForm
    {
        [Required]
        public string ItemName { get; set; }

    }
}
