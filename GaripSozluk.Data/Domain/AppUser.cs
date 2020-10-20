using GaripSozluk.Data.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {
        public DateTime? BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }


        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }
        public virtual ICollection<Header> Headers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostRating> Ratings { get; set; }
        public virtual ICollection<BlockedUser> BlockedUsers { get; set; }
        public virtual ICollection<BlockedUser> BlockedBy { get; set; }
    }
}
