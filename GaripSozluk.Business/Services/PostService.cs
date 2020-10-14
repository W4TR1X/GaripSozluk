using GaripSozluk.Business.Interfaces;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostRatingRepository _postRatingRepository;
        private readonly IUserService _userService;

        public PostService(
            IPostRepository postRepository,
            IPostRatingRepository postRatingRepository,
            IUserService userService)
        {
            _postRepository = postRepository;
            _postRatingRepository = postRatingRepository;
            _userService = userService;
        }

        public void DownVote(ClaimsPrincipal user, int postId)
        {
            var userId = _userService.GetUser(user).Id;
            var postRating = _postRatingRepository.Get(x => x.UserId == userId && x.PostId == postId);

            if (postRating != null)
            {
                if (!postRating.IsLiked)
                {
                    _postRatingRepository.Remove(postRating);
                }
                else
                {
                    postRating.IsLiked = false;
                    postRating.UpdateDate = DateTime.Now;
                }
            }
            else
            {
                postRating = new PostRating()
                {
                    PostId = postId,
                    CreateDate = DateTime.Now,
                    IsLiked = false,
                    UserId = userId
                };

                _postRatingRepository.Add(postRating);
            }

            _postRatingRepository.Save();
        }

        public int GetPostRating(int postId)
        {
            return GetLikeCount(postId) - GetDislikeCount(postId);
        }

        public int GetLikeCount(int postId)
        {
            var totalRating = _postRatingRepository.Where(x => x.PostId == postId).Select(x => x.IsLiked ? 1 : 0).Sum();
            return totalRating;
        }

        public int GetDislikeCount(int postId)
        {
            var totalRating = _postRatingRepository.Where(x => x.PostId == postId).Select(x => !x.IsLiked ? 1 : 0).Sum();
            return totalRating;
        }

        public void UpVote(ClaimsPrincipal user, int postId)
        {
            var userId = _userService.GetUser(user).Id;
            var postRating = _postRatingRepository.Get(x => x.UserId == userId && x.PostId == postId);

            if (postRating != null)
            {
                if (postRating.IsLiked)
                {
                    _postRatingRepository.Remove(postRating);
                }
                else
                {
                    postRating.IsLiked = true;
                    postRating.UpdateDate = DateTime.Now;
                }
            }
            else
            {
                postRating = new PostRating()
                {
                    PostId = postId,
                    CreateDate = DateTime.Now,
                    IsLiked = true,
                    UserId = userId
                };

                _postRatingRepository.Add(postRating);
            }

            _postRatingRepository.Save();
        }
    }
}
