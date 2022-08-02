using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<UserDTO>> Me();

        Task<ServiceResponse<string>> Login(string email, string password);

        Task<bool> UserExists(string email);
    }
}