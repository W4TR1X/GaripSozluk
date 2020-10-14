using GaripSozluk.Common.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Account
{
    public class LoginVM : IVM, IValidation
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }



        public Dictionary<string, string> ValidationErrors { get; set; }
        public VMStatus Status { get; set; }

        public void Dispose()
        {
        }


        public LoginVM()
        {
            ValidationErrors = new Dictionary<string, string>();
        }
    }
}
