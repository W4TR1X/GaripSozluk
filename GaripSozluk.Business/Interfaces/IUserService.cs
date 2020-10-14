using GaripSozluk.Common.ViewModels;
using GaripSozluk.Common.ViewModels.Account;
using GaripSozluk.Common.ViewModels.User;
using GaripSozluk.Data.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface IUserService
    {
        bool Register(RegisterVM model);

        bool Login(LoginVM model);

        void Logout();

        void BlockUser(ClaimsPrincipal user, int blockedUserId);

        AppUser GetUser(ClaimsPrincipal user);

        AppUser GetUserById(int id);


        List<int> GetBlockedUserIds(ClaimsPrincipal user);
        List<BlockedUserVM> GetBlockedUsers(ClaimsPrincipal user);


        void RemoveBlockedUser(ClaimsPrincipal user, int blockedUserId);
    }
}
