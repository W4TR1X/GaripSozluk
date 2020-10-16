using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class DocumentVM
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


        public string GetAuthorText()
        {
            if (Author_name != null && Author_name.Count > 0)
            {
                return $"({string.Join(",", Author_name)})";
            }
            return "";
        }

        public bool Equals([AllowNull] DocumentVM other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Title.Equals(other.Title);
        }

        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }

    }
}