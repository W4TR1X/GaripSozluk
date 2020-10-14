using GaripSozluk.Common.ViewModels.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface IOpenLibraryApiService
    {
        ApiResultVM SearchAuthor(string authorName);

        ApiResultVM SearchTitle(string title);
    }
}
