using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.Enums
{
    public enum ApiSearchTypeEnum
    {
        [Display(Name ="Yazar")]
        Author,
        [Display(Name ="Kitap, derği, makale adı")]
        Title
    }
}
