using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shop.Common;
using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;
using Microsoft.EntityFrameworkCore;


namespace Online_Shop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<User> AcceptVerification(int id)
        {
            try
            {
                User? user = _context.Users.Find((int)id);
                user.Verification = EVerificationStatus.ACCEPTED;
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> DenyVerification(int id)
        {
            try
            {
                User? user = _context.Users.Find((int)id);
                user.Verification = EVerificationStatus.DENIED;
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                List<User> users = _context.Users.Include(u => u.Orders).ToList();
                return users;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllSalesmans()
        {
            try
            {
                List<User> salesmans = _context.Users.Include(u => u.Products).Where(s => s.Type == EUserType.SALESMAN).ToList();
                return salesmans;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                User user = _context.Users.Include(o => o.Orders).Where(o => o.Id == id).FirstOrDefault();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> UpdateProfile(User newUser)
        {
            try
            {
                _context.Users.Update(newUser);
                _context.SaveChanges();
                return newUser;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
