using System;

namespace GaripSozluk.Data.Domain.Interfaces
{
    public interface IBaseEntity 
    {
        int Id { get; set; }

        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
