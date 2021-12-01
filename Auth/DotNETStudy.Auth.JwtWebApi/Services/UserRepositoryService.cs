using System;
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
                new UserDto(Guid.NewGuid(), "Anu Viswan", "anu", "18710228888"),
                new UserDto(Guid.NewGuid(), "Jia Anu", "anu", "17613128735"),
                new UserDto(Guid.NewGuid(), "Naina Anu", "anu", "18916830198"),
                new UserDto(Guid.NewGuid(), "Sreena Anu", "anu", "13258685816"),
            });
        }

        public UserDto GetUser(UserModel userModel)
        {
            return _userCollection.Single(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
        }
    }
}
