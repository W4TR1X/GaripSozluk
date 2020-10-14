using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Common.ViewModels.Post;
using GaripSozluk.Data.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface IPostService
    {
        void UpVote(ClaimsPrincipal user, int postId);

        void DownVote(ClaimsPrincipal user, int postId);

        int GetPostRating(int postId);

        int GetLikeCount(int postId);

        int GetDislikeCount(int postId);
    }
}
