using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Common.ViewModels.Post;
using GaripSozluk.Data.Domain;
using Microsoft.AspNetCore.Mvc;
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
        IList<HeaderRowVM> GetHeadersByCategoryId(ClaimsPrincipal contextUser, string categoryCode);

        IList<HeaderRowVM> GetAllHeaders(ClaimsPrincipal contextUser);

        Header GetHeaderById(ClaimsPrincipal contextUser, string headerCode);

        PostHeaderListVM GetHeaderPosts(ClaimsPrincipal user, string headerCode, string categoryCode, int PageNumber = 1);

        PostHeaderListVM GetPopularHeaders(ClaimsPrincipal user, string categoryCode = "");

        bool AddNewHeader(ClaimsPrincipal contextUser, NewHeaderVM model);

        bool AddNewPost(ClaimsPrincipal contextUser, NewPostVM model);

        string GetRandomHeaderIdCode(ClaimsPrincipal contextUser);

        PostHeaderListVM Search(ClaimsPrincipal contextUser, HeaderSearchVM searchModel);

        int BulkInsert(ClaimsPrincipal contextUser, List<string> headerList);


       
        void AddYesterdaysLogs();
        void AddYesterdaysMostRequestedPathLogs();
    }
}
