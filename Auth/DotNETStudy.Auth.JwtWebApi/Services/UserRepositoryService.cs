using System.Linq;
using System.Collections.Generic;
using DotNETStudy.Auth.JwtWebApi.Dtos;
using DotNETStudy.Auth.JwtWebApi.Models;

namespace DotNETStudy.Auth.JwtWebApi.Services
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private List<UserDto> _userCollection = new List<UserDto>();
        public UserRepositoryService()
        {
            _userCollection.AddRange(new[]
            {
                new UserDto("Anu Viswan","anu","admin"),
                new UserDto("Jia Anu","jia","admin"),
                new UserDto("Naina Anu","naina","admin"),
                new UserDto("Sreena Anu","sree","admin"),
            });
        }

        public UserDto GetUser(UserModel userModel)
        {
            return _userCollection.Single(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
        }
    }
}
