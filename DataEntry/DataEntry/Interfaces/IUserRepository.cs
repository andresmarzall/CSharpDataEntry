using DataEntry.ModelsEntity;
using DataEntry.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntry.Interfaces
{
    // Interface for the User Repository, defining methods for user data operations.
    public interface IUserRepository 
    {
        // Retrieves a user entity by their email address.
        UserEntity GetByEmail(string Email);

        // Retrieves all user entities in the repository.
        IEnumerable<UserEntity> GetAll();

        // Adds a new user entity to the repository.
        void Add(UserEntity user);

        // Updates an existing user entity in the repository.
        void Update(UserEntity user);

        // Deletes a user entity by their user ID.
        void Delete(int userId);
    }
}
