using Microsoft.EntityFrameworkCore;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Model.Context;
using RestAspNet5DockerAzure.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestAspNet5DockerAzure.Repository.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private DbSet<User> dataset;

        public UserRepository(MySQLContext context) : base(context)
        {
            _context = context;
            dataset = _context.Set<User>();
        }

       
        public List<Role> GetUserRoles(User user)
        {
            if (user == null) return null;
            List<Role> roles= _context.UserRoles
                .Where(r => r.UsersId == user.Id)
                .Join(
                _context.Roles,
                r => r.RolesId,
                b => b.Id,
                (r, b) => new Role { Id = b.Id, Name = b.Name })
                .ToList();
            return  roles;
        }

        public override User Create(User user)
        {
            if (user == null) return null;
            user.Password = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return base.Create(user);
        }

        public override User Update(User user)
        {
            if (user == null) return null;
            user.Password = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return base.Update(user);
        }

        public User RefreshUserInfo(User user)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }

        }

        //Custom Methods based on Generic Repository

        public User ValidateCredentials(User user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());

            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass) && (u.Enabled == true));
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => (u.UserName == userName) && (u.Enabled == true));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => (u.UserName == userName));
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            _context.SaveChanges();
            return true;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public User Enable(long id)
        {
            var user = dataset.SingleOrDefault(p => p.Id.Equals(id));
            user.Enabled = true;
            if (user != null)
            {
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public User Disable(long id)
        {
            var user = dataset.SingleOrDefault(p => p.Id.Equals(id));
            user.Enabled = false;
            if (user != null)
            {
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
