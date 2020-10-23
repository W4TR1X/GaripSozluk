using System;
using System.Collections.Generic;
using System.Text;


namespace GaripSozluk.Common.ViewModels.Post
{
    public class PostHeaderVM
    {
        public string CategoryCode { get; set; }
        public string HeaderCode { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public string HeaderTitle { get; set; }

        public DateTime HeaderDate { get; set; }

        public int ClickCount { get; set; }

        public List<PostVM> Posts { get; set; }
        public bool IsApiResult { get; set; }


        public PostHeaderVM()
        {
            Posts = new List<PostVM>();
            IsApiResult = false;
        }
    }
}
