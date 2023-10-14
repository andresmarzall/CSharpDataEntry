using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataEntry.ModelsEntity;
using DataEntry.Interfaces;
using DataEntry.Repository.Repositories;
using System.Text.RegularExpressions;
using System.Linq;


//So far, this exercise is just about adding data. but with options to expand it if desired.

//Strong validations and proper error trapping ensure that data entered meets requirements.
//This makes it safe to add new functions as validations are provided when entering data, preventing database issues.

//Thanks to the use of dependency injection and the repository, high scalability is achieved, if more operations are to be added in the future.


namespace DataEntry
{
    class Program
    {
        static void Main(string[] args)
        {

            // Setting up Dependency Injection (DI) for the User Repository

            var serviceProvider = new ServiceCollection()
                .AddScoped<IUserRepository, UserRepository>()
                .BuildServiceProvider();

            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();

            // We start using do while
            do
            {
                UserEntity newUser = new UserEntity();

              
                Console.WriteLine("Enter the details of the new user:");

                do
                {
                    Console.Write("Name: ");
                    newUser.FirstName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newUser.FirstName))
                    {
                        Console.WriteLine("Name cannot be null.");
                        continue;
                    }

                    if (!IsValidName(newUser.FirstName))
                    {
                        Console.WriteLine("Name contains invalid characters. Please try again.");
                        continue;
                    }

                    Console.Write("Last name: ");
                    newUser.LastName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newUser.LastName))
                    {
                        Console.WriteLine("Last name is mandatory.");
                        continue;
                    }

                    if (!IsValidName(newUser.LastName))
                    {
                        Console.WriteLine("Last name contains invalid characters. Please try again.");
                        continue;
                    }

                    Console.Write("Age: ");
                    if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
                    {
                        Console.WriteLine("Age must be a valid number and greater than or equal to zero. Please try again.");
                        continue;
                    }
                    newUser.Age = age;

                    Console.Write("Email: ");
                    newUser.Email = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newUser.Email) || !IsValidEmail(newUser.Email))
                    {
                        Console.WriteLine("User's email must be mandatory and have a valid format. Please try again.");
                        continue;
                    }

                    Console.Write("Password: ");
                    newUser.Password = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newUser.Password) || newUser.Password.Length < 8 || !IsValidPassword(newUser.Password))
                    {
                        Console.WriteLine("User's password must have at least 8 characters and not contain invalid characters. Please try again");
                        continue;
                    }

                    break; // Exit the loop if everything is valid
                } while (true);

                userRepository.Add(newUser);

                // A database context is added to store the data.
                using (var dbContext = new DataRegisterContext())
                {
                    // The new user is added to the database.
                    dbContext.UserEntities.Add(newUser);
                    dbContext.SaveChanges();
                }

                Console.WriteLine($"User {newUser.FirstName} {newUser.LastName} has been added successfully.");

                Console.Write("Do you want to enter another user? (yes/no): ");
                string response = Console.ReadLine().ToLower();

                if (response != "yes")
                {
                    break; // Exit the loop if you do not want to enter another user
                }
            } while (true);
        }

       // We make sure that the data you enter is correctly validated, thanks to the declarations
       // we make in this section and that we used previously.name, email and password are validated

        private static bool IsValidName(string name)
        {
            // First and last name validation. Only letters and spaces are allowed
            return !Regex.IsMatch(name, @"[^a-zA-Z\s]");
        }

        private static bool IsValidEmail(string email)
        {
            // Email validation
            return Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        }
       
        private static bool IsValidPassword(string password)
        {
            // Password validation: at least 8 characters, no special characters
            return password.Length >= 8 && !Regex.IsMatch(password, @"[^a-zA-Z0-9]");
        }
    }
}
