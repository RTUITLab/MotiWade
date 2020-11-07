using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GlobalTimer
    {
        public int Id { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        public string Data { get; set; }
    }
}
