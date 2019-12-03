using System;
using System.Collections.Generic;

namespace learn.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Messages>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
    }
}
