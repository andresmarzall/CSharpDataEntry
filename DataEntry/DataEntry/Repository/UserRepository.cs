using DataEntry.Interfaces;
using DataEntry.ModelsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

// The UserRepository class implements the IUserRepository interface and provides
// basic CRUD operations for user entities.


namespace DataEntry.Repository.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly List<UserEntity> _users = new List<UserEntity>();
       
        // Retrieves a user entity by their email address.
        public UserEntity GetByEmail(string Email)
        {
            return _users.FirstOrDefault(u => u.Email == Email);
        }

        // Retrieves all user entities in the repository.
        public IEnumerable<UserEntity> GetAll()
        {
            return _users;
        }

        // Adds a new user entity to the repository.
        public void Add(UserEntity user)
        {
            _users.Add(user);
        }

        // Update is responsible for updating an already registered entity
        public void Update(UserEntity user)
        {
            
        }

        // Deletes a user entity by their user ID.
        public void Delete(int userId)
        {
           
        }
    }
}