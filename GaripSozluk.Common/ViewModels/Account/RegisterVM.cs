using GaripSozluk.Common.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Account
{
    public class RegisterVM : IVM, IValidation
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Kullanıcı adı")]
        public string  Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Mail adresi")]
        [DataType(DataType.EmailAddress)]
        public string MailAddress { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum tarihi")]
        public DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Parola tekrarı")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }



        public Dictionary<string, string> ValidationErrors { get; set; }
        public VMStatus Status { get; set; }

        public void Dispose()
        {
        }

        public RegisterVM()
        {
            ValidationErrors = new Dictionary<string, string>();
        }

    }
}
