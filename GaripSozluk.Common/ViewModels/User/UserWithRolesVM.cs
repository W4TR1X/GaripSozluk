using GaripSozluk.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.User
{
   public class UserWithRolesVM
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<int> BlockedUserIds { get; set; }
        public ICollection<string> Roles { get; set; }

        public UserWithRolesVM()
        {
            BlockedUserIds = new List<int>();
            Roles = new List<string>();
        }
    }
}
