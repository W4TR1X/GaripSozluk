using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Api;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

                content.Docs = content.Docs.OrderByDescending(x => x.First_publish_year).ToList();

                content.Docs.ForEach(x => {
                    x.Author = x.Author_name.FirstOrDefault() ?? "";
                });
            }

            return new ApiResultVM() { QueryString = authorName, SearchType = Common.Enums.ApiSearchTypeEnum.Title, ResultModel = content };
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

                content.Docs = content.Docs.OrderByDescending(x => x.First_publish_year).ToList();

                content.Docs.ForEach(x => {
                    x.Author = x.Author_name?.FirstOrDefault() ?? "";                
                });
            }

            return new ApiResultVM() { QueryString = title, SearchType = Common.Enums.ApiSearchTypeEnum.Title, ResultModel = content };
        }
    }
}
