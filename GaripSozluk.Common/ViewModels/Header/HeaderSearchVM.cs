using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Header
{
    public class HeaderSearchVM
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string SearchText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool InvertOrder { get; set; }


        public int? CategoryCode { get; set; }
        public int? HeaderCode { get; set; }
        public int PageNumber { get; set; }
    }
}
