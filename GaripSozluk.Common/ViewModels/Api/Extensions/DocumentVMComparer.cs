using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api.Extensions
{
    public class DocumentVMComparer : IEqualityComparer<DocumentVM>
    {
        public bool Equals([AllowNull] DocumentVM document1, [AllowNull] DocumentVM document2)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(document1, document2)) return true;

            //Check whether any of the compared objects is null.
            if (document1 is null || document2 is null)
                return false;

            //Check whether the properties are equal.
            return document1.Title == document2.Title;
        }

        public int GetHashCode([DisallowNull] DocumentVM obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}
