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

namespace GaripSozluk.Business.Services
{
    public class HeaderService : IHeaderService
    {
        //ToDo: OK! Private sabit tanımlamalarına küçük harfle başlamayalım. 'PageMaxItemCount' olmalıdır.
        const int PageMaxItemCount = 8;

        private readonly IHeaderRepository _headerRepository;
        private readonly IPostRepository _postRepository;
        private readonly IPostRatingRepository _postRatingRepository;

        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public HeaderService(IHeaderRepository headerRepository,
            IPostRepository postRepository,
            IPostRatingRepository postRatingRepository, 
            IPostService postService,
            IUserService userService)
        {
            _headerRepository = headerRepository;
            _postRepository = postRepository;
            _postRatingRepository = postRatingRepository;

            _postService = postService;
            _userService = userService;
        }

        
        public Header GetHeaderById(ClaimsPrincipal contextUser, int id)
        {
            var blockedUserIds = _userService.GetBlockedUserIds(contextUser);

            return _headerRepository.Get(x => x.Id == id && !blockedUserIds.Contains(x.UserId));
        }

        public IList<HeaderRowVM> GetHeadersByCategoryId(ClaimsPrincipal contextUser, int categoryId)
        {
            var blockedUserIds = _userService.GetBlockedUserIds(contextUser);

            var headers = _headerRepository
                .Where(x => x.CategoryId == categoryId && !blockedUserIds.Contains(x.UserId))
                .Select(x => new HeaderRowVM()
                {
                    HeaderId = x.Id,
                    Title = x.Title,
                    PostCount = x.Posts.Count - 1
                }).ToList();

            return headers;
        }
        public int GetRandomHeaderIndex(ClaimsPrincipal contextUser)
        {
            var blockedUserIds = _userService.GetBlockedUserIds(contextUser);

            var headerIds = _headerRepository.Where(x => !blockedUserIds.Contains(x.UserId)).Select(x => x.Id).ToList();
            return headerIds[new Random().Next(headerIds.Count - 1)];
        }

        //ToDo: OK! PageNumber 'pageNumber' olarak değiştirelim. 
        public PostHeaderListVM GetHeaderPosts(ClaimsPrincipal user, int headerId, int? categoryId, int pageNumber = 1)
        {
            var userId = 0;

            if (user.Claims.Any())
            {
                userId = _userService.GetUser(user).Id;
            }

            var blockedUserIds = _userService.GetBlockedUserIds(user);

            var model = new PostHeaderListVM();

            var headerEntity = _headerRepository.Where(x => x.Id == headerId && !blockedUserIds.Contains(x.UserId), new List<string> { "User", "Posts" }).FirstOrDefault();

            if (headerEntity == null)
            {
                model = GetPopularHeaders(user);
                return model;
            }

            headerEntity.ClickCount++;

            var header = new PostHeaderVM()
            {
                CategoryId = headerEntity.CategoryId,
                HeaderId = headerEntity.Id,
                HeaderTitle = headerEntity.Title,
                UserId = headerEntity.UserId,
                Username = headerEntity.User.UserName,
                ClickCount = headerEntity.ClickCount
            };

            //ToDo: OK! Sadece count çekeceksen where sorgusu yazmana gerek yok single olarak count içerisinde filtre yapabilirsin. Örnek kodu aşağıda paylaşıyorum.
            var postCount = headerEntity.Posts.Count(x => !blockedUserIds.Contains(x.UserId));

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
                    .Where(x => !blockedUserIds.Contains(x.UserId))
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

                    if (userId > 0)
                    {
                        var rating = _postRatingRepository.Get(x => x.UserId == userId && x.PostId == post.Id);

                        if (rating != null)
                        {
                            postVM.LikeState = rating.IsLiked ? Common.Enums.PostLikeState.Liked : Common.Enums.PostLikeState.Disliked;
                        }
                    }

                    header.Posts.Add(postVM);

                });
            }

            model.Headers.Add(header);

            _headerRepository.Save();

            return model;
        }

        //ToDo: OK! categortId 'categoryId' olarak değiştirelim.
        public PostHeaderListVM GetPopularHeaders(ClaimsPrincipal user, int categoryId = 1)
        {
            var userId = 0;

            if (user.Claims.Any())
            {
                userId = _userService.GetUser(user).Id;
            }

            var blockedUserIds = _userService.GetBlockedUserIds(user);

            var model = new PostHeaderListVM();

            _headerRepository
                .Where(x => x.CategoryId == categoryId && !blockedUserIds.Contains(x.UserId), new List<string> { "User", "Posts" })
                .Take(8)
                .ToList()
                .ForEach(x =>
                {
                    var header = new PostHeaderVM()
                    {
                        CategoryId = x.CategoryId,
                        HeaderId = x.Id,
                        HeaderTitle = x.Title,
                        UserId = x.UserId,
                        Username = x.User.UserName,
                        ClickCount = x.ClickCount
                    };

                    var post = x.Posts.Where(x => !blockedUserIds.Contains(x.UserId))
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

                        if (userId > 0)
                        {
                            var rating = _postRatingRepository.Get(x => x.UserId == userId && x.PostId == post.Id);

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
            var user = _userService.GetUser(contextUser);

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

                _postRepository.Add(post);
                _postRepository.Save();

                return true;
            }

            return false;
        }

        public bool AddNewPost(ClaimsPrincipal contextUser, NewPostVM model)
        {
            var user = _userService.GetUser(contextUser);

            var post = new Post()
            {
                Content = model.Content,
                HeaderId = model.HeaderId,
                UserId = user.Id,
                CreateDate = DateTime.Now
            };

            _postRepository.Add(post);
            _postRepository.Save();

            return true;
        }

        public PostHeaderListVM Search(ClaimsPrincipal contextUser, HeaderSearchVM searchModel)
        {
            var userId = 0;

            var blockedUserIds = _userService.GetBlockedUserIds(contextUser);

            if (contextUser.Claims.Any())
            {
                userId = _userService.GetUser(contextUser).Id;
            }

            var model = new PostHeaderListVM();

            var query = _headerRepository.Where(x => x.Title.Contains(searchModel.SearchText) && !blockedUserIds.Contains(x.UserId), new List<string> { "Posts", "User" });

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

            query.ToList().ForEach(x =>
            {
                var header = new PostHeaderVM()
                {
                    HeaderId = x.Id,
                    CategoryId = x.CategoryId,
                    ClickCount = x.ClickCount,
                    HeaderTitle = x.Title,
                    UserId = x.UserId,
                    Username = x.User.UserName
                };

                model.Headers.Add(header);
            });

            return model;
        }
    }
}
