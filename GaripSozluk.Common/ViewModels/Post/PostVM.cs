using GaripSozluk.Common.Enums;
using System;



namespace GaripSozluk.Common.ViewModels.Post
{
    public class PostVM
    {
       

        public int PostId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime PostDate { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public PostLikeState LikeState { get; set; }

        public bool IsApiResult { get; set; }


        public PostVM()
        {
            IsApiResult = false;
        }

    }
}
