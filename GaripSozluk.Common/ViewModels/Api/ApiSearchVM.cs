using GaripSozluk.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class ApiSearchVM
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Arama metni")]
        [MaxLength(50)]
        public string QueryText { get; set; }

        [Display(Name = "Arama tipi")]
        public ApiSearchTypeEnum SearchType { get; set; }

    }
}
