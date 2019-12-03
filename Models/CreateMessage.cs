using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn.Models
{
    public class CreateMessage
    {
        public string Message { get; set; }
        public ICollection<SelectListItem> Users { get; set; }
        public DateTime Date { get; set; }
        public int  UserId { get; set; }
    }
}
