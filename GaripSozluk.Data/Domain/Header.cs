using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class Header : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int ClickCount { get; set; }
        public int UserId { get; set; }
        public bool IsAdminOnly { get; set; }

        public string IdCode { get; set; }


        public virtual Category Category { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
