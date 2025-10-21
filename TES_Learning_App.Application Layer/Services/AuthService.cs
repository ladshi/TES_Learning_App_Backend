using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TES_Learning_App.Application_Layer.DTOs.Auth.Requests;
using TES_Learning_App.Application_Layer.DTOs.Auth.Response;
using TES_Learning_App.Application_Layer.Interfaces.Infrastructure;
using TES_Learning_App.Application_Layer.Interfaces.IRepositories;
using TES_Learning_App.Application_Layer.Interfaces.IServices;
using TES_Learning_App.Domain.Entities;

namespace TES_Learning_App.Application_Layer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        //public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            //_authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            // --- MANUAL VALIDATION LOGIC ---
            var validationErrors = ValidateLogin(dto);

            // If there are any validation errors, stop immediately.
            if (validationErrors.Any())
            {
                // We return a failure response with a list of all the errors.
                return new AuthResponseDto { IsSuccess = false, Message = string.Join(" ", validationErrors) };
            }

            var user = await _unitOfWork.AuthRepository.GetUserByIdentifierAsync(dto.Identifier);
            //var user = await _authRepository.GetUserByIdentifierAsync(dto.Identifier);

            if (user == null) return new AuthResponseDto { IsSuccess = false, Message = "Invalid credentials." };

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return new AuthResponseDto { IsSuccess = false, Message = "Invalid credentials." };
            }

            var token = _tokenService.CreateToken(user);
            return new AuthResponseDto { IsSuccess = true, Message = "Login successful.", Token = token };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var validationErrors = ValidateRegistration(dto);

            if (validationErrors.Any())
            {
                return new AuthResponseDto { IsSuccess = false, Message = string.Join(" ", validationErrors) };
            }

            if (await _unitOfWork.AuthRepository.UserExistsAsync(dto.Email))
            //if (await _authRepository.UserExistsAsync(dto.Email))
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Email is already taken." };
            }
            var user = new User { Username = dto.Username, Email = dto.Email.ToLower() };

            var createdUser = await _unitOfWork.AuthRepository.RegisterAsync(user, dto.Password);
            //var createdUser = await _authRepository.RegisterAsync(user, dto.Password);

            await _unitOfWork.CompleteAsync();

            // We can log the user in immediately after they register
            var loginDto = new LoginDto
            {
                Identifier = createdUser.Email,
                Password = dto.Password // Use the original password from the registration DTO
            };

            return await LoginAsync(loginDto);
        }

        // --- THE NEW, DEDICATED VALIDATOR FOR LOGIN ---
        private List<string> ValidateLogin(LoginDto dto)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(dto.Identifier))
                errors.Add("Username or Email is required.");
            if (string.IsNullOrWhiteSpace(dto.Password))
                errors.Add("Password is required.");
            return errors;
        }

        private List<string> ValidateRegistration(RegisterDto dto)
        {
            var errors = new List<string>();

            // Username Validation
            if (string.IsNullOrWhiteSpace(dto.Username))
                errors.Add("Username is required.");
            else if (dto.Username.Length > 100)
                errors.Add("Username cannot be longer than 100 characters.");

            // Email Validation (using a more robust Regex)
            if (string.IsNullOrWhiteSpace(dto.Email))
                errors.Add("Email is required.");
            else
            {
                // Industrial Practice: Use a Regular Expression (Regex) for robust email format validation.
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                if (!emailRegex.IsMatch(dto.Email))
                    errors.Add("A valid email address is required.");
            }

            // Password Validation (with added complexity rules)
            if (string.IsNullOrWhiteSpace(dto.Password))
                errors.Add("Password is required.");
            else
            {
                if (dto.Password.Length < 8)
                    errors.Add("Password must be at least 8 characters long.");
                if (!Regex.IsMatch(dto.Password, "[A-Z]"))
                    errors.Add("Password must contain at least one uppercase letter.");
                if (!Regex.IsMatch(dto.Password, "[a-z]"))
                    errors.Add("Password must contain at least one lowercase letter.");
                if (!Regex.IsMatch(dto.Password, "[0-9]"))
                    errors.Add("Password must contain at least one number.");
                if (!Regex.IsMatch(dto.Password, "[^a-zA-Z0-9]"))
                    errors.Add("Password must contain at least one special character.");
            }

            return errors;
        }


    }
}

