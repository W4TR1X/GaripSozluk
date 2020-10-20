using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Common.ViewModels.Post;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using GaripSozluk.Common.ViewModels.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaripSozluk.Common.ViewModels.User;

namespace GaripSozluk.Business.Services
{
    public class HeaderService : IHeaderService
    {
        //ToDo: OK! Private sabit tanımlamalarına küçük harfle başlamayalım. 'PageMaxItemCount' olmalıdır.
        const int PageMaxItemCount = 8;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IHeaderRepository _headerRepository;
        private readonly IPostRatingRepository _postRatingRepository;

        private readonly IOpenLibraryApiService _openLibraryApiService;

        private readonly ILogRepository _logRepository;

        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public HeaderService(
            ICategoryRepository categoryRepository,
            IHeaderRepository headerRepository,
            IPostRatingRepository postRatingRepository,
            IOpenLibraryApiService openLibraryApiService,
            ILogRepository logRepository,
            IPostService postService,
            IUserService userService)
        {
            _categoryRepository = categoryRepository;
            _headerRepository = headerRepository;
            _postRatingRepository = postRatingRepository;

            _logRepository = logRepository;
            _openLibraryApiService = openLibraryApiService;

            _postService = postService;
            _userService = userService;
        }


        public Header GetHeaderById(ClaimsPrincipal contextUser, int id)
        {
            var blockedUserIds = _userService.GetUserWithRoles(contextUser).BlockedUserIds;

            return _headerRepository.Get(x => x.Id == id && !blockedUserIds.Contains(x.UserId));
        }

        public IList<HeaderRowVM> GetHeadersByCategoryId(ClaimsPrincipal contextUser, int categoryId)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var headers = _headerRepository
                .Where(x => x.CategoryId == categoryId && !user.BlockedUserIds.Contains(x.UserId));

            if (!user.IsAdmin)
            {
                headers = headers.Where(x => x.IsAdminOnly == false);
            }

            var headerVMs = headers.Select(x => new HeaderRowVM()
            {
                HeaderId = x.Id,
                Title = x.Title,
                PostCount = x.Posts.Count - 1
            }).ToList();

            return headerVMs;
        }
        public int GetRandomHeaderIndex(ClaimsPrincipal contextUser)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var headerIds = _headerRepository.Where(x => !user.BlockedUserIds.Contains(x.UserId));

            if (!user.IsAdmin)
            {
                headerIds = headerIds.Where(x => x.IsAdminOnly == false);
            }

            var headerIdList = headerIds.Select(x => x.Id).ToList();
            return headerIdList[new Random().Next(headerIdList.Count - 1)];
        }

        //ToDo: OK! PageNumber 'pageNumber' olarak değiştirelim. 
        public PostHeaderListVM GetHeaderPosts(ClaimsPrincipal contextUser, int headerId, int? categoryId, int pageNumber = 1)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var model = new PostHeaderListVM();

            var headerEntity = _headerRepository.Where(x => x.Id == headerId && !user.BlockedUserIds.Contains(x.UserId), new List<string> { "User", "Posts" }).FirstOrDefault();

            var header = new PostHeaderVM();

            ApiResultVM apiModel = null;

            if (headerEntity == null)
            {
                model = GetPopularHeaders(contextUser);
                return model;
            }
            else
            {
                if(headerEntity.IsAdminOnly && !user.IsAdmin) // Admin check
                {
                    model = GetPopularHeaders(contextUser);
                    return model;
                }

                var title = headerEntity.Title;
                var isBook = title.EndsWith("(Kitap)");
                var isAuthor = title.EndsWith("(Yazar)");

                if (isBook || isAuthor)
                {
                    title = title.Remove(title.Length - 7);
                    if (isBook)
                    {
                        apiModel = _openLibraryApiService.Search(title, Common.Enums.ApiSearchTypeEnum.Title);
                    }
                    else if (isAuthor)
                    {
                        apiModel = _openLibraryApiService.Search(title, Common.Enums.ApiSearchTypeEnum.Author);
                    }
                }
            }

            headerEntity.ClickCount++;

            header.CategoryId = headerEntity.CategoryId;
            header.HeaderId = headerEntity.Id;
            header.HeaderTitle = headerEntity.Title;
            header.UserId = headerEntity.UserId;
            header.Username = headerEntity.User.UserName;
            header.ClickCount = headerEntity.ClickCount;
            header.HeaderDate = headerEntity.UpdateDate ?? headerEntity.CreateDate;

            //ToDo: OK! Sadece count çekeceksen where sorgusu yazmana gerek yok single olarak count içerisinde filtre yapabilirsin. Örnek kodu aşağıda paylaşıyorum.
            var postCount = headerEntity.Posts.Count(x => !user.BlockedUserIds.Contains(x.UserId));

            //Örnek Kod: 
            //var postCount = headerEntity.Posts.Count(x => !blockedUserIds.Contains(x.UserId));


            var pageCount = (postCount / PageMaxItemCount) + ((postCount % PageMaxItemCount) > 0 ? 1 : 0);

            if (pageNumber > pageCount)
            {
                pageNumber = 1;
            }

            model.CurrentPage = pageNumber;
            model.PageCount = pageCount;

            if (headerEntity.Posts != null)
            {
                var posts = headerEntity.Posts
                    .Where(x => !user.BlockedUserIds.Contains(x.UserId))
                    .Skip((pageNumber - 1) * PageMaxItemCount)
                    .Take(PageMaxItemCount)
                    .ToList();

                posts.ForEach(post =>
                {
                    var postVM = new PostVM()
                    {
                        PostId = post.Id,
                        UserId = post.UserId,
                        Username = post.User?.UserName ?? _userService.GetUserById(post.UserId).UserName,
                        Content = post.Content,
                        PostDate = post.UpdateDate ?? post.CreateDate,
                        LikeCount = _postService.GetLikeCount(post.Id),
                        DislikeCount = _postService.GetDislikeCount(post.Id)

                    };

                    if (user.Id > 0)
                    {
                        var rating = _postRatingRepository.Get(x => x.UserId == user.Id && x.PostId == post.Id);

                        if (rating != null)
                        {
                            postVM.LikeState = rating.IsLiked ? Common.Enums.PostLikeState.Liked : Common.Enums.PostLikeState.Disliked;
                        }
                    }

                    header.Posts.Add(postVM);
                });
            }

            if (apiModel != null)
            {
                header.IsApiResult = true;

                foreach (var doc in apiModel.ResultModel.Docs.Take(10))
                {
                    DateTime dateValue;
                    if (!DateTime.TryParse(doc.First_publish_year + "-01-01", out dateValue))
                    {
                        dateValue = DateTime.Now;
                    }

                    var post = new PostVM()
                    {
                        Content = doc.Title,
                        Username = doc.GetAuthorText(),
                        DislikeCount = 0,
                        LikeCount = 0,
                        IsApiResult = true,
                        PostDate = dateValue
                    };
                    header.Posts.Add(post);
                }
            }

            model.Headers.Add(header);
            _headerRepository.Save();

            return model;
        }

        //ToDo: OK! categortId 'categoryId' olarak değiştirelim.
        public PostHeaderListVM GetPopularHeaders(ClaimsPrincipal contextUser, int categoryId = 1)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var model = new PostHeaderListVM();

            var headerQuery = _headerRepository.Where(x => x.CategoryId == categoryId && !user.BlockedUserIds.Contains(x.UserId), new List<string> { "User", "Posts" });

            if (!user.IsAdmin)
            {
                headerQuery = headerQuery.Where(x => x.IsAdminOnly == false);
            }

            headerQuery.Take(8).ToList()
                .ForEach(x =>
                {
                    var header = new PostHeaderVM()
                    {
                        CategoryId = x.CategoryId,
                        HeaderId = x.Id,
                        HeaderTitle = x.Title,
                        UserId = x.UserId,
                        Username = x.User.UserName,
                        ClickCount = x.ClickCount,
                        HeaderDate = x.UpdateDate ?? x.CreateDate
                    };

                    var post = x.Posts.Where(x => !user.BlockedUserIds.Contains(x.UserId))
                        .OrderByDescending(y => _postService.GetPostRating(y.Id))
                        .FirstOrDefault();

                    if (post != null)
                    {
                        var postVM = new PostVM()
                        {
                            PostId = post.Id,
                            UserId = post.UserId,
                            Username = post.User?.UserName ?? _userService.GetUserById(post.UserId).UserName,
                            Content = post.Content,
                            PostDate = post.UpdateDate ?? post.CreateDate,
                            LikeCount = _postService.GetLikeCount(post.Id),
                            DislikeCount = _postService.GetDislikeCount(post.Id)
                        };

                        if (user.Id > 0)
                        {
                            var rating = _postRatingRepository.Get(x => x.UserId == user.Id && x.PostId == post.Id);

                            if (rating != null)
                            {
                                postVM.LikeState = rating.IsLiked ? Common.Enums.PostLikeState.Liked : Common.Enums.PostLikeState.Disliked;
                            }
                        }

                        header.Posts.Add(postVM);

                    };

                    model.Headers.Add(header);
                });

            return model;
        }

        public bool AddNewHeader(ClaimsPrincipal contextUser, NewHeaderVM model)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            if (_headerRepository.Where(x => x.CategoryId == model.CategoryId && x.Title == model.Title).Any())
            {
                model.ValidationErrors.Add("errorMessage", "Bu katogeride bu başlıkta bir konu bulunmaktadır!");
            }
            else
            {
                var header = new Header()
                {
                    CategoryId = model.CategoryId,
                    ClickCount = 0,
                    CreateDate = DateTime.Now,
                    Title = model.Title,
                    UserId = user.Id
                };

                _headerRepository.Add(header);
                _headerRepository.Save();

                model.HeaderId = header.Id;

                var post = new Post()
                {
                    Content = model.Content,
                    HeaderId = model.HeaderId,
                    UserId = user.Id,
                    CreateDate = header.CreateDate
                };

                _postService.Add(post);
                _postService.Save();

                return true;
            }

            return false;
        }

        public bool AddNewPost(ClaimsPrincipal contextUser, NewPostVM model)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var post = new Post()
            {
                Content = model.Content,
                HeaderId = model.HeaderId,
                UserId = user.Id,
                CreateDate = DateTime.Now
            };

            _postService.Add(post);
            _postService.Save();

            return true;
        }

        public PostHeaderListVM Search(ClaimsPrincipal contextUser, HeaderSearchVM searchModel)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var model = new PostHeaderListVM();

            var query = _headerRepository.Where(x => x.Title.Contains(searchModel.SearchText) && !user.BlockedUserIds.Contains(x.UserId), new List<string> { "Posts", "User" });

            if (searchModel.StartDate.HasValue)
            {
                query = query.Where(x => x.CreateDate >= searchModel.StartDate.Value);
            }

            if (searchModel.EndDate.HasValue)
            {
                query = query.Where(x => x.CreateDate <= searchModel.EndDate.Value);
            }

            if (searchModel.InvertOrder)
            {
                query = query.OrderByDescending(x => x.Title);
            }
            else
            {
                query = query.OrderBy(x => x.Title);
            }

            if (!user.IsAdmin)
            {
                query = query.OrderByDescending(x => x.IsAdminOnly == false);
            }

            query.ToList().ForEach(x =>
            {
                var header = new PostHeaderVM()
                {
                    HeaderId = x.Id,
                    CategoryId = x.CategoryId,
                    ClickCount = x.ClickCount,
                    HeaderTitle = x.Title,
                    UserId = x.UserId,
                    Username = x.User.UserName,
                    HeaderDate = x.UpdateDate ?? x.CreateDate
                };

                model.Headers.Add(header);
            });

            return model;
        }

        public int BulkInsert(ClaimsPrincipal contextUser, List<string> headerList)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            int insertedHeaderCount = 0;

            if (headerList.Count > 0)
            {
                var category = _categoryRepository.GetOrCreate("Kitap");

                foreach (var title in headerList)
                {
                    var header = _headerRepository.Get(x => x.Title == title && x.CategoryId == category.Id);

                    if (header == null)
                    {
                        header = new Header()
                        {
                            Title = title,
                            UserId = user.Id,
                            CategoryId = category.Id,
                            CreateDate = DateTime.Now
                        };

                        _headerRepository.Add(header);
                        insertedHeaderCount++;
                    }
                }
                _headerRepository.Save();
            }

            return insertedHeaderCount;
        }

        public IList<HeaderRowVM> GetAllHeaders(ClaimsPrincipal contextUser)
        {
            var user = _userService.GetUserWithRoles(contextUser);

            var headers = _headerRepository.GetAll().Include("Posts");

            if (!user.IsAdmin)
            {
                headers = headers.Where(x => x.IsAdminOnly == false);
            }

            return headers.ToList().Select(x =>
                new HeaderRowVM()
                {
                    HeaderId = x.Id,
                    PostCount = x.Posts.Count,
                    Title = x.Title
                }
            ).ToList();
        }

        public void AddYesterdaysLogs()
        {
            /* 
                5 - Her gün saat 01:00’da çalıştırılacak.Job her çalıştırıldığında 1 adet başlık kaydı açılacak. 
    
                Başlık bir önceki günkü log kayıtlarını yeni bir post başlığı ve yorum alanında gösterecek halde hazırlanmalıdır. 
                    Örneğin: Bugün 19.10.2020 ise bugün 01:00’da 
                    “18.10.2020 günü log listesi(log)” adında bir başlık ve 
                        içerisinde 18 ekim tarihli log kayıtları listelenmiş olmalıdır. 
                        Liste bir tablo halinde değil, bir başlık altındaki yorumlar şeklinde yer almalıdır.
        
                    Bu başlık sadece “Admin” rolündeki kişi tarafından görüntülenebilecektir.
            */

            var date = DateTime.Now.AddDays(-1).Date;

            var headerTitle = date.ToString("dd MMMM yyyy") + " günü log listesi (log)";

            var header = new Header()
            {
                CategoryId = _categoryRepository.GetOrCreate("Log").Id,
                CreateDate = DateTime.Now,
                Title = headerTitle,
                UserId = 1004, //Bot'un id'si  
                IsAdminOnly = true
            };

            var logList = _logRepository.GetAll()
                .Where(x => x.CreateDate.Date == DateTime.Now.AddDays(-1).Date)
                .ToList();

            _headerRepository.Add(header);
            _headerRepository.Save();

            header.Posts = new List<Post>();

            foreach (var log in logList)
            {
                header.Posts.Add(new Post
                {
                    // Rota yolu   Cevap durum kodu    İz tanımlayıcı 

                    Content = $"{log.IPAddress} adresinden, \"{log.RequestMethod}\" metodu ile \"{log.RequestPath}\" adresine \"{log.UserAgent}\" kullanılarak istek yapılmıştır. " +
                              $"Cevap olarak \"{log.ResponseStatusCode}\" dönmüştür. \n (Trace identifier: {log.TraceIdentifier}), Rota yolu \"{log.RoutePath}\"  ",

                    CreateDate = log.CreateDate,
                    UserId = 1004 //Bot'un id'si                
                });
            }

            _postService.Save();
        }

        public void AddYesterdaysMostRequestedPathLogs()
        {
            /*
                 6-	Günde 1 kez çalışacak şekilde yeni bir job daha yaratılacak. Job api projesi içinden tetiklenecektir.
                 7-	Her gün saat 01:10’da çalıştırılacak. Job her çalıştırıldığında 1 adet başlık kaydı açılacak. 

                     Başlık bir önceki gün en fazla istek yapılan bağlantıları gösterecek halde hazırlanmalıdır. 
                     Örneğin: Bugün 19.10.2020 ise bugün 01:10’da 
                         “18.10.2020 gününde en fazla istek yapılan adresle(log-request)” adında bir başlık ve 
                         içerisinde 18 ekim tarihli en fazla kullanılan request path kayıtları listelenmiş olmalıdır. 
                         Liste bir tablo halinde değil, bir başlık altındaki yorumlar şeklinde yer almalıdır.
                         Yorum şablonu şöyle olmalıdır: 
                             “/Log/List adresine yapılan istek gün içerisinde 235 defa çağrılmıştır.”

                     Bu başlık sadece “Admin” rolündeki kişi tarafından görüntülenebilecektir.
            */

            var date = DateTime.Now.AddDays(-1).Date;

            var headerTitle = date.ToString("dd MMMM yyyy") + " tarihinde en fazla istek yapılan adresler (log-request)";

            var header = new Header()
            {
                CategoryId = _categoryRepository.GetOrCreate("Log").Id,
                CreateDate = DateTime.Now,
                Title = headerTitle,
                UserId = 1004, //Bot'un id'si   
                IsAdminOnly = true
            };

            var logGroupList = _logRepository.GetAll().ToList()
                .Where(x => x.CreateDate.Date == date)
                .GroupBy(x => x.RequestPath)
                .OrderByDescending(x => x.Count())
                .ToList();

            _headerRepository.Add(header);
            _headerRepository.Save();

            header.Posts = new List<Post>();

            foreach (var logGroup in logGroupList)
            {
                header.Posts.Add(new Post
                {
                    Content = string.Format("\"{0}\" adresine yapılan istek gün içerisinde {1} defa çağrılmıştır.", logGroup.Key, logGroup.Count()),
                    CreateDate = DateTime.Now,
                    UserId = 1004 //Bot'un id'si                
                });
            }

            _postService.Save();
        }
    }
}
