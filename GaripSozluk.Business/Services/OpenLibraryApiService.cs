using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Api;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Services
{
    public class OpenLibraryApiService : IOpenLibraryApiService
    {
        public ApiResultVM SearchAuthor(string authorName)
        {
            var content = new OpenLibrarySearchJsonVM();

            var client = new RestClient($"http://openlibrary.org/search.json?author=" + authorName);

            var request = new RestRequest(Method.GET);
            IRestResponse response = client.ExecuteAsync(request).Result;

            if (response.IsSuccessful)
            {
                content = JsonConvert.DeserializeObject<OpenLibrarySearchJsonVM>(response.Content);
            }

            return new ApiResultVM() { QueryString = authorName, SearchType = Common.Enums.ApiSearchTypeEnum.Author, ResultModel = content };
        }

        public ApiResultVM SearchTitle(string title)
        {
            var content = new OpenLibrarySearchJsonVM();

            var client = new RestClient($"http://openlibrary.org/search.json?title=" + title);

            var request = new RestRequest(Method.GET);
            IRestResponse response = client.ExecuteAsync(request).Result;

            if (response.IsSuccessful)
            {
                content = JsonConvert.DeserializeObject<OpenLibrarySearchJsonVM>(response.Content);
            }

            return new ApiResultVM() { QueryString = title, SearchType = Common.Enums.ApiSearchTypeEnum.Title, ResultModel = content };
        }
    }
}
