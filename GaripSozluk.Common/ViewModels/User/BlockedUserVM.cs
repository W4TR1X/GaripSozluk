using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.User
{
    public class BlockedUserVM
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; }

        [Display(Name = "Bloklanma tarihi")]
        public DateTime Date { get; set; }
    }
}
