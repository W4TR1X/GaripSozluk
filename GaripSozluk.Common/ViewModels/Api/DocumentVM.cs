using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class DocumentVM : IEqualityComparer<DocumentVM>
    {
        public string Author { get; set; }
        public List<string> Author_name { get; set; }
        public List<string> First_sentence { get; set; }
        public string Key { get; set; }
        public List<string> Language { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public List<String> Publish_year { get; set; }
        public String First_publish_year { get; set; }

        public List<String> Seed { get; set; }


        public bool IsSelected { get; set; }






        public bool Equals([AllowNull] DocumentVM x, [AllowNull] DocumentVM y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if (x.GetHashCode() == y.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode([DisallowNull] DocumentVM obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}