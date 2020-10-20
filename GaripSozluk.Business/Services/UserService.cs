using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels;
using GaripSozluk.Common.ViewModels.Account;
using GaripSozluk.Common.ViewModels.User;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Services
{
    public class UserService : IUserService
    {

        private readonly IBlockedUserRepository _blockedUserRepository;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(
            IBlockedUserRepository blockedUserRepository,

            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _blockedUserRepository = blockedUserRepository;
        }

        public bool Register(RegisterVM model)
        {
            var newUser = new AppUser();
            newUser.UserName = model.Username;

            if (model.BirthDate <= DateTime.Now.Date.AddYears(-18))
            {
                var result = _userManager.CreateAsync(newUser, model.Password).Result;
                if (result.Succeeded)
                {
                    var addRoleResult = _userManager.AddToRoleAsync(newUser, "User").Result;
                    if (addRoleResult.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        foreach (var error in addRoleResult.Errors)
                        {
                            model.ValidationErrors.Add("", $"{error.Code} -> {error.Description}");
                        }
                        return false;
                    }
                }

                foreach (var error in result.Errors)
                {
                    model.ValidationErrors.Add("", $"{error.Code} -> {error.Description}");
                }
            }
            else
            {
                model.ValidationErrors.Add("errorMessage", $"18 yaşından küçüklerin kayıt olması yasaktır!");
            }

            return false;
        }

        public bool Login(LoginVM model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false).Result;
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                model.ValidationErrors.Add("uname", "Hatalı giriş");
                model.ValidationErrors.Add("errorMessage", "Hatalı giriş");
            }

            return false;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync().Wait();
        }

        public void BlockUser(ClaimsPrincipal user, int blockedUserId)
        {
            var thisUser = GetUserWithRoles(user);

            if (thisUser.User.Id != blockedUserId && !_blockedUserRepository.Where(x => x.UserId == thisUser.User.Id && x.BlockedUserId == blockedUserId).Any())
            {
                _blockedUserRepository.Add(new BlockedUser()
                {
                    UserId = thisUser.User.Id,
                    BlockedUserId = blockedUserId,
                    CreateDate = DateTime.Now
                });

                _blockedUserRepository.Save();
            }
        }

        public UserWithRolesVM GetUserWithRoles(ClaimsPrincipal user)
        {
            var thisUser = _userManager.GetUserAsync(user).Result;

            if (thisUser != null)
            {
                var userRoles = _userManager.GetRolesAsync(thisUser).Result;

                //thisUser.

                return new UserWithRolesVM()
                {
                    Id = thisUser.Id,
                    User = thisUser,
                    Roles = userRoles,
                    IsAdmin = IsAdmin(userRoles),
                    BlockedUserIds = GetBlockedUserIds(user)
                };
            }

            return new UserWithRolesVM();
        }

        public bool IsAdmin(ClaimsPrincipal user)
        {
            if (user != null && user.Claims.Count() > 0)
            {
                return IsAdmin(GetUserWithRoles(user).Roles);
            }

            return false;
        }

        public bool IsAdmin(ICollection<string> roles)
        {
            if (roles.Contains("Admin"))
            {
                return true;
            }

            return false;
        }

        public AppUser GetUserById(int id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
            return user;
        }

        public List<int> GetBlockedUserIds(ClaimsPrincipal user)
        {
            var blockedUserIds = new List<int>();

            if (user.Claims.Count() > 0)
            {
                var thisUser = _userManager.GetUserAsync(user).Result;

                _blockedUserRepository.Where(x => x.UserId == thisUser.Id, new List<string> { "User" }).ToList().ForEach(x =>
                {
                    blockedUserIds.Add(x.BlockedUserId);
                });
            }

            return blockedUserIds;

        }

        public List<BlockedUserVM> GetBlockedUsers(ClaimsPrincipal user)
        {
            var thisUser = _userManager.GetUserAsync(user).Result;

            var blockedUsers = new List<BlockedUserVM>();


            _blockedUserRepository.Where(x => x.UserId == thisUser.Id, new List<string> { "User" }).ToList().ForEach(x =>
            {
                blockedUsers.Add(new BlockedUserVM()
                {
                    Id = x.BlockedUserId,
                    Username = x.User.UserName,
                    Date = x.UpdateDate ?? x.CreateDate
                });
            });

            return blockedUsers;

        }

        public void RemoveBlockedUser(ClaimsPrincipal user, int blockedUserId)
        {
            var thisUser = _userManager.GetUserAsync(user).Result;

            var blockedUser = _blockedUserRepository.Get(x => x.UserId == thisUser.Id && x.BlockedUserId == blockedUserId);

            if (blockedUser != null)
            {
                _blockedUserRepository.Remove(blockedUser);
                _blockedUserRepository.Save();
            }

        }
    }
}
