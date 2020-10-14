using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class Post : BaseEntity
    {
        public int HeaderId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }



        public virtual Header Header { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<PostRating> Ratings { get; set; }

    }
}
