using DomainModels;
using LoginRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManager
{
    public class UserManager
    {
        UserRepository _userrepository = new UserRepository();

        public int AddUser(User user)
        {
            try
            {
                int id = _userrepository.CreateUser(user);
                return id;
            }
            catch (Exception ex)
            {

                throw ex; ;
            }

        }

        public User GetUser(string name, string password)
        {
            User user = new User();
            try
            {
                user = _userrepository.GetUser(name, password);
                return user;
            }
            catch (Exception ex)
            {

                throw ex; 
            }

        }
    }
}
