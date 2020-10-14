using GaripSozluk.Data.Domain.Interfaces;
using System;

namespace GaripSozluk.Data.Domain
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
