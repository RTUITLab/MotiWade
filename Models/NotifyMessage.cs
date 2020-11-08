using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class NotifyMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public string Topic { get; set; }
    }
}
