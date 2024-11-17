using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Data.Contex;
using ForceShop.Domian.Interfaces.User;
using ForceShop.Domian.Models.User;
using Microsoft.EntityFrameworkCore;
using ForceShop.Domian.Models.User;

namespace ForceShop.Data.implementation.User
{
    public class UserRepository : IUserRepository
    {

        #region Dependency Injection

        private ForceShopContex _contex;

        public UserRepository(ForceShopContex contex)
        {
            _contex = contex;
        }

        #endregion

        //------------------------------------------------------

        #region Add

        public async Task<bool> AddUserAsync(Domian.Models.User.User User)
        {
            try
            {
                await _contex.Users.AddAsync(User);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete

        public bool DeleteUser(Domian.Models.User.User User)
        {
            try
            {
                _contex.Users.Remove(User);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int UserID)
        {
            try
            {
                var User = await GetUserByIDAsync(UserID);
                DeleteUser(User);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GetAll

        public async Task<IEnumerable<Domian.Models.User.User>> GetAllUsersAsync()
        {
            return await _contex.Users.ToListAsync();
        }

        #endregion

        #region GetByID

        public async Task<Domian.Models.User.User> GetUserByIDAsync(int UserID)
        {
            return await _contex.Users.FindAsync(UserID);
        }

        #endregion

        #region IsExist

        public async Task<bool> IsExistByEmailAsync(string email)
        {
            return await _contex.Users.AnyAsync(p => p.UserEmail == email);
        }

        #endregion

        #region GetBySearch

        public async Task<IEnumerable<Domian.Models.User.User>> GetUsersBySearchAsync(string UserInput)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Save

        public async Task SaveAsync()
        {
            _contex.SaveChangesAsync();
        }

        #endregion

        #region Update

        public bool UpdateUser(Domian.Models.User.User User)
        {
            try
            {
                _contex.Users.Update(User);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        #region Count

        public async Task<int> UsersCount()
        {
            return await _contex.Users.CountAsync();
        }

        #endregion

        #region GetUserByEmail - active - All

        public async Task<Domian.Models.User.User> GetUserByEmailAsync(string email)
        {
            return await _contex.Users.AsNoTracking().FirstOrDefaultAsync(p => p.UserEmail == email);
        }

        public async Task<Domian.Models.User.User> GetActiveUserByEmailAsync(string email)
        {
            return await _contex.Users.AsNoTracking().FirstOrDefaultAsync(p => p.UserEmail == email && p.IsDelete == false);
        }

        #endregion

        #region ValidateConfirmCodeByEmail

        public async Task<bool> IsSucsessConfirmCodeByEmailAsync(string email, string ConfirmCode)
        {
            var user = await GetUserByEmailAsync(email);

            if (user != null)
            {
                if (user.ConfirmCode == ConfirmCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }



        #endregion

        #region GetUserIDByEmail

        public int GetUserIDByEmail(string email)
        {
            return _contex.Users.AsNoTracking().FirstOrDefault(p => p.UserEmail == email.Trim().ToLower()).ID;
        }



        #endregion

    }
}
