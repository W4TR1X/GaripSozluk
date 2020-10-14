using GaripSozluk.Data.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class PostRating : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool IsLiked { get; set; }


        public virtual Post Post { get; set; }
        public virtual AppUser User { get; set; }
    }
}
