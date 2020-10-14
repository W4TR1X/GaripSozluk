using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class DocumentVM
    {
        public List<string> Author_name { get; set; }
        public List<string> First_sentence { get; set; }
        public string Key { get; set; }
        public List<string> Language { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public List<String> Publish_year { get; set; }
        public String First_publish_year { get; set; }

        public List<String> Seed { get; set; }

    }
}