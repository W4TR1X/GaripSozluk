using GaripSozluk.Data.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class AppRole : IdentityRole<int>, IBaseEntity
    {
        public AppRole()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
