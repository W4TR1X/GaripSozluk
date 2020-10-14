using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class BlockedUser : BaseEntity
    {
        public int  UserId { get; set; }
        public int BlockedUserId { get; set; }


        public virtual AppUser User { get; set; }
        public virtual AppUser Blocked { get; set; }
    }
}