using System;
using System.Collections.Generic;

namespace learn.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool? IsDeleted { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
