using IMDB.DataLayer.Entities;
using IMDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.API.ServiceInterfaces
{
    public interface IUserService
    {
        long InsertPerson(Person person);
        Users UpdateUser(Users user);
        Person UpdatePerson(Person person);
        Users GetUserById(string id);
        Users GetUserByUsername(string Username);
        Users DeleteUserById(string id);
        Person DeletePersonById(long id);
        Users DeleteUserByUserName(string userName);
    }
}
