using AutoMapper;
using BuisnnesService.Models;
using Infrastructure.Auth;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Services
{
    public class UserManagerService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtAutorizationService _jwtService;

        private readonly Mapper _mapper;

        public UserManagerService(IUserRepository repository, JwtAutorizationService jwtService, Mapper mapper)
        {
            _userRepository = repository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<AuthResult> LoginAsync(UserDto userDto)
        {
            UserClaims userClaims;
            User User;
            try
            {
                User = await _userRepository.GetUserByLoginAsync(userDto.Email.ToLower());
                if (GetPasswordHash(userDto.Password) != User.Password)
                    throw new ArgumentException();

                userClaims = _mapper.Map<UserClaims>(User);
            }
            catch (InvalidOperationException)
            {
                return new AuthResult()
                {
                    Success = false,
                    ErrorMessage = "Аккаунта с указаной почтой не существует"

                };
            }
            catch(ArgumentException)
            {
                return new AuthResult()
                {
                    Success = false,
                    ErrorMessage = "Пороль не правильный"
                };
            }
            var tokens = _jwtService.CreateToken(userClaims);
            User.RefreshToken = tokens.Item2;
            await _userRepository.UpdateUserAcync(User);
            return new AuthResult()
            {
                Token = tokens.Item1,
                RefreshToken = tokens.Item2,
                Success = true
            };
        }

        
        public async Task LogoutAsync()
        {
            await _userRepository.Delete_RefreshToken();
        }

        public async Task<AuthResult> RefreshAsync(string token)
        {
            try
            {
                var UserClaims = _jwtService.DesirializeToken(token);
                var tokens = _jwtService.CreateToken(UserClaims);
                var User = await _userRepository.GetUserByIdAsync(UserClaims.Id);
                if (User.RefreshToken != token)
                    throw new Exception();
                User.RefreshToken = tokens.Item2;
                await _userRepository.UpdateUserAcync(User);
                return new AuthResult() {
                    Success = true,
                    Token = tokens.Item1,
                    RefreshToken = tokens.Item2,
                };
            }
            catch
            {
                return new AuthResult()
                {
                    Success = false,
                    ErrorMessage = "Токен не валиден"
                };
            }
            
        }

        public async Task<AuthResult> RegistAsync(UserDto user)
        {
            UserClaims userClaims;
            var User = _mapper.Map<User>(user);
            User.FullName = NormolizeName(User.FullName);
            User.Email = User.Email.ToLower();
            User.Password = GetPasswordHash(User.Password);

            try
            {
                User = await _userRepository.AddUserAcync(User);
                userClaims = _mapper.Map<UserClaims>(User);
            }catch 
                {
                return new AuthResult()
                {
                    Success = false,
                    ErrorMessage = "Аккаунт с указаной почтой уже существует"
                };
            }
            var tokens = _jwtService.CreateToken(userClaims);
            User.RefreshToken = tokens.Item2;
            await _userRepository.UpdateUserAcync(User);

            return new AuthResult() {
                Token = tokens.Item1,
                RefreshToken = tokens.Item2,
                Success = true 
                };
            
        }

        private string NormolizeName(string Name)
        {
            var words = Name.Split(' ');
            var full_name = "";
            foreach(var item in words)
            {
                var sign = item[0].ToString().ToUpper();
                var wordWithOutFirstChar = item.Remove(0, 1);
                full_name += $"{sign}{wordWithOutFirstChar} ";
            }
            full_name.TrimEnd();
            return full_name;
        }

        private string GetPasswordHash(string password)
        {
            return password;
        }

    }
}
