using GaripSozluk.Common.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Post
{
    public class PostHeaderListVM : IPagination
    {
        public  List<PostHeaderVM> Headers{ get; set; }


        public int CurrentPage { get; set; }
        public int PageCount { get; set; }


        public PostHeaderListVM()
        {
            CurrentPage = 1;
            PageCount = 1;
            Headers = new List<PostHeaderVM>();
        }
    }
}
