using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.User;


namespace ForceShop.Domian.Interfaces.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<Models.User.User>> GetAllUsersAsync();
        Task<bool> AddUserAsync(Models.User.User User);
        bool UpdateUser(Models.User.User User);
        bool DeleteUser(Models.User.User User);
        Task<bool> DeleteUser(int UserID);
        Task<Models.User.User> GetUserByIDAsync(int UserID);
        Task<IEnumerable<Models.User.User>> GetUsersBySearchAsync(string UserInput);
        Task SaveAsync();
        Task<bool> IsExistByEmailAsync(string email);
        Task<Models.User.User> GetUserByEmailAsync(string email);

        Task<Models.User.User> GetActiveUserByEmailAsync(string email);

        Task<bool> IsSucsessConfirmCodeByEmailAsync(string email , string ConfirmCode);
        Task<int> UsersCount();

        int GetUserIDByEmail (string email);
    }
}
