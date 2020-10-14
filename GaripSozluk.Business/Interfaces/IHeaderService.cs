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
    public interface IHeaderService
    {
        IList<HeaderRowVM> GetHeadersByCategoryId(ClaimsPrincipal contextUser, int categoryId);

        Header GetHeaderById(ClaimsPrincipal contextUser, int id);

        PostHeaderListVM GetHeaderPosts(ClaimsPrincipal user, int headerId, int? categoryId, int PageNumber = 1);

        PostHeaderListVM GetPopularHeaders(ClaimsPrincipal user, int categoryId = 1);

        bool AddNewHeader(ClaimsPrincipal contextUser, NewHeaderVM model);

        bool AddNewPost(ClaimsPrincipal contextUser, NewPostVM model);

        int GetRandomHeaderIndex(ClaimsPrincipal contextUser);

        PostHeaderListVM Search(ClaimsPrincipal contextUser, HeaderSearchVM searchModel);
    }
}
