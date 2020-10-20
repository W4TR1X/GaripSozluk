using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.Enums;
using GaripSozluk.Common.ViewModels.Api;
using GaripSozluk.Common.ViewModels.Api.Extensions;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public OpenLibraryApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApiResultVM Search(string queryText, ApiSearchTypeEnum searchType)
        {
            var content = new OpenLibrarySearchJsonVM();

            string query = "";

            if(searchType== ApiSearchTypeEnum.Author)
            {
                query = _configuration.GetSection("Paths:OpenLibraryAuthorSearchPath").Value + queryText;
            }
            else
            {
                query = _configuration.GetSection("Paths:OpenLibraryTitleSearchPath").Value + queryText;
            }


            var client = new RestClient(query);

            var request = new RestRequest(Method.GET);
            IRestResponse response = client.ExecuteAsync(request).Result;

            if (response.IsSuccessful)
            {
                content = JsonConvert.DeserializeObject<OpenLibrarySearchJsonVM>(response.Content);
                           
                content.Docs = content.Docs.OrderByDescending(x => x.First_publish_year).Distinct(new DocumentVMComparer()).ToList();
                content.Num_filtered = content.Docs.Count();

                content.Docs.ForEach(x => {
                    x.Author = x.Author_name?.FirstOrDefault() ?? "";
                });
            }

            return new ApiResultVM() { QueryString = queryText, SearchType =searchType, ResultModel = content };
        }
    }
}
