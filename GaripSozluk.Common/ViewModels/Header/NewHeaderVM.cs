using GaripSozluk.Common.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Header
{
    public class NewHeaderVM : NewPostVM
    {
        [DisplayName("Kategori")]
        public string CategoryCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(100)]
        [DisplayName("Başlık")]
        public string Title { get; set; }

    }
}
