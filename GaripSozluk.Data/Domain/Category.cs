using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public string IdCode { get; set; }

        public ICollection<Header> Headers { get; set; }
    }
}
