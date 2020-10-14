using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class OpenLibrarySearchJsonVM 
    {
        public int Start { get; set; }
        public int Num_found { get; set; }
        public List<DocumentVM> Docs { get; set; }
    }
}