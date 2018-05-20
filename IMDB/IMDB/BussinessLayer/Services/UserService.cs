using IMDB.API.ServiceInterfaces;
using IMDB.DataLayer.Entities;
using IMDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.BussinessLayer.Services
{
    /// <summary>
    /// Currently Delete is not finished, due to Database design it is unknown what will happen when we delete Person if that person still exists in PersonJobs
    /// </summary>
    internal partial class UserService : IUserService
    {
        private readonly MovieDBContext movieDB;

        public UserService(MovieDBContext movieDB)
        {
            this.movieDB = movieDB;
        }
        #region Insert Person
        public long InsertPerson(Person person)
        {
            movieDB.Person.Add(person);
            movieDB.SaveChanges();
            return person.PersonId;
        }
        #endregion
        /// <summary>
        /// Through Update User method we can attach Person to User.
        /// </summary>
        /// <param name="user"></param>
        #region Update User And Person
        public Users UpdateUser(Users user)
        {
            movieDB.Entry(user).State = EntityState.Modified;
            movieDB.SaveChanges();
            return user;
        }
        public Person UpdatePerson(Person person)
        {
            movieDB.Entry(person).State = EntityState.Modified;
            movieDB.SaveChanges();
            return person;
        }
        #endregion
        #region Load User
        public Users GetUserById(string id)
        {
            return movieDB.Users.SingleOrDefault(u => u.Id == id);
        }
        public Users GetUserByUsername(string Username)
        {
            return movieDB.Users.SingleOrDefault(u => u.UserName == Username);
        }
        #endregion
        #region Delete User And Person
        public Users DeleteUserById(string id)
        {
            Users user = movieDB.Users.Find(id);
            movieDB.Users.Remove(user);
            movieDB.SaveChanges();
            return user;
        }
        public Users DeleteUserByUserName(string userName)
        {
            Users user = movieDB.Users.SingleOrDefault(u => u.UserName == userName);
            movieDB.Users.Remove(user);
            movieDB.SaveChanges();
            return user;
        }
        public Person DeletePersonById(long id)
        {
            Person person = movieDB.Person.Find(id);
            movieDB.Person.Remove(person);
            movieDB.SaveChanges();
            return person;
        }
        #endregion


    }
}
