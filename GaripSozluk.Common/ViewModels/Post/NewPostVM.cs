using GaripSozluk.Common.ViewModels.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Post
{
    public class NewPostVM : IVM, IValidation
    {

        [HiddenInput]
        public int HeaderId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(2000)]
        [DisplayName("İçerik")]
        public string Content { get; set; }

        public Dictionary<string, string> ValidationErrors { get; set; }

        [HiddenInput]
        public VMStatus Status { get; set; }

        public NewPostVM()
        {
            ValidationErrors = new Dictionary<string, string>();
        }

        public void Dispose()
        {
        }
    }
}
