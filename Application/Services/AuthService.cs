using BugTrackerAPI.Application.Dtos;
using BugTrackerAPI.Application.Interfaces;
using BugTrackerAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtTokenService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

            //by default user for all new user
            var role = "User";
            await _userManager.AddToRoleAsync(user, role);

            return _jwtService.GenerateToken(user, role);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!valid)
                throw new Exception("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return _jwtService.GenerateToken(user,role);
        }

    }
}
