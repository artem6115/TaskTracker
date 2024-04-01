using AutoMapper;
using BuisnnesService.Models;
using Infrastructure.Auth;
using Infrastructure.EntitiesConfigurations;
using Infrastructure.Migrations;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.Services
{
    public class UserManagerService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtAutorizationService _jwtService;
        private readonly SpiceGenerator _generator;
        private readonly Mapper _mapper;

        public UserManagerService(IUserRepository repository, JwtAutorizationService jwtService, SpiceGenerator generator , Mapper mapper)
        {
            _userRepository = repository;
            _jwtService = jwtService;
            _mapper = mapper;
            _generator = generator;
        }

        public async Task<AuthResult> LoginAsync(UserLoginDto userDto)
        {
            UserClaims userClaims;
            User User;
            NormolizeDto(userDto);


            try
            {
                User = await _userRepository.GetUserByLoginAsync(userDto.Email);
                var password = GetPasswordHash(userDto.Password + User.Spice);
                if ( password != User.Password)
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

        public async Task<AuthResult> RegistAsync(UserRegistDto user)
        {
            UserClaims userClaims;
            NormolizeDto(user);
            user.FullName = NormolizeName(user.FullName);
            var User = _mapper.Map<User>(user);
            User.Spice = await _generator.NextAsync(null!);
            User.Password = GetPasswordHash(User.Password+User.Spice);
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
            Name = Name.Trim();
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
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
        private void NormolizeDto(UserLoginDto User)
        {
            User.Email = User.Email.ToLower().Trim();
            User.Password = User.Password.Trim();
        }

        public async Task RemovePassword(string password)
        {
            var User = await _userRepository.GetUserByIdAsync(UserClaims.User.Id);
            User.Password = GetPasswordHash(password+User.Spice);
            await _userRepository.UpdateUserAcync(User);
        }

        public async Task<User> GetUser()
        {
            User user;
            try
            {
                user = await _userRepository.GetUserByIdAsync(UserClaims.User.Id);
            }
            catch { return null!; }
            return user;
        }
    }
}
