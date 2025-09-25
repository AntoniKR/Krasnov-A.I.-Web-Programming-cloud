using FinancialAssetsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FinancialAssetsApp.Data.Service
{
    public interface IAuthService
    {
        Task<User> GetUserByName(string username);
        Task<bool> ValidateUser(string username, string password);
        Task AddUser(User user, string password);
    }
}
