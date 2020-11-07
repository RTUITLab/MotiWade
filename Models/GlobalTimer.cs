using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GlobalTimer
    {
        public int Id { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
    }
}
